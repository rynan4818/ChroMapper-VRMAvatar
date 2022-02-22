using UnityEngine;
using System;
using System.IO;
using ChroMapper_VRMAvatar.Util;
using VRM;

namespace ChroMapper_VRMAvatar.Component
{
    public class VRMAvatarController : MonoBehaviour
    {
        async void LoadModelAsync(string path)
        {
            var instance = await VrmUtility.LoadAsync(path);
            instance.EnableUpdateWhenOffscreen();
            instance.ShowMeshes();
            instance.Root.transform.position = new Vector3(0, 0, 0);
        }
        private void Start()
        {
            AddVRMShaders.Initialize(Path.Combine(Environment.CurrentDirectory, "Player2VRM.shaders"));
            Debug.Log("Shaders Inport");
            LoadModelAsync(Path.Combine(Environment.CurrentDirectory, "AliciaSolid.vrm"));
        }
    }
}
