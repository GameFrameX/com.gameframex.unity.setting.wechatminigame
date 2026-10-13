<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting（微信小遊戲儲存）

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting.wechatminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.wechatminigame/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting.wechatminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.wechatminigame/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文件](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [簡體中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 專案簡介

GameFrameX Setting（微信小遊戲儲存）是 [Setting 主包](https://github.com/GameFrameX/com.gameframex.unity.setting)的微信小遊戲儲存後端渠道包：基於微信小遊戲 SDK（`WeChatWASM`）的同步儲存介面實作 `ISettingStorageBackend`，在 `SubsystemRegistration` 階段自註冊——引用即註冊，無需任何手工配置。

### 核心特性

- **微信小遊戲儲存整合** — 封裝 `StorageGet/Set/DeleteAllSync` 等同步儲存介面
- **自註冊（引用即註冊）** — `RuntimeInitializeOnLoadMethod(SubsystemRegistration)` 自註冊，早於 `SettingComponent` 首次建立後端
- **單槽互斥 + PlayerPrefs 兜底** — 對齊主包 `SettingStorageBackendFactory` 契約；Editor 與未註冊場景回落 Unity PlayerPrefs
- **條件編譯** — asmdef `defineConstraints`（`UNITY_WEBGL` + `ENABLE_WECHAT_MINI_GAME`），非小遊戲工程不編譯
- **IL2CPP 裁剪防護** — `Preserve` + `CroppingHelper`

## 快速開始

### 安裝

前提：已安裝 `com.gameframex.unity.setting` 3.0.0+ 與微信小遊戲 SDK（含 `Wx` 程式集）。

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.setting.wechatminigame": "1.0.0"
     }
   }
   ```
2. 直接在 `manifest.json` 的 `dependencies` 節點下添加：
   ```json
   {
      "com.gameframex.unity.setting.wechatminigame": "https://github.com/gameframex/com.gameframex.unity.setting.wechatminigame.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 方式添加：`https://github.com/gameframex/com.gameframex.unity.setting.wechatminigame.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。

### 工作原理

安裝後無需任何程式碼改動：

- `WeChatMiniGameSettingStorage` 在 `SubsystemRegistration` 階段呼叫 `SettingStorageBackendFactory.RegisterMiniGameBackend` 自註冊；
- `SettingComponent` 首次建立儲存後端時取用註冊的平台後端；Editor 與未註冊任何渠道包的場景回落 Unity PlayerPrefs；
- 重複註冊保留首個並輸出 Warning（儲存後端為「每運行環境至多一個」的互斥語義）。

## 依賴

| 包 | 說明 |
|----|------|
| `com.gameframex.unity.setting` | 3.0.0+，公共契約與 PlayerPrefs 兜底 |
| 微信小遊戲 SDK | 提供 `WeChatWASM`（`Wx` 程式集），由微信小遊戲轉換工具鏈引入 |

## 專案結構

```
Runtime/
├── WeChat/
│   └── WeChatMiniGameSettingStorage.cs      # 微信小遊戲儲存後端
├── GameFrameXSettingWeChatMiniGameCroppingHelper.cs  # 裁剪輔助類
└── GameFrameX.Setting.WeChatMiniGame.Runtime.asmdef  # 程式集定義（defineConstraints 互斥）
```

## 從 2.x 遷移

Setting 主包 3.0.0 起移除了內嵌的 `Runtime/MiniGame/` 小遊戲後端：微信小遊戲工程加裝本渠道包即可，儲存行為與 2.x 完全一致。

## 文件與資源

- [官方文件](https://gameframex.doc.alianblank.com)
- [更新日誌](./CHANGELOG.md)

## 社群與支援

- QQ群: 467608841 / 233840761

## 開源協議

詳見 [LICENSE.md](LICENSE.md) 文件。
