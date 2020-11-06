using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using System;

namespace Celeste.Mod.YetAnotherHelper.Entities
{
    [CustomEntity("YetAnotherHelper/StickyJellyfish")]
    public class StickyJellyfish : Glider
    {
        public StickyJellyfish(EntityData data, Vector2 offset) : base(data, offset)
        {
        }

        public static void Load()
        {
            On.Celeste.Glider.Update += StickyJellyUpdateHook;
            On.Celeste.Glider.OnCollideH += StickyJellyHorizontalHook;
        }

        public static void Unload()
        {
            On.Celeste.Glider.Update -= StickyJellyUpdateHook;
            On.Celeste.Glider.OnCollideH -= StickyJellyHorizontalHook;
        }

        public static void StickyJellyUpdateHook(On.Celeste.Glider.orig_Update orig, Glider self)
        {
            if (self is StickyJellyfish && self.Speed.X == 0f)
            {
                self.Speed.Y = -1.67f;
            }
            orig(self);
        }

        private static void StickyJellyHorizontalHook(On.Celeste.Glider.orig_OnCollideH orig, Glider self, CollisionData data)
        {
            if (self is StickyJellyfish)
            {
                if (data.Hit is DashSwitch)
                    (data.Hit as DashSwitch).OnDashCollide(null, Vector2.UnitX * (float)Math.Sign(self.Speed.X));
                self.Speed.X = 0f;
            }
            orig(self, data);
        }
    }
}
