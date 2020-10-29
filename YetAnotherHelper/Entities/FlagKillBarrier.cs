using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Celeste.Mod.Entities;
using Monocle;
using System.Reflection;

namespace Celeste.Mod.YetAnotherHelper.Entities
{
    [CustomEntity("YetAnotherHelper/FlagKillBarrier")]
    [Tracked(false)]
    public class FlagKillBarrier : SeekerBarrier
    {
        public string ReferenceFlag;

        public string KillBoxFalseColor;

        public string KillBoxTrueColor;

        public Color FalseColor { get; set; }

        public Color TrueColor { get; set; }

        public Color KillBarrierColor => (Scene as Level).Session.GetFlag(ReferenceFlag) ? TrueColor : FalseColor;

        private static FieldInfo particlesInfo = typeof(SeekerBarrier).GetField("particles", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);

        public FlagKillBarrier(EntityData data, Vector2 offset) : base(data, offset)
        {
            ReferenceFlag = data.Attr("flag");
            KillBoxFalseColor = data.Attr("falseColorHex", "FFFFFF");
            KillBoxTrueColor = data.Attr("trueColorHex", "FF0000");
            Depth = 0;
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);

            FalseColor = Calc.HexToColor(KillBoxFalseColor);
            TrueColor = Calc.HexToColor(KillBoxTrueColor);

            scene.Tracker.GetEntity<SeekerBarrierRenderer>().Untrack(this);
            scene.Tracker.GetEntity<FlagKillBarrierRenderer>().Track(this);
        }

        public override void Removed(Scene scene)
        {
            base.Removed(scene);

            scene.Tracker.GetEntity<FlagKillBarrierRenderer>().Untrack(this);
        }

        public override void Render()
        {
            base.Render();
            List<Vector2> list = (List<Vector2>)particlesInfo.GetValue(this);
            foreach (Vector2 vector in list)
            {
                Draw.Pixel.Draw(Position + vector, Vector2.Zero, KillBarrierColor * 0.5f);
            }

            if (Flashing)
            {
                Draw.Rect(Collider, Color.Pink * Flash * 0.5f);
            }
        }

        private void CheckForPlayerEntry()
        {
            Level level = SceneAs<Level>();
            Player player = level.Tracker.GetEntity<Player>();

            foreach (Entity entity in CollideAll<Actor>())
            {
                if (entity.GetType().ToString().Contains("Player"))
                {
                    if (level.Session.GetFlag(ReferenceFlag))
                    {
                        player.Die((player.Position - Position).SafeNormalize());
                    }
                }
            }
        }

        public override void DebugRender(Camera camera)
        {
            base.DebugRender(camera);

            Collider.Render(camera, SceneAs<Level>().Session.GetFlag(ReferenceFlag) ? Color.Red : Color.DarkRed);
        }

        public override void Update()
        {
            base.Update();
            CheckForPlayerEntry();
        }
    }
}
