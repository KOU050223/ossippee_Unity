# オシッピー（Unity）

じょぎハッカソンにて作成するプロジェクト`オシッピー`のUnity版リポジトリ

起承転結の"起"の部分に当たります

## contributor

<div style="display: flex; gap: 10px; align-items: center;">
  <div style="text-align: center;">
    <img src="https://avatars.githubusercontent.com/u/181622588?v=4" width="20%">
    <p>ikinoiiiseebi</p>
  </div>
  <div style="text-align: center;">
    <img src="https://avatars.githubusercontent.com/u/86902332?v=4" width="20%">
    <p>KOU050223</p>
  </div>
</div>

## ファイル構成について

Unityでのコンフリクトを避けるため独自のファイル構成をしています

```bash
.
├── Develop
│   ├── Ebi
│   │   └── PlayScene_copy.unity
│   └── Uomi
│       ├── MenuScene_copy.unity
│       └── SceneManager.cs
└── Release
    ├── Scenes
    │   ├── MenuScene.unity
    │   └── PlayScene.unity
    └── Scripts
```

開発をDevelopフォルダー内にある個人フォルダーにて行い完成次第、Releaseフォルダーに差し替えるコミットを行なってください

また、Releaseと区別をつけるためにファイル名に注意をしてください

めんどくさくなったらシーンを分けているため直接Releaseをいじってもそこまで問題はないと思うのでいじってください

## 注意等

コンフリクトが起きた場合一人で解消せずにhelpを送ることも推奨します

.unityファイルでコンフリクトが起きた場合基本的に後者を選んどくことが無難だと思いますが、変更した覚えがない場合前者を選びましょう。（コンフリクトが起きないことを願っておきましょう）
