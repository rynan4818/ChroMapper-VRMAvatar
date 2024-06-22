■ChroMapper-VRMAvatar　インストール方法

・ChroMapperのインストールフォルダにあるPluginsフォルダに ChroMapper-VRMAvatar.dllをコピーします。

■ChroMapper-VRMAvatarのライセンスについて

MITライセンスで配布します。著作権表記・詳細は以下の通りです。
https://github.com/rynan4818/ChroMapper-VRMAvatar/blob/main/LICENSE

■ChroMapper-VRMAvatar.dllに内包のライブラリについて

○ChroMapper-VRMAvatarには UniVRM (https://github.com/vrm-c/UniVRM) の UniVRM-0.96.2_b978.unitypackage
  でビルドした以下のDLLファイルをILMergeによって同梱しています。

        FastSpringBone.dll
        MToon.dll
        UniGLTF.dll
        UniHumanoid.dll
        VRM.dll
        VRMShaders.GLTF.IO.Runtime.dll
        VRMShaders.GLTF.UniUnlit.Runtime.dll
        VRMShaders.VRM.IO.Runtime.dll
        
  また、以下のシェーダーをアセットバンドルで vrmavatar.shaders にまとめ、DLLに埋め込んでいます。
  Assets\VRMShaders\GLTF\UniUnlit\Resources\UniGLTF\UniUnlit.shader
  Assets\VRMShaders\VRM\MToon\MToon\Resources\Shaders\MToon.shader
  Assets\VRMShaders\GLTF\IO\Resources\UniGLTF\NormalMapExporter.shader
  Assets\VRMShaders\GLTF\IO\Resources\UniGLTF\StandardMapExporter.shader
  Assets\VRMShaders\GLTF\IO\Resources\UniGLTF\StandardMapImporter.shader

  ※ChroMapperはURP（Universal Render Pipeline）を使用していますが、現時点ではMToonはURPに対応していなため
    UniVRM内でUniUnlitに変換されてい表示しています。

UniVRMの著作権表記・ライセンスは以下の通りです。
https://github.com/vrm-c/UniVRM/blob/master/LICENSE.txt

■アイコン素材について

ICONIONで作成しました。
http://iconion.com/ja/
