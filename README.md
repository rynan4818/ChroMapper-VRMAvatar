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
