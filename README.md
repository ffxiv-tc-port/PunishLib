# PunishLib

由 Puni.sh(PunishXIV)開發的函式庫，提供 EzConfig 風格的共用設定、ImGui 輔助元件
（`AboutTab`、`IconButtons`、`ImGuiEx` 等）與 puni.sh API key 驗證，供 PunishXIV 系列插件共用。

## 台服 fork 的目的

修掉兩個會讓遊戲主執行緒凍結的阻塞呼叫：

- **`API.ValidateKey()` 原本每次呼叫都 `new HttpClient()` 且沒設定 `Timeout`**（預設 100 秒）。
  `AboutTab` 的「Test Key」按鈕又是同步 `.Result` 呼叫它——網路不通時最壞會讓遊戲整整凍結
  100 秒。改為共用的靜態 `HttpClient` + 10 秒逾時，並把按鈕改成非阻塞（發起工作、每幀檢查
  `IsCompleted` 才收結果，絕不在 Draw 路徑上 `.Result`/`.Wait()`）。
- **`UseWindowsForms` 補回 csproj**：`AutoRetainer`/`Avarice`/`PalacePal` 透過 PunishLib 取得
  `System.Windows.Forms`，但 2026-07-31 統一 pin 時基底版本沒有這個屬性，導致那三個插件建置
  失敗，補回來讓單一 pin 能服務全部消費端。
- **ECommons 來源改成條件式 `ProjectReference`/`PackageReference` 互斥**（樹裡有 ECommons
  子模組就用它，沒有才退回 NuGet），避免版本序倒掛時 8 個消費端靜默改吃未加固的 NuGet 版
  ECommons；並把該分支的 NuGet lock 檔改寫到 `obj/` 底下，不弄髒版控裡那份。

## 與上游的差異

以上三項修正。目前 pin 落後上游 `master` 分支較多個 commit（上游持續在做套件整併等變更），
其餘差異未逐一比對。

## 誰在用它

艦隊裡 24 個插件消費：`Artisan`、`AutoDuty`、`AutoHook`、`AutoRetainer`、`Avarice`、`BOCCHI`、
`ChilledLeves`、`EurekaHelper`、`Explorers-Icebox`、`GatherBuddyReborn`、`ICE`、`LazyLoot`、
`Lifestream`、`NecroLens`、`NotificationMaster`、`PalacePal`、`Questionable`、`Saucy`、
`SomethingNeedDoing`、`Splatoon`、`TextAdvance`、`WrathCombo`、`YesAlready`、`visland`。

---

上游原始碼：<https://github.com/PunishXIV/PunishLib>
