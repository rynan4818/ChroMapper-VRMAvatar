using System;
using System.IO;
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
        private static Harmony _harmony;
        public const string HARMONY_ID = "com.github.rynan4818.ChroMapper-VRMAvatar";
        private UI _ui;

        [Init]
        private void Init()
        {
            _ui = new UI();
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("VRMAvatar: Plugin has loaded!");
            SceneManager.sceneLoaded += SceneLoaded;
        }

        [Exit]
        private void Exit()
        {
            _harmony.UnpatchAll(HARMONY_ID);
            Debug.Log("VRMAvatar: Application has closed!");
        }
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3) // Mapper scene
            {
                if (vrmAvatarController != null && vrmAvatarController.isActiveAndEnabled)
                    return;
                vrmAvatarController = new GameObject("VRMAvatar").AddComponent<VRMAvatarController>();
                _ui.VRMAvatarControllerSet();
                MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
                _ui.AddMenu(mapEditorUI);
                VRMAvatarController._plugin = this;
                VRMAvatarController._ui = this._ui;
                AddVRMShaders.Initialize(Path.Combine(Environment.CurrentDirectory, "vrmavatar.shaders"));
            }
        }
    }
}
