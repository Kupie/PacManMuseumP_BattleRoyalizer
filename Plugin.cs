using battleRoyale;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;

namespace BattleRoyalizer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    internal static bool DebugLogging = false;
    public static bool needsFindingButtonGuides = true;
    public static bool needsFindingCreditText = true;
    private void Awake()
    {
        string dateTimeString = "[" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "]";
        // Plugin startup logic
        Logger.LogInfo($"Plugin {dateTimeString} {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        Harmony.PatchAll();
        Plugin.Log = base.Logger;
    }

    // Debug Logging system
    public class D
    {
        public static void L(string message, string type)
        {
            string dateTimeString = "[" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "]";

            message = dateTimeString + message;
            if (!DebugLogging) {
                return;
            }
            if (type == "info")
            {
                Plugin.Log.LogInfo(message);
            }
            if (type == "error")
            {
                Plugin.Log.LogError(message);
            }
            if (type == "warn")
            {
                Plugin.Log.LogWarning(message);
            }
        }
    }

}
