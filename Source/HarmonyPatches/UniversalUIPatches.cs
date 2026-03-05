using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UniverseLib.UI;

namespace UnityExplorerFixer.HarmonyPatches {
    [HarmonyPatch(typeof(UniversalUI))]
    internal static class UniversalUIPatches {

        [HarmonyPatch("Init")]
        [HarmonyPostfix]
        private static void Init_Postfix() {
            try {
                if (UniversalUI.PoolHolder != null) {
                    RCGLifeCycle.DontDestroyForever(UniversalUI.PoolHolder);
                }

            } catch (Exception e) {
                Log.Exception(e);
            }
        }
    }
}
