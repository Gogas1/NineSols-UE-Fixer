using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using NineSolsAPI;
using NineSolsAPI.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniverseLib;

namespace UnityExplorerFixer;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInDependency("com.sinai.unityexplorer", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("com.originalnicodr.cinematicunityexplorer", BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class UnityExplorerFixer : BaseUnityPlugin {

    private Harmony harmony = null!;

    private void Awake() {
        try {
            Log.Init(Logger);
            RCGLifeCycle.DontDestroyForever(gameObject);

            var isUnityExplorerPresent = Chainloader.PluginInfos.ContainsKey("com.originalnicodr.cinematicunityexplorer") || Chainloader.PluginInfos.ContainsKey("com.sinai.unityexplorer");
            if (isUnityExplorerPresent) {
                harmony = Harmony.CreateAndPatchAll(typeof(UnityExplorerFixer).Assembly);
            }

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        } catch (Exception ex) {
            Log.Error(ex);
        }
    }

    private void OnDestroy() {
        if (harmony != null) {
            harmony.UnpatchSelf();
        }
    }
}