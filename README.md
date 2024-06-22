# ChroMapper-VRMAvatar
BeatSaberの作譜ツールの[ChroMapper](https://github.com/Caeden117/ChroMapper)でVRMアバターファイルを読み込んで表示するプラグインです。

[ChroMapper-CameraMovement](https://github.com/rynan4818/ChroMapper-CameraMovement)のVRM対応の時の検証用でしたが、単独で動くプラグインにしました。

![image](https://user-images.githubusercontent.com/14249877/155876053-1782cadd-3c9f-4f77-b3ea-b65e6bf64026.png)

# インストール方法
1. [リリースページ](https://github.com/rynan4818/ChroMapper-VRMAvatar/releases)から、最新版のプラグインのzipファイルをダウンロードして下さい。

2. ダウンロードしたzipファイルを解凍してChroMapperのインストールフォルダにある`Plugins`フォルダに`ChroMapper-VRMAvatar.dll`をコピーします。

- Ver1.0.0 では以下のファイルをChroMapperのインストールフォルダにコピーしていましたが、Ver1.0.1 よりChroMapper-VRMAvatar.dllに統合したため不要になりました。Ver1.0.0をお使いの方は、不要なため削除して下さい。
    - netstandard.dll
    - UniGLTF.dll
    - UniHumanoid.dll
    - VRM.dll
    - VRMShaders.GLTF.IO.Runtime.dll
    - VRMShaders.GLTF.UniUnlit.Runtime.dll
    - vrmavatar.shaders

# 使用方法
譜面を読み込んでエディタ画面を出して下さい。Tabキーを押すと右側にアイコンパネルが出ますので、ピンクの人物アイコンを押すと下の画像の VRMAvatar 設定パネルが開きます。

![image](https://user-images.githubusercontent.com/14249877/155876159-d74f78e9-6c87-461c-a322-cd0f171fda25.png)

* VRM File ： VRMファイルのフルパスです。[Select]ボタンで設定して下さい。
* Blinker ： VRMファイルを読み込んだ場合に、まばたきを自動で行います。
* LookAt ： VRMファイルを読み込んだ場合に、カメラ目線になります。
* Animation ： 通常は使用しません。不具合がある場合にチェックを外して見て下さい。
* Reload ： VRMファイルを変更した場合に再読み込みするボタンです。
* Select ： VRMファイルを選択します。
* Scale ： アバターのサイズ調整用です。
* X offset ： アバターの配置場所のX軸オフセット値です。0はステージ中央です。
* Y offset ： アバターの配置場所のY軸オフセット値です。0はステージ床面です。
* Z offset ： アバターの配置場所のZ軸オフセット値です。0はステージ中央です。
* X rotation ： アバターのX軸の向きです。
* Y rotation ： アバターのY軸の向きです。
* Z rotation ： アバターのZ軸の向きです。
* VRMAvatar Enable ： チェックを外すとアバターが消えます。
* Setting Save ： 上記設定を保存します。
* Close ： 設定パネルを閉じます。

## 開発者情報
このプロジェクトをビルドするには、ChroMapperのインストールパスを指定するため、以下の内容で`<ChroMapperDir>`を自分の環境に合わせて、`ChroMapper-VRMAvatar\ChroMapper-VRMAvatar.csproj.user`ファイルを作成して下さい。
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ChroMapperDir>C:\TOOL\ChroMapper\chromapper</ChroMapperDir>
  </PropertyGroup>
</Project>
```
# 添付ライブラリについて

### UniVRM
以下のファイルは [UniVRM](https://github.com/vrm-c/UniVRM) の `UniVRM-0.95.1_6465.unitypackage`でビルドしたDLLファイルを使用しています。`vrmavatar.shaders`は同パッケージのアセットから `UniUnlit.shader`をアセットバンドルで出力した物です。
- UniGLTF.dll
- UniHumanoid.dll
- VRM.dll
- VRMShaders.GLTF.IO.Runtime.dll
- VRMShaders.GLTF.UniUnlit.Runtime.dll
- vrmavatar.shaders

ライセンス： https://github.com/vrm-c/UniVRM/blob/master/LICENSE.txt

# プラグイン作成の参考
プラグインでのVRMの表示方法は[Player2VRM](https://github.com/yoship1639/Player2VRM)を参考にさせてもらいました。

URP対応の解決は[UniVRMのSimpleViewer](https://github.com/vrm-c/UniVRM/tree/master/Assets/VRM_Samples/SimpleViewer)を参考にしました。

開発時の検討内容は[MEMO.md](https://github.com/rynan4818/ChroMapper-VRMAvatar/blob/main/MEMO.md)に残しました。
