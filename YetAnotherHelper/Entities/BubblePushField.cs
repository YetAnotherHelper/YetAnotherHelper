using Microsoft.Xna.Framework;
using Celeste.Mod.Entities;
using System;
using System.Collections.Generic;
using Monocle;

// Code originally written by:
//
// WoofWoofDoggo and max480
// for Spring Collab 2020

namespace Celeste.Mod.YetAnotherHelper.Entities 
{
    [CustomEntity("YetAnotherHelper/BubbleField")]
    public class BubblePushField : Entity 
    {
        public enum ActivationMode 
        {
            Always, OnlyWhenFlagActive, OnlyWhenFlagInactive
        }

        public float Strength;

        private Dictionary<WindMover, float> WindMovers = new Dictionary<WindMover, float>();

        private int FramesSinceSpawn = 0;

        private int SpawnFrame = 30;

        public PushDirection Direction;

        private ActivationMode activationMode;

        private string flag;

        public BubblePushField(EntityData data, Vector2 offset) : this(
            data.Position + offset,
            data.Width,
            data.Height,
            data.Float("strength", 1f),
            data.Attr("direction", "right"),
            data.Enum("activationMode", ActivationMode.Always),
            data.Attr("flag", "bubble_push_field")
            ) { }

        public BubblePushField(Vector2 position, int width, int height, float strength, string direction, ActivationMode activationMode, string flag)
        {
            Position = position;
            Strength = strength;
            this.activationMode = activationMode;
            this.flag = flag;

            Enum.TryParse(direction, out Direction);

            Collider = new Hitbox(width, height);
        }

        public override void Added(Scene scene) 
        {
            base.Added(scene);
        }

        public override void Removed(Scene scene) 
        {
            base.Removed(scene);
        }

        public override void Render() 
        {
            base.Render();
        }

        public override void Update() 
        {
            base.Update();

            Session session = SceneAs<Level>().Session;
            if((activationMode == ActivationMode.OnlyWhenFlagActive && !session.GetFlag(flag))
                || (activationMode == ActivationMode.OnlyWhenFlagInactive && session.GetFlag(flag))) 
            {

                // the bubble push field is currently turned off by a session flag.
                Collidable = false;
                return;
            }

            Collidable = true;

            FramesSinceSpawn++;
            if (Strength > 0)
            {
                if (FramesSinceSpawn == SpawnFrame)
                {
                    FramesSinceSpawn = 0;
                    SpawnFrame = Calc.Random.Next(2, 10);
                    Add(new BubbleParticle(true, true));
                }

                foreach (WindMover mover in Scene.Tracker.GetComponents<WindMover>())
                {
                    if (mover.Entity.CollideCheck(this))
                    {
                        if (WindMovers.ContainsKey(mover))
                            WindMovers[mover] = Calc.Approach(WindMovers[mover], Strength, Engine.DeltaTime / .6f);
                        else
                            WindMovers.Add(mover, 0f);
                    }
                    else
                    {
                        if (WindMovers.ContainsKey(mover))
                        {
                            WindMovers[mover] = Calc.Approach(WindMovers[mover], 0f, Engine.DeltaTime / 0.3f);
                            if (WindMovers[mover] == 0f)
                                WindMovers.Remove(mover);
                        }
                    }
                }

                foreach (WindMover mover in WindMovers.Keys)
                {
                    float windSpeed = Strength * 2f * Ease.CubeInOut(WindMovers[mover]);

                    if (mover != null && mover.Entity != null && mover.Entity.Scene != null)
                        switch (Direction)
                        {
                            case PushDirection.Up:
                                mover.Move(new Vector2(0, -windSpeed));
                                break;

                            case PushDirection.Down:
                                mover.Move(new Vector2(0, windSpeed));
                                break;

                            case PushDirection.Left:
                                mover.Move(new Vector2(-windSpeed, 0));
                                break;

                            case PushDirection.Right:
                                mover.Move(new Vector2(windSpeed, 0));
                                break;
                        }
                }
            }
            else
                return;
        }
    }

    class BubbleParticle : Component 
        {
        public Vector2 Position = Vector2.Zero;

        public BubblePushField BubbleField;

        public MTexture Texture;

        private int FramesAlive = 0;

        private int FramesMaxAlive;

        private Vector2 Origin, End;

        private static readonly string[] TextureNames = new string[] { "a", "b" };

        public BubbleParticle(bool active, bool visible) : base(active, visible) { }

        public override void Added(Entity entity) 
        {
            base.Added(entity);

            BubbleField = (BubblePushField)entity;
            Position = BubbleField.Position;

            Texture = GFX.Game["particles/YetAnotherHelper/bubble_" + TextureNames[Calc.Random.Next(0, 1)]];

            // Determine bubble spawn point
            switch(BubbleField.Direction) 
            {
                case PushDirection.Up:
                    Origin = new Vector2(Calc.Random.Range(BubbleField.BottomLeft.X, BubbleField.BottomRight.X), BubbleField.BottomCenter.Y);
                    End = new Vector2(Calc.Random.Range(BubbleField.TopLeft.X, BubbleField.TopRight.X), BubbleField.TopCenter.Y);
                    FramesMaxAlive = (int)Calc.Random.Range(20, BubbleField.Height / BubbleField.Strength * .5f);
                    break;

                case PushDirection.Down:
                    Origin = new Vector2(Calc.Random.Range(BubbleField.TopLeft.X, BubbleField.TopRight.X), BubbleField.TopCenter.Y);
                    End = new Vector2(Calc.Random.Range(BubbleField.BottomLeft.X, BubbleField.BottomRight.X), BubbleField.BottomCenter.Y);
                    FramesMaxAlive = (int)Calc.Random.Range(20, BubbleField.Height / BubbleField.Strength * .5f);
                    break;

                case PushDirection.Right:
                    Origin = new Vector2(BubbleField.CenterLeft.X, Calc.Random.Range(BubbleField.BottomLeft.Y, BubbleField.TopLeft.Y));
                    End = new Vector2(BubbleField.CenterRight.X, Calc.Random.Range(BubbleField.BottomRight.Y, BubbleField.TopRight.Y));
                    FramesMaxAlive = (int)Calc.Random.Range(20, BubbleField.Width / BubbleField.Strength * .5f);
                    break;

                case PushDirection.Left:
                    Origin = new Vector2(BubbleField.CenterRight.X, Calc.Random.Range(BubbleField.BottomRight.Y, BubbleField.TopRight.Y));
                    End = new Vector2(BubbleField.CenterLeft.X, Calc.Random.Range(BubbleField.BottomLeft.Y, BubbleField.TopLeft.Y));
                    FramesMaxAlive = (int)Calc.Random.Range(20, BubbleField.Width / BubbleField.Strength * .5f);
                    break;
            }

            Position = Origin;
        }

        public override void Update() 
        {
            base.Update();

            if(FramesAlive == FramesMaxAlive)
                RemoveSelf();

            FramesAlive++;

            if(BubbleField.Direction == PushDirection.Up || BubbleField.Direction == PushDirection.Down) 
            {
                Position.X = Calc.Approach(Position.X, End.X, BubbleField.Strength / 5);
                Position.Y = Calc.Approach(Position.Y, End.Y, BubbleField.Strength * 2);
            } else {
                Position.X = Calc.Approach(Position.X, End.X, BubbleField.Strength * 2);
                Position.Y = Calc.Approach(Position.Y, End.Y, BubbleField.Strength / 5);
            }
        }

        public override void Render() 
        {
            Texture.DrawCentered(Position);
        }
    }

    public enum PushDirection 
    {
        Left,
        Right,
        Up,
        Down
    }
}
