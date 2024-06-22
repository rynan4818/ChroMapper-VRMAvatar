using System;
using System.Reflection;
using System.IO;
using ChroMapper_VRMAvatar.Component;
using SimpleJSON;
using UnityEngine;

namespace ChroMapper_VRMAvatar.Configuration
{
    public class Options
    {
        private static Options instance;
        public static readonly string settingJsonFile = Application.persistentDataPath + "/vrmavatar.json";

        public string vrmAvatarFileName = Path.Combine(Environment.CurrentDirectory, "AliciaSolid.vrm");
        public float avatarScale = 1f;
        public float avatarXoffset = 0f;
        public float avatarYoffset = 0f;
        public float avatarZoffset = 0f;
        public float avatarXrotation = 0;
        public float avatarYrotation = 0;
        public float avatarZrotation = 0;
        public float avatarCameraScale = 1.5f;
        public float originMatchOffsetY = -0.5f;
        public float originMatchOffsetZ = -1.5f;
        public bool avatarAnimation = true;
        public bool avatarBlinker = true;
        public bool avatarLookAt = true;
        public bool avatarEnable = true;

        public static Options Instance{
            get {
                if (instance is null)
                    instance = VRMAvatarController.externalControl ? new Options() : SettingLoad();
                return instance;
            }
        }

        public static Options SettingLoad()
        {
            var options = new Options();
            if (!File.Exists(settingJsonFile))
                return options;
            var members = options.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            using (var jsonReader = new StreamReader(settingJsonFile))
            {
                var optionsNode = JSON.Parse(jsonReader.ReadToEnd());
                foreach (var member in members)
                {
                    try
                    {
                        if (!(member is FieldInfo field))
                            continue;
                        var optionValue = optionsNode[field.Name];
                        if (optionValue != null)
                            field.SetValue(options, Convert.ChangeType(optionValue.Value, field.FieldType));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Optiong {member.Name} member to load ERROR!.\n{e}");
                        options = new Options();
                    }
                }
            }
            return options;
        }
        public static void SettingFileReload()
        {
            instance = SettingLoad();
        }

        public void SettingSave()
        {
            var optionsNode = new JSONObject();
            var members = this.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            foreach (var member in members)
            {
                if (!(member is FieldInfo field))
                    continue;
                optionsNode[field.Name] = field.GetValue(this).ToString();
            }
            using (var jsonWriter = new StreamWriter(settingJsonFile, false))
                jsonWriter.Write(optionsNode.ToString(2));
        }
    }
}
