using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.YetAnotherHelper.Triggers
{
    [CustomEntity("YetAnotherHelper/SpawnJellyTrigger")]
    public class SpawnJellyTrigger : Trigger
    {
        public bool OnlyOnce { get; private set; }

        public bool Activated { get; private set; }

        public SpawnJellyTrigger(EntityData data, Vector2 offset) : this(data, offset, data.Bool("onlyOnce", true), data.Bool("activated", false))
        {
        }

        public SpawnJellyTrigger(EntityData data, Vector2 offset, bool onlyOnce, bool activated) : base(data, offset)
        {
            OnlyOnce = onlyOnce;
            Activated = activated;
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            Level level = player.SceneAs<Level>();
            if(!OnlyOnce)
                level.Add(new Glider(player.Position, false, false));
            else
            {
                if(!Activated)
                {
                    level.Add(new Glider(player.Position, false, false));
                    Activated = true;
                }
            }
        }
    }
}
