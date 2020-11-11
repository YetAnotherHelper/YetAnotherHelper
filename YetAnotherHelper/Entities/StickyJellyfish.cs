using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using System;
using MonoMod.Utils;
using Monocle;

namespace Celeste.Mod.YetAnotherHelper.Entities
{
    [CustomEntity("YetAnotherHelper/StickyJellyfish")]
    public class StickyJellyfish : Glider
    {
        private static SpriteBank SpriteBank;
        private static ParticleType P_ExpandSlime;
        private static ParticleType P_GlideSlime;
        private static ParticleType P_GlideUpSlime;
        private static ParticleType P_GlowSlime;
        private static ParticleType P_ExpandOrig;
        private static ParticleType P_GlideOrig;
        private static ParticleType P_GlideUpOrig;
        private static ParticleType P_GlowOrig;

        public StickyJellyfish(EntityData data, Vector2 offset) : base(data, offset) {
            DynData<Glider> gliderData = new DynData<Glider>(this);
            Remove(gliderData.Get<Sprite>("sprite"));
            gliderData["sprite"] = SpriteBank.Create("stickyJellyfish");
            Add((Component)gliderData["sprite"]);

            P_ExpandSlime = new ParticleType(P_Expand)
            {
                Color = Calc.HexToColor("55E055"),
                Color2 = Calc.HexToColor("F7FFF4")
            };

            P_GlideSlime = new ParticleType(P_Glide)
            {
                Color = Calc.HexToColor("87E087"),
                Color2 = Calc.HexToColor("C6F5C6")
            };

            P_GlideUpSlime = new ParticleType(P_GlideUp)
            {
                Color = Calc.HexToColor("87E087"),
                Color2 = Calc.HexToColor("C6F5C6")
            };

            P_GlowSlime = new ParticleType(P_Glow)
            {
                Color = Calc.HexToColor("55E055"),
                Color2 = Calc.HexToColor("F7FFF4")
            };

            P_ExpandOrig = new ParticleType(P_Expand);
            P_GlideOrig = new ParticleType(P_Glide);
            P_GlideUpOrig = new ParticleType(P_GlideUp);
            P_GlowOrig = new ParticleType(P_Glow);
        }

        public static void Load()
        {
            On.Celeste.Glider.Update += StickyJellyUpdateHook;
            On.Celeste.Glider.OnCollideH += StickyJellyHorizontalHook;
        }

        public static void LoadContent()
        {
            SpriteBank = new SpriteBank(GFX.Game, "Graphics/YAN/StickyJellyfish.xml");
        }

        public static void Unload()
        {
            On.Celeste.Glider.Update -= StickyJellyUpdateHook;
            On.Celeste.Glider.OnCollideH -= StickyJellyHorizontalHook;
        }

        public static void StickyJellyUpdateHook(On.Celeste.Glider.orig_Update orig, Glider self)
        {
            if (self is StickyJellyfish) {
                if(self.Speed.X == 0f) {
                    self.Speed.Y = -1.67f;
                }

                P_Expand = P_ExpandSlime;
                P_Glide = P_GlideSlime;
                P_GlideUp = P_GlideUpSlime;
                P_Glow = P_GlowSlime;
                orig(self);
                P_Expand = P_ExpandOrig;
                P_Glide = P_GlideOrig;
                P_GlideUp = P_GlideUpOrig;
                P_Glow = P_GlowOrig;

                return;
            }

            orig(self);
        }

        private static void StickyJellyHorizontalHook(On.Celeste.Glider.orig_OnCollideH orig, Glider self, CollisionData data)
        {
            if (self is StickyJellyfish)
            {
                if (data.Hit is DashSwitch)
                    (data.Hit as DashSwitch).OnDashCollide(null, Vector2.UnitX * Math.Sign(self.Speed.X));
                self.Speed.X = 0f;
            }
            orig(self, data);
        }
    }
}