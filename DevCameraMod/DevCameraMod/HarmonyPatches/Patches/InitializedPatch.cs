using HarmonyLib;
using System.Collections;
using GorillaLocomotion;

namespace DevCameraMod.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(GTPlayer))]
    [HarmonyPatch(nameof(GTPlayer.Awake), MethodType.Normal)]
    internal class InitializedPatch
    {
        internal static void Postfix(GorillaLocomotion.GTPlayer __instance) => __instance.StartCoroutine(Delay());

        private static IEnumerator Delay()
        {
            yield return 0;
            
            Plugin.Instance.OnInitialized();
        }
    }
}
