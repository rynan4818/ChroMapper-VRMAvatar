using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChroMapper_VRMAvatar.Component;
using ChroMapper_VRMAvatar.UserInterface;
using ChroMapper_VRMAvatar.Util;

namespace ChroMapper_VRMAvatar
{
    [Plugin("VRMAvatar")]
    public class Plugin
    {
        public static VRMAvatarController vrmAvatarController;
        public static Harmony _harmony;
        public static UI _ui;
        public const string HARMONY_ID = "com.github.rynan4818.ChroMapper-VRMAvatar";

        [Init]
        private void Init()
        {
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("VRMAvatar: Plugin has loaded!");
            SceneManager.sceneLoaded += SceneLoaded;
            AddVRMShaders.Initialize("ChroMapper_VRMAvatar.Resources.vrmavatar.shaders", "UniGLTF/UniUnlit");
            _ui = new UI();
        }

        [Exit]
        private void Exit()
        {
            _harmony.UnpatchSelf();
            Debug.Log("VRMAvatar: Application has closed!");
        }
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3) // Mapper scene
            {
                if (vrmAvatarController != null && vrmAvatarController.isActiveAndEnabled)
                    return;
                vrmAvatarController = new GameObject("VRMAvatar").AddComponent<VRMAvatarController>();
                MapEditorUI mapEditorUI = Object.FindObjectOfType<MapEditorUI>();
                _ui.AddMenu(mapEditorUI);
            }
        }
    }
}
