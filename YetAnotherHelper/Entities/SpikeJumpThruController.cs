using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.YetAnotherHelper.Module;
using Celeste.Mod.Entities;
using System.Linq;

// Based on RainbowSpinnerColorController
// from SpringCollab2020 / MaxHelpingHand

namespace Celeste.Mod.YetAnotherHelper.Entities 
{
    [CustomEntity("YetAnotherHelper/SpikeJumpThruController")]
    public class SpikeJumpThruController : Entity 
    {
        private static bool SpikeHooked;
        private static SpikeJumpThruController CurrentController;
        private static SpikeJumpThruController NextController;
        private static float TransitionProgress = -1f;

        public SpikeJumpThruController(EntityData data, Vector2 offset) : this(data.Bool("persistent", false), offset) 
            { }

        public SpikeJumpThruController(bool persistent, Vector2 offset) : base(offset) 
        {
            YetAnotherHelperModule.Instance.Session.SpikeJumpThruHooked = persistent;

            Add(new TransitionListener
            {
                OnIn = progress => TransitionProgress = progress,
                OnOut = progress => TransitionProgress = progress,
                OnInBegin = () => TransitionProgress = 0f,
                OnInEnd = () => TransitionProgress = -1f
            });

        }

        public static void Load() 
        {
            On.Celeste.Level.LoadLevel += OnLoadLevelHook;
        }

        public static void Unload() 
        {
            On.Celeste.Level.LoadLevel -= OnLoadLevelHook;
        }

        public override void Update() 
        {
            base.Update();
            if(TransitionProgress == -1f && CurrentController == null) 
            {
                CurrentController = this;
                NextController = null;
            } 
        }

        public override void Awake(Scene scene) 
        {
            base.Awake(scene);

            NextController = this;
            if(!SpikeHooked) 
            {
                On.Celeste.Spikes.OnCollide += OnCollideHook;
                SpikeHooked = true;
            }
        }

        public override void Removed(Scene scene) 
        {
            base.Removed(scene);

            CurrentController = NextController;
            NextController = null;

            TransitionProgress = -1f;

            if(SpikeHooked && CurrentController == null) 
            {
                On.Celeste.Spikes.OnCollide -= OnCollideHook;
                SpikeHooked = false;
            }
        }

        public override void SceneEnd(Scene scene) 
        {
            base.SceneEnd(scene);

            CurrentController = NextController = null;
            if(SpikeHooked) 
            {
                On.Celeste.Spikes.OnCollide -= OnCollideHook;
                SpikeHooked = false;
            }
        }

        private static void OnLoadLevelHook(On.Celeste.Level.orig_LoadLevel orig, Level level, Player.IntroTypes introType, bool isFromLoader) 
        {
            orig(level, introType, isFromLoader);

            if(YetAnotherHelperModule.Instance.Session.SpikeJumpThruHooked && !level.Session.LevelData.Entities.Any(entity => entity.Name == "YetAnotherHelper/SpikeJumpThruController")) 
            {
                level.Add(new SpikeJumpThruController(YetAnotherHelperModule.Instance.Session.SpikeJumpThruHooked, Vector2.Zero));
                level.Entities.UpdateLists();
            }
        }

        private static void OnCollideHook(On.Celeste.Spikes.orig_OnCollide orig, Spikes spikes, Player player) 
        {
            // If the player is picking up a holdable, don't kill them.
            if(player.StateMachine.State == 8) 
            {
                return;
            }

            orig(spikes, player);
		}
    }
}
