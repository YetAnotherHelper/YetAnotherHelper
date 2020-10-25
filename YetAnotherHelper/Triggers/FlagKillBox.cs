using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.YetAnotherHelper.Triggers
{
    [CustomEntity("YetAnotherHelper/FlagKillBox")]
    class FlagKillBox : Trigger
    {
        public bool KillBoxActive { get; private set; }

        public string ReferenceFlag { get; private set; }

        public FlagKillBox(EntityData data, Vector2 offset) : this(data, offset, data.Bool("killBoxActive", false))
        {
        }

        public FlagKillBox(EntityData data, Vector2 offset, bool killBoxActive) : base(data, offset)
        {
            KillBoxActive = killBoxActive;
            ReferenceFlag = data.Attr("flag");
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);
            Level level = SceneAs<Level>();

            if (!level.Session.GetFlag(ReferenceFlag))
                KillBoxActive = false;
            else
                KillBoxActive = true;

            if (!KillBoxActive)
                return;
            else
            {
                player.Die((player.Position - Position).SafeNormalize());
            }
        }
    }
}
