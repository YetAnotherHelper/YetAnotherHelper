using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Monocle;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Celeste.Mod.YetAnotherHelper.Entities 
{
    [CustomEntity("YetAnotherHelper/ObjectHolder")]
    [Tracked(true)]
    class ObjectHolder : JumpThru 
    {
        private MTexture Sprite;
        public ObjectHolder(EntityData data, Vector2 position) : base(data.Position + position, 16, false) 
        {
            Collidable = false;
            Sprite = GFX.Game[data.Attr("spriteName", "objects/YetAnotherHelper/objectHolder")];
        }

        public override void Render() 
        {
            base.Render();

            Sprite.Draw(Position);
        }

        public static void Load() 
        {
            On.Celeste.Player.Update += PlayerUpdate;
        }

        public static void Unload() 
        {
            On.Celeste.Player.Update -= PlayerUpdate;
        }

        public static void PlayerUpdate(On.Celeste.Player.orig_Update orig, Player self) 
        {
            List<Entity> objHolders = self.Scene.Tracker.GetEntities<ObjectHolder>();

            objHolders.ForEach(holder =>
            {
                holder.Collidable = false;
            });
            orig(self);
            objHolders.ForEach(holder =>
            {
                holder.Collidable = true;
            });
        }
    }
}
