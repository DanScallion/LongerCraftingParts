using HarmonyLib;
using TaleWorlds.MountAndBlade;
using LongerCraftingParts.Settings;
using TaleWorlds.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Engine;
using System.Reflection;

namespace LongerCraftingParts
{
    public class Main : MBSubModuleBase
    {
        //MCM
        public static readonly string ModuleFolderName = "LongerCraftingParts";
        public static readonly string ModName = "LongerCraftingParts";

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        //MCM
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            try
            {
                if(IsHarmonyLoaded() && IsMCMLoaded())
                {
                    bool flag = this.harmonyLCP == null;
                    if(flag)
                    {
                        this.harmonyLCP = new Harmony("LongerCraftingParts");
                        this.harmonyLCP.PatchAll(Assembly.GetExecutingAssembly());
                    }
                }

            }
            catch (Exception ex)
            {
            }

            /*bool flag = !this._isLoaded;
            bool flag2 = flag;
            if(flag2)
            {
                //InformationManager.DisplayMessage(new InformationMessage($"LongerCraftingParts. {typeof(MCSISettings).Assembly.GetName().Version.ToString(3)}", Color.FromUint(4282569842U)));
                this._isLoaded = true;
            }*/
        }

        private Harmony harmonyLCP;

        //private bool _isLoaded = false;

        public static bool IsMCMLoaded()
        {
            bool isLoaded = false;
            List<string> modlist = Utilities.GetModulesNames().ToList<string>();
            bool check = modlist.Contains("Bannerlord.MBOptionScreen");

            if (check)
            {
                isLoaded = true;
            }
            return isLoaded;
        }

        public static bool IsHarmonyLoaded()
        {
            bool isLoaded = false;
            List<string> modlist = Utilities.GetModulesNames().ToList<string>();
            bool check = modlist.Contains("Bannerlord.Harmony");

            if (check)
            {
                isLoaded = true;
            }
            return isLoaded;
        }
    }
}