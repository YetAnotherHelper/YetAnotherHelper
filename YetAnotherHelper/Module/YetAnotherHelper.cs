using Celeste.Mod.YetAnotherHelper.Entities;
using Celeste.Mod.YetAnotherHelper.Triggers;
using System;

namespace Celeste.Mod.YetAnotherHelper.Module
{
    public class YetAnotherHelperModule : EverestModule
    {

        public static YetAnotherHelperModule Instance;

        public YetAnotherHelperModule()
        {
            Instance = this;
        }

        public override Type SettingsType => null;

        public override Type SaveDataType => null;

        public override Type SessionType => typeof(YetAnotherHelperSession);
        public YetAnotherHelperSession Session => (YetAnotherHelperSession)Instance._Session;

        public override void Load()
        {
            On.Celeste.LevelLoader.LoadingThread += add_FlagKillBarrierRenderer;
            RemoveLightSourcesTrigger.Load();
            StickyJellyfish.Load();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(bool firstLoad)
        {
        }

        public override void Unload()
        {
            On.Celeste.LevelLoader.LoadingThread -= add_FlagKillBarrierRenderer;
            RemoveLightSourcesTrigger.Unload();
            StickyJellyfish.Unload();
        }

        private static void add_FlagKillBarrierRenderer(On.Celeste.LevelLoader.orig_LoadingThread orig, LevelLoader self)
        {
            orig.Invoke(self);
            self.Level.Add(new FlagKillBarrierRenderer());
        }
    }
}