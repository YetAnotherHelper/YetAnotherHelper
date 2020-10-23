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
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(bool firstLoad)
        {
        }

        public override void Unload()
        {
        }

    }
}