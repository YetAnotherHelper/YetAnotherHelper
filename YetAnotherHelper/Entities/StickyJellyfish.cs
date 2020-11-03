using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.YetAnotherHelper.Entities
{
    [CustomEntity("YetAnotherHelper/StickyJellyfish")]
    public class StickyJellyfish : Glider
    {
        public StickyJellyfish(EntityData data, Vector2 offset) :base(data, offset)
        {
        }

        public static void Load()
        {
            On.Celeste.Glider.Update += StickyJellyUpdateHook;
        }

        public static void Unload()
        {
            On.Celeste.Glider.Update -= StickyJellyUpdateHook;
        }

        public static void StickyJellyUpdateHook(On.Celeste.Glider.orig_Update orig, Glider self)
        {
            if (self is StickyJellyfish && self.Speed.X == 0f)
            {
                self.Speed.Y = -1.67f;
            }
            orig(self);
        }
    }
}
