using UnityEngine;
using System.IO;
using System.Collections;
using VRM;
using VRMShaders;
using UniGLTF;
using ChroMapper_VRMAvatar.Configuration;
using ChroMapper_VRMAvatar.UserInterface;

namespace ChroMapper_VRMAvatar.Component
{
    public class VRMAvatarController : MonoBehaviour
    {
        public static RuntimeGltfInstance avatar { private set; get; }
        public static GameObject cm_MapEditorCamera { private set; get; }
        public static VRMBlendShapeProxy m_proxy { private set; get; }
        public static VRMLookAtHead lookAt { private set; get; }
        public static Blinker m_blink { private set; get; }
        public static Animation animation { private set; get; }
        public static Plugin _plugin { internal set; get; }
        public static UI _ui { internal set; get; }
        public static bool externalControl { get; set; }

        public async void LoadModelAsync()
        {
            if (!File.Exists(Options.Instance.vrmAvatarFileName))
            {
                Debug.LogError("Avatar File ERR!");
                return;
            }
            if (avatar != null)
            {
                GameObject.Destroy(avatar.gameObject);
                avatar = null;
            }
            VrmUtility.MaterialGeneratorCallback materialCallback = (glTF_VRM_extensions vrm) => new VRMUrpMaterialDescriptorGenerator(vrm);
            avatar = await VrmUtility.LoadAsync(Options.Instance.vrmAvatarFileName, new RuntimeOnlyAwaitCaller(), materialCallback);
            avatar.EnableUpdateWhenOffscreen();
            avatar.ShowMeshes();
            AvatarTransformSet();
            lookAt = avatar.GetComponent<VRMLookAtHead>();
            if (lookAt != null)
            {
                m_blink = avatar.gameObject.AddComponent<Blinker>();
                lookAt.Target = cm_MapEditorCamera.transform;
                lookAt.UpdateType = UpdateType.LateUpdate;
                m_proxy = avatar.GetComponent<VRMBlendShapeProxy>();
            }
            animation = avatar.GetComponent<Animation>();
            if (animation && animation.clip != null)
                animation.Play(animation.clip.name);
        }

        public static void AvatarTransformSet()
        {
            if (avatar == null)
                return;
            var position = new Vector3(Options.Instance.avatarXoffset, Options.Instance.avatarYoffset, Options.Instance.avatarZoffset);
            position *= Options.Instance.avatarCameraScale;
            avatar.Root.transform.position = new Vector3(position.x, position.y + Options.Instance.originMatchOffsetY, position.z + Options.Instance.originMatchOffsetZ);
            avatar.Root.transform.rotation = Quaternion.Euler(Options.Instance.avatarXrotation, Options.Instance.avatarYrotation, Options.Instance.avatarZrotation);
            avatar.Root.transform.localScale = new Vector3(Options.Instance.avatarScale, Options.Instance.avatarScale, Options.Instance.avatarScale) * Options.Instance.avatarCameraScale;
            if (lookAt != null)
            {
                m_blink.enabled = Options.Instance.avatarBlinker;
                lookAt.enabled = Options.Instance.avatarLookAt;
            }
            if (Options.Instance.avatarAnimation && animation && animation.clip != null)
                animation.enabled = Options.Instance.avatarAnimation;
        }

        public static void AvatarOptionsSet(string path, Vector3 offset, Vector3 rotation, float scale, float cameraScale, float origenMatchOffsetY, float origenMatchOffsetZ, bool animation, bool blinker, bool lookAt)
        {
            Options.Instance.vrmAvatarFileName = path;
            Options.Instance.avatarXoffset = offset.x;
            Options.Instance.avatarYoffset = offset.y;
            Options.Instance.avatarZoffset = offset.z;
            Options.Instance.avatarXrotation = rotation.x;
            Options.Instance.avatarYrotation = rotation.y;
            Options.Instance.avatarZrotation = rotation.z;
            Options.Instance.avatarScale = scale;
            Options.Instance.avatarCameraScale = cameraScale;
            Options.Instance.originMatchOffsetY = origenMatchOffsetY;
            Options.Instance.originMatchOffsetZ = origenMatchOffsetZ;
            Options.Instance.avatarAnimation = animation;
            Options.Instance.avatarBlinker = blinker;
            Options.Instance.avatarLookAt = lookAt;
        }

        public void AvatarEnableChange()
        {
            if (Options.Instance.avatarEnable)
            {
                if (avatar == null)
                {
                    LoadModelAsync();
                }
                else
                {
                    avatar.Root.SetActive(true);
                }
            }
            else
            {
                if (avatar != null)
                    avatar.Root.SetActive(false);
            }
        }

        private IEnumerator Start()
        {
            cm_MapEditorCamera = GameObject.Find("MapEditor Camera");
            yield return new WaitForSeconds(0.5f); //他のプラグイン起動まで待つ
            externalControl = GameObject.Find("VRMAvatarExternalControls") != null;
            if (externalControl)
            {
                _ui.ExtensionButtonDisable();
            }
            else
            {
                _ui.ExtensionButtonEnable();
            }
            if (!externalControl)
                AvatarEnableChange();
        }
    }
}
