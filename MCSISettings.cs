using System;
using MCM.Abstractions.FluentBuilder;
using TaleWorlds.Localization;
using MCM.Common;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions;
using System.Collections.Generic;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using System.ComponentModel;

namespace LongerCraftingParts.Settings
{
    public class MCSISettings : AttributeGlobalSettings<MCSISettings>
    {
        private int _maxSize = 150;
        private int _minSize = 30;

        private bool _uniformScaling = false;
        private bool _speedCap = false;

        private int _minSpeed = 70;

        public override string Id => "LongerCraftingParts";
        public override string DisplayName => $"LongerCraftingParts. {typeof(MCSISettings).Assembly.GetName().Version.ToString(3)}";
        public override string FolderName => "LongerCraftingParts";
        public override string FormatType => "xml";

        [SettingPropertyGroup("{=MCM_001_Settings_Header}General Settings:")]
        [SettingPropertyInteger("{=MCM_001_Settings_Name_001}Max Size", 110, 300, RequireRestart = false, HintText = "{=MCM_001_Settings_Info_001}Select maximum size of a crafting piece.")]
        public int MaxSize
        {
            get => _maxSize;
            set
            {
                if(_maxSize != value)
                {
                    _maxSize = value;
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyGroup("{=MCM_001_Settings_Header}General Settings:")]
        [SettingPropertyInteger("{=MCM_001_Settings_Name_002}Min Size", 10, 90, RequireRestart = false, HintText = "{=MCM_002_Settings_Info_002}Select minimum size of a crafting piece.")]
        public int MinSize
        {
            get => _minSize;
            set
            {
                if (_minSize != value)
                {
                    _minSize = value;
                    OnPropertyChanged();
                }
            }
        }

        [SettingPropertyGroup("{=MCM_001_Settings_Header}General Settings:")]
        [SettingPropertyBool("{=MCM_001_Settings_Name_003}Uniform Scaling", RequireRestart = true, HintText = "{=MCM_003_Settings_Info_003}Choose if you want weapons to scale in all dimensions instead of vanilla Lenght alone.")]
        public bool IsUniformScaling
        {
            get => _uniformScaling;
            set
            {
                _uniformScaling = value;
                OnPropertyChanged();
            }
        }

        /*[SettingPropertyGroup("{=MCM_001_Settings_Header}General Settings:")]
        [SettingPropertyBool("{=MCM_001_Settings_Name_004}Speed cap", RequireRestart = false, HintText = "{=MCM_004_Settings_Info_003}If ON, weapon speed will never go below certain value.")]
        public bool SpeedCap
        {
            get => _speedCap;
            set
            {
                _speedCap = value;
                OnPropertyChanged();
            }
        }

        [SettingPropertyGroup("{=MCM_001_Settings_Header}General Settings:")]
        [SettingPropertyInteger("{=MCM_001_Settings_Name_004}Speed cap value", 50, 100, RequireRestart = false, HintText = "{=MCM_004_Settings_Info_003}Set minimum speed that weapon cannot go below. ")]
        public int minSpeed
        { 
            get => _minSpeed;
            set
            {
                _minSpeed = value;
                OnPropertyChanged();
            }
        }*/

        /*public void Settings()
        {
            TextObject MCSISettings00 = new TextObject("LongerCraftingParts");
            TextObject MCSISettings01 = new TextObject("Size Options:");
            TextObject MCSISettings02 = new TextObject("Max Size");
            TextObject MCSISettings03 = new TextObject("Min Size");
            TextObject MCSISettings04 = new TextObject("Uniform Scaling");

            /*var builder = BaseSettingsBuilder.Create(MCSISettings00.ToString(), MCSISettings00.ToString())!
                .SetFormat("xml")
                .SetFolderName(Main.ModuleFolderName)
                .CreateGroup("General Settings:", groupBuilder => groupBuilder
                .AddInteger("minsize", MCSISettings03.ToString(),10, 90, new ProxyRef<int>(() => MinSize, o => MinSize = o), floatingBuilder => floatingBuilder
                    .SetRequireRestart(false)
                    .SetHintText("Select minimum size of a crafting piece."))
                .AddInteger("maxsize", MCSISettings02.ToString(), 110, 300, new ProxyRef<int>(() => _maxSize, o => _maxSize = o), floatingBuilder => floatingBuilder
                    .SetRequireRestart(false)
                    .SetHintText("Select maximum size of a crafting piece."))
                .AddBool("uniformScaling", MCSISettings04.ToString(), new ProxyRef<bool>(() => _uniformScaling, o => _uniformScaling = o), boolBuilder => boolBuilder
           .SetHintText("Test")
           .SetRequireRestart(true))
                ).CreatePreset("default", "Default", presetBuilder => presetBuilder
                 .SetPropertyValue("minsize", 30)
                 .SetPropertyValue("maxsize", 150)
                 .SetPropertyValue("uniformScaling", false))
                 .CreatePreset("vanilla", "Vanilla", presetBuilder => presetBuilder
                 .SetPropertyValue("minsize", 90)
                 .SetPropertyValue("maxsize", 110)
                 .SetPropertyValue("uniformScaling", false))
                 .CreatePreset("max", "Max", presetBuilder => presetBuilder
                 .SetPropertyValue("minsize", 10)
                 .SetPropertyValue("maxsize", 300)
                 .SetPropertyValue("uniformScaling", false));
                    
            var globalSettings = builder.BuildAsGlobal();
            globalSettings.Register();
        }*/

        public override IEnumerable<ISettingsPreset> GetBuiltInPresets()
        {
            foreach (ISettingsPreset preset in base.GetBuiltInPresets())
            {
                yield return preset;
            }

            yield return new MemorySettingsPreset(this.Id, "vanilla", "Vanilla", () => new MCSISettings
            {
                MinSize = 90,
                MaxSize = 110,
                //SpeedCap = false,
                //minSpeed = 70
            });
            yield return new MemorySettingsPreset(this.Id, "max", "Max", () => new MCSISettings
            {
                MinSize = 10,
                MaxSize = 300,
                //SpeedCap = true,
               // minSpeed = 85
            });
            yield break;
        }

        public MCSISettings()
        {
            this.PropertyChanged += this.MCSISettings_PropertyChanged;
        }

        private void MCSISettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool flag = e.PropertyName == "Debug";
            if(flag)
            {
                this.IsUniformScaling = false;
            }
        }

    }
}