using HarmonyLib;
using TaleWorlds.Core;

namespace LongerCraftingParts
{
    [HarmonyPatch(typeof(Crafting), "ScaleThePiece")]
    internal class Scale
    {
        public static bool Prefix(CraftingPiece.PieceTypes scalingPieceType, ref int
            percentage)
        {
            if(LongerCraftingParts.Settings.MCSISettings.Instance.MaxSize != 110)
            {
                percentage = (percentage - 90) * (LongerCraftingParts.Settings.MCSISettings.Instance.MaxSize / 20);
            }
            if (percentage < LongerCraftingParts.Settings.MCSISettings.Instance.MinSize)
                percentage = LongerCraftingParts.Settings.MCSISettings.Instance.MinSize;
            if (percentage > LongerCraftingParts.Settings.MCSISettings.Instance.MaxSize)
                percentage = LongerCraftingParts.Settings.MCSISettings.Instance.MaxSize;
            //MBInformationManager.AddQuickInformation(new TaleWorlds.Localization.TextObject(percentage.ToString()));
            return true;
        }
    }
}