# ChroMapper-VRMAvatar
VRM読み込みの検証用

[Player2VRM](https://github.com/yoship1639/Player2VRM)を参考に実装。

## 検証方法
1. このリポジトリをクローンする
2. ChroMapperのインストールパスを指定するため、以下の内容で`<ChroMapperDir>`を自分の環境に合わせて、`ChroMapper-VRMAvatar\ChroMapper-VRMAvatar.csproj.user`ファイルを作成する。
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ChroMapperDir>C:\TOOL\ChroMapper\chromapper</ChroMapperDir>
  </PropertyGroup>
</Project>
```
3. VS2019でビルドする
4. DLLフォルダのDLLファイルを ChroMapper.exe と同じフォルダにコピー
5. [`AliciaSolid.vrm`](https://3d.nicovideo.jp/works/td32797)と、Player2VRMから[`Player2VRM.shaders`](https://github.com/yoship1639/Player2VRM/blob/master/Assets/Player2VRM.shaders)を入手してChroMapper.exe と同じフォルダにコピー
6. VRMモデルが読み込まれるが、ピンクになってしまう。現状ここで止まってる。

DLLフォルダのDLLは、UniVRMの`UniVRM_Samples-0.95.1_6465.unitypackage`を2021.1.15f1にインポートして、`AliciaSolid.vrm`を読み込んでHierarchyに配置してビルドした出力からコピーした。
  
## 調べたこと
ChroMapperのUnityプロジェクトにUniVRMのパッケージをインポートして、`AliciaSolid.vrm`を読み込んでも同様にピンクになるので、根本的なところが問題あるんじゃないかと思っている。

ほとんどの人はVRMでアバター表示しているので、VRM対応したいところだけど、なにぶん私がアバター周りを触ってこなかったのでサッパリです。とりあえず、カスタムアバターなら読めるので、優先度は低い感じです。

### 2022/02/23
どうもChroMapperがURP(Universal Render Pipeline)を使ってるのが原因ぽい。
- [Unity2021.2.4f1のURPで、OpenXRのみでVRアプリ開発をする](https://qiita.com/Kazu_Sack/items/776accbe1e8582b3d735)
- [VRoidのモデルのマテリアルをURP対応のマテリアルに変更してみた](https://qiita.com/carnaite0224/items/59537e4b2ce4a9bd0eb9)
- [URP/HDRPを使ったVRM対応アプリの開発方法 / VRM Importer Extension](https://speakerdeck.com/sotanmochi/vrm-importer-extension)
- [VRMImporterExtension-URP](https://github.com/sotanmochi/VRMImporterExtension-URP)

このあたりを参考に、プラグインで読み込み時にURP対応のシェーダーに変換すれば表示可能ぽい。
結構難易度高めなのと、上手く行かない場合は手動でモデルのシェーダーを変更する必要があるので、CameraMovement用のカスタムアバターの作成を補助する方向に考えた方が良いのでは無いかと思ってます。

- CameraMovementで使えるカスタムアバターの条件
    - モデルのルートのGameObject名が`_CustomAvatar`であること
    - AssetBundleで出力されていること
    - シェーダーが[BeatSaber/Unlit Glow](https://github.com/Caeden117/ChroMapper/blob/master/Assets/_Graphics/Shaders/Beat%20Saber/sh_custom_unlit.shader)、[BeatSaber/Lit Glow](https://github.com/Caeden117/ChroMapper/blob/master/Assets/_Graphics/Shaders/Beat%20Saber/sh_custom_lit.shader)、[BeatSaber/Standard](https://github.com/Caeden117/ChroMapper/blob/master/Assets/_Graphics/Shaders/Beat%20Saber/bs_standard.shader)であればChroMapperで持ってるのでこれらを使用すること。

なので、上記3つのシェーダーと選択したモデルのルートオブジェクトを`_CustomAvatar`でAssetBundle出力するエディタスクリプトを作成したインポートできるプロジェクトを作成すれば、任意のUnityプロジェクトで出力できるので良いのかなと思っています。シェーダーとテクスチャの張替え作業は必要ですが。
