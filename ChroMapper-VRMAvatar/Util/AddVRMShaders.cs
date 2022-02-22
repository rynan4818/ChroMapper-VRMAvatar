﻿using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ChroMapper_VRMAvatar.Util
{
    public static class AddVRMShaders
    {
        public static Dictionary<string, Shader> Shaders { get; } = new Dictionary<string, Shader>();

        public static void Initialize(string shadersFile)
        {
            var bundlePath = Path.Combine(Environment.CurrentDirectory, shadersFile);
            if (File.Exists(bundlePath))
            {
                var assetBundle = AssetBundle.LoadFromFile(bundlePath);
                var assets = assetBundle.LoadAllAssets<Shader>();
                foreach (var asset in assets)
                {
                    UnityEngine.Debug.Log("Add Shader: " + asset.name);
                    Shaders.Add(asset.name, asset);
                }
            }
        }
    }
}