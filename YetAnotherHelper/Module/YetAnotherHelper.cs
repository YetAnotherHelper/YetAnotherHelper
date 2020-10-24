using Celeste.Mod.YetAnotherHelper.Entities;
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

        public override Type SessionType => null;

        public override void Load()
        {
            On.Celeste.LevelLoader.LoadingThread += add_FlagKillBarrierRenderer;
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
        }

        private static void add_FlagKillBarrierRenderer(On.Celeste.LevelLoader.orig_LoadingThread orig, LevelLoader self)
        {
            orig.Invoke(self);
            self.Level.Add(new FlagKillBarrierRenderer());
        }
    }
}