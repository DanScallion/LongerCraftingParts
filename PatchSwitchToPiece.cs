using HarmonyLib;
using TaleWorlds.Core;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;


namespace LongerCraftingParts
{
    [HarmonyPatch(typeof(Crafting), "SwitchToPiece")]
    internal static class PatchSwitchToPiece
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var include = false;
            foreach (var instruction in instructions)
            {
                if (include)
                    yield return instruction;
                else if (instruction.opcode == OpCodes.Callvirt
                && instruction.operand is MethodInfo methodInfo
                && methodInfo.Name == "SetScale")
                    include = true;
            }
        }
    }
}