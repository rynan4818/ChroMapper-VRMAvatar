# ChroMapper-VRMAvatar
VRM読み込みの検証用

[Player2VRM](https://github.com/yoship1639/Player2VRM)を参考に実装。

## 検証方法
1. このリポジトリをクローンする
2. 以下の内容で<ChroMapperDir>を自分の環境に合わせて、ChroMapper-VRMAvatar\ChroMapper-VRMAvatar.csproj.user を作成する。
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
5. `AliciaSolid.vrm`と、[Player2VRM](https://github.com/yoship1639/Player2VRM)から`Player2VRM.shaders`を入手してChroMapper.exe と同じフォルダにコピー
6. VRMモデルが読み込まれるが、ピンクになってしまう。現状ここで止まってる。

DLLフォルダのDLLは、UniVRMの`UniVRM_Samples-0.95.1_6465.unitypackage`を2021.1.15f1にインポートして、`AliciaSolid.vrm`を読み込んでHierarchyに配置してビルドした出力からコピーした。
