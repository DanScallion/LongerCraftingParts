using HarmonyLib;
using TaleWorlds.Core;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Label = System.Reflection.Emit.Label;
using System;
using LongerCraftingParts.Settings;
using MCM.Abstractions.Base.Global;
using System.Runtime.CompilerServices;

namespace LongerCraftingParts
{
    [HarmonyPatch(typeof(CraftingPiece), "Deserialize")]
    internal class uniformScaling
    {
        private static bool Prepare()
        {
            MCSISettings settings = GlobalSettings<MCSISettings>.Instance;
            return settings != null && settings.IsUniformScaling;
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var instructionList = new List<CodeInstruction>(instructions);
            var instructionListNew = new List<CodeInstruction>(instructions);
            bool done = false;
            bool isLabeled = false;
            Label jumpTo = il.DefineLabel();

            //find label
            for (int j = 0; j < instructionList.Count - 1; j++)
            {
                if (instructionList[j].opcode == OpCodes.Ldc_I4_1 && instructionList[j + 1].opcode == OpCodes.Br_S && instructionList[j - 1].opcode == OpCodes.Br_S && !isLabeled)
                {
                    jumpTo = instructionList[j].labels[0];
                    isLabeled = true;
                }
            }
            //find injecting place
            for (int i = 0; i < instructionList.Count -1; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldarg_0 && instructionList[i + 1].opcode == OpCodes.Call
                     && instructionList[i + 1].operand is MethodInfo methodInfo && methodInfo.Name == "get_PieceType" && !done)
                {
                    int insertAt = i;
                    instructionListNew.Insert(insertAt, new CodeInstruction(OpCodes.Ldarg_0));
                    instructionListNew.Insert(insertAt + 1, instructionList[i + 1]);
                    instructionListNew.Insert(insertAt + 2, new CodeInstruction(OpCodes.Brfalse_S, jumpTo));
                    done = true;
                }

            }
            return instructionListNew;          
        }
        
    }
}
