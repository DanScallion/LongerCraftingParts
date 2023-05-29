using System;
using MCM.Abstractions.FluentBuilder;
using TaleWorlds.Localization;
using MCM.Common;

namespace LongerCraftingParts.Settings
{
    class MCSISettings : IDisposable
    {
        private static MCSISettings _instance;

        public int MaxSize { get; set; }
        public int MinSize { get; set; }
        public bool FirstRun { get; set; }


        public static MCSISettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MCSISettings();
                }
                return _instance;
            }
        }

        public void Settings()
        {
            TextObject MCSISettings00 = new TextObject("LongerCraftingParts");
            TextObject MCSISettings01 = new TextObject("Size Options:");
            TextObject MCSISettings02 = new TextObject("Max Size");
            //TextObject MCSISettings03 = new TextObject("Min Size");

            var builder = BaseSettingsBuilder.Create(MCSISettings00.ToString(), MCSISettings00.ToString())!
                .SetFormat("xml")
                .SetFolderName(Main.ModuleFolderName)
                .SetSubFolder(Main.ModName)
                .CreateGroup("General Settings:", groupBuilder => groupBuilder
                /*.AddInteger("minsize", MCSISettings03.ToString(),30, 100, new ProxyRef<int>(() => MinSize, o => MinSize = o), floatingBuilder => floatingBuilder
                    .SetRequireRestart(false)
                    .SetHintText("Select minimum size of a crafting piece."))*/
                .AddInteger("maxsize", MCSISettings02.ToString(), 60, 300, new ProxyRef<int>(() => MaxSize, o => MaxSize = o), floatingBuilder => floatingBuilder
                    .SetRequireRestart(false)
                    .SetHintText("Select maximum size of a crafting piece.")));

            var globalSettings = builder.BuildAsGlobal();
            globalSettings.Register();
            var perSaveSettings = builder.BuildAsPerSave();
            perSaveSettings.Register();

            if (!FirstRun)
            {
                MCSISettings.Instance.FirstRun = true;
                MCSISettings.Instance.MinSize = 50;
                MCSISettings.Instance.MaxSize = 150;
            }
        }

        public void Dispose()
        {
            ((IDisposable)_instance).Dispose();
        }
    }
}