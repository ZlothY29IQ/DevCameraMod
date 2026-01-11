using DevCameraMod.Scripts;
using HarmonyLib;

namespace DevCameraMod.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(VRRig), nameof(VRRig.SharedStart))]
    internal class RigPatch
    {
        internal static void Postfix(VRRig __instance) => __instance.gameObject.AddComponent<TagSpawner>();
    }
}
