using Celeste.Mod.Entities;
using Monocle;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Celeste.Mod.YetAnotherHelper.Module;

// Code originally written by:
//
// WoofWoofDoggo and max480
// for Spring Collab 2020

namespace Celeste.Mod.YetAnotherHelper.Triggers 
{
    [CustomEntity("YetAnotherHelper/RemoveLightSourcesTrigger")]
    [Tracked]
    public class RemoveLightSourcesTrigger : Trigger
    {
        public static float alphaFade
        {
            get
            {
                return YetAnotherHelperModule.Instance.Session.LightSourceAlpha;
            }
            set
            {
                YetAnotherHelperModule.Instance.Session.LightSourceAlpha = value;
            }
        }

        private float offsetTo;
        private float offsetFrom;
        private PositionModes positionMode;
        private bool onlyOnce;
        
        public static void Load()
        {
            On.Celeste.BloomRenderer.Apply += BloomRendererHook;
            On.Celeste.LightingRenderer.BeforeRender += LightHook;
        }

        public static void Unload()
        {
            On.Celeste.BloomRenderer.Apply -= BloomRendererHook;
            On.Celeste.LightingRenderer.BeforeRender -= LightHook;
        }

        private static void BloomRendererHook(On.Celeste.BloomRenderer.orig_Apply orig, BloomRenderer self, VirtualRenderTarget target, Scene scene)
        {
            if(alphaFade < 1f)
            {
                // multiply all alphas by alphaFade, and back up original values.
                List<BloomPoint> affectedBloomPoints = new List<BloomPoint>();
                List<float> originalAlpha = new List<float>();
                foreach(BloomPoint bloomPoint in scene.Tracker.GetComponents<BloomPoint>().ToArray())
                {
                    if(bloomPoint.Visible)
                    {
                        affectedBloomPoints.Add(bloomPoint);
                        originalAlpha.Add(bloomPoint.Alpha);
                        bloomPoint.Alpha *= alphaFade;
                    }
                }

                // render the bloom.
                orig(self, target, scene);

                // restore original alphas.
                int index = 0;
                foreach(BloomPoint bloomPoint in affectedBloomPoints)
                {
                    bloomPoint.Alpha = originalAlpha[index++];
                }
            } else
            {
                // alpha multiplier is 1: nothing to modify, go on with vanilla.
                orig(self, target, scene);
            }
        }

        private static void LightHook(On.Celeste.LightingRenderer.orig_BeforeRender orig, LightingRenderer self, Scene scene)
        {
            if(alphaFade < 1f)
            {
                // multiply all alphas by alphaFade, and back up original values.
                List<VertexLight> affectedVertexLights = new List<VertexLight>();
                List<float> originalAlpha = new List<float>();
                foreach(VertexLight vertexLight in scene.Tracker.GetComponents<VertexLight>().ToArray())
                {
                    if(vertexLight.Visible && !vertexLight.Spotlight)
                    {
                        affectedVertexLights.Add(vertexLight);
                        originalAlpha.Add(vertexLight.Alpha);
                        vertexLight.Alpha *= alphaFade;
                    }
                }

                // render the lighting.
                orig(self, scene);

                // restore original alphas.
                int index = 0;
                foreach(VertexLight vertexLight in affectedVertexLights)
                {
                    vertexLight.Alpha = originalAlpha[index++];
                }
            } else
            {
                // alpha multiplier is 1: nothing to modify, go on with vanilla.
                orig(self, scene);
            }
        }

        public RemoveLightSourcesTrigger(EntityData data, Vector2 offset) : base(data, offset)
        {
            offsetTo = data.Float("offsetTo", 1f);
            offsetFrom = data.Float("offsetFrom", 1f);
            positionMode = data.Enum<PositionModes>("positionMode");
            onlyOnce = data.Bool("onlyOnce");
        }

        public override void OnStay(Player player)
        {
            base.OnStay(player);
            alphaFade = MathHelper.Lerp(offsetFrom, offsetTo, GetPositionLerp(player, positionMode));
        }

        public override void OnLeave(Player player)
        {
            base.OnLeave(player);

            if(onlyOnce) 
                RemoveSelf();
        }
    }
}