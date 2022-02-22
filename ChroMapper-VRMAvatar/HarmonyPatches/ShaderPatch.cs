using HarmonyLib;
using UnityEngine;
using ChroMapper_VRMAvatar.Util;

namespace ChroMapper_VRMAvatar.HarmonyPatches
{
    [HarmonyPatch(typeof(Shader))]
    [HarmonyPatch(nameof(Shader.Find))]
    static class ShaderPatch
    {
        static bool Prefix(ref Shader __result, string name)
        {
            if (AddVRMShaders.Shaders.TryGetValue(name, out var shader))
            {
                __result = shader;
                return false;
            }

            return true;
        }
    }
}
