using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using System.Linq;
using Monocle;
using System.Collections;
using System;

namespace Celeste.Mod.YetAnotherHelper.Triggers
{
    [CustomEntity("YetAnotherHelper/LightningStrikeTrigger")]
    public class LightningStrikeTrigger : Trigger
    {
        public bool Activated { get; private set; }

        private readonly float PlayerOffset;

        private readonly float VerticalOffset;

        private readonly float StrikeHeight;

        private readonly int Seed;

        private readonly float Delay;

        private readonly bool Raining;

        private readonly bool Flash;

        private readonly bool Constant;

        private int ConstantTimer = -1;

        private readonly Random rand;

        public LightningStrikeTrigger(EntityData data, Vector2 offset) : this(data, offset, data.Float("playerOffset", 0f), data.Float("verticalOffset", 0), data.Float("height", 0), data.Int("seed", 0), data.Float("delay", 0f), data.Bool("raining", true), data.Bool("flash", true), data.Bool("constant", false))
        {
        }

        public LightningStrikeTrigger(EntityData data, Vector2 offset, float playerOffset, float verticalOffset, float strikeHeight, int seed, float delay, bool raining, bool flash, bool constant) : base(data, offset)
        {
            PlayerOffset = playerOffset;
            VerticalOffset = verticalOffset;
            StrikeHeight = strikeHeight;
            Seed = seed;
            Delay = delay;
            Raining = raining;
            Flash = flash;
            Constant = constant;

            rand = new Random(seed);
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            if (!Activated && !Constant)
            {
                Strike(player);
                Activated = true;
            }
        }

        public override void OnStay(Player player)
        {
            base.OnStay(player);

            if (Constant)
            {
                ConstantTimer -= 1;

                if (ConstantTimer <= 0)
                {
                    Strike(player);
                    ConstantTimer = rand.Next(40, 60);
                }
            }
        }

        public void Strike(Player player)
        {
            Level level = player.SceneAs<Level>();
            if (StrikeHeight == 0)
                level.Add(new LightningStrike(new Vector2(player.X + PlayerOffset, level.Bounds.Top), rand.Next(1, 100), level.Bounds.Height, Delay));
            else
                level.Add(new LightningStrike(new Vector2(player.X + PlayerOffset, player.Y + VerticalOffset), rand.Next(1, 100), StrikeHeight, Delay));

            Add(new Coroutine(ThunderEffect(level), true));

            if (Raining && !level.Background.Backdrops.OfType<RainFG>().Any())
            {
                level.Background.Backdrops.Add(new RainFG());
            }
        }

        private IEnumerator ThunderEffect(Level level)
        {
            yield return Delay;
            Audio.Play("event:/new_content/game/10_farewell/lightning_strike");
            level.Shake(0.3f);

            if (Flash)
                level.Flash(Color.White, false);

            yield break;
        }
    }
}
