# SCHALE Charge Plus · v0.1.2-ba

首个面向公开的构建：把 [GlacierGlimmer/zmd-charge-plus](https://github.com/GlacierGlimmer/zmd-charge-plus) 的 Windows 桌面状态栏 HUD 整体重绘为《蔚蓝档案》的视觉语言，并加入按角色形态切换的主题层。

功能、变量库、方案系统、动画时间线、多实例与多显示器逻辑全部沿用上游，本次没有删改任何功能。

---

## 八套角色主题

设置 → **显示与位置 → 角色主题**。切换即预览，点「保存并应用」后持久化。

| 主题 | 配色 | 光环形状 |
| :-- | :-- | :-- |
| 夏莱（默认）SCHALE | `#212529` 底盘 + `#00D9FA` 青 | 素扁椭圆环 |
| 空崎日奈 Hina | 光环品红 `#E448F6`、深紫 `#3C0048` | 尖刺冠冕 |
| 日奈（泳装）Swimsuit | `#1EAAF0` 天蓝 + `#D2E6F0` 淡蓝 + `#F0B4D2` 粉 | 尖刺冠冕 |
| 日奈（礼服）Dress | `#E47EEA` 淡紫粉 + `#463C78` 靛紫 | 尖刺冠冕 |
| 日奈（睡衣）Nightwear | `#FCEA96` 奶黄 + `#F0B4C8` 粉 + `#241A2E` 暗 | 尖刺冠冕 |
| 龙华妃咲 Kisaki | `#1E244E` 深靛 + `#66AECC` 青蓝 + `#CCD2D2` 银白 | 云气卷曲月牙 |
| 妃咲（泳装）Swimsuit | `#464664` 蓝紫 + `#DCAAAA` 珊瑚 + `#EAEAF0` 近白 | 云气卷曲月牙 |
| 妃咲（妃妃）Kindergarten | `#FCEA96` 小黄帽 + `#FABEC8` 樱粉 + `#E4A854` 橙 | 云气卷曲月牙 |

七套角色皮肤的关键色取自对应形态官方立绘的像素统计（高饱和色众数 + 高光/暗部采样），不是同一色相旋转出来的明暗梯度。

光环形状绑角色不绑服装：日奈四个形态共用尖刺冠冕，妃咲三个形态共用云气月牙。这是原作里的设定，也是肉眼一眼能认角色的那个符号。

---

## 视觉改动

**配色** — 以 SchaleDB 官方暗色主题令牌为准：`#212529` 深灰底、`#00D9FA` 标志青、`#006BFF` 蓝强调，替换原版终末地黄绿体系。

**光环** — HUD 左侧图标盘上方一圈扁椭圆光环，三层描边叠出发光感。完整动画与简洁动画两条时间线都会点亮它。运行时按当前主题替换几何形状。

**胶囊** — `LinearGradientBrush` 深海蓝渐变 + 青色描边 + 外发光；三个渐变色标与描边、辉光都随主题实时重新着色，不需要重启。

**图标** — 应用与托盘图标重绘为「青色光环 + 深色角色盘」，ICO 覆盖 16 / 20 / 24 / 32 / 40 / 48 / 64 / 128 / 256 px 九档。全部帧用 DIB 而非 PNG 压缩，旧版 shell 与 GDI+ 都能正确解码。

**字体** — HUD 数字与标题改用 **Nunito Sans**（SchaleDB 的界面字体），从上游可变字体实例化成 400 / 500 / 600 / 700 四个静态字重；中文回退系统雅黑 / 苹方。

**控件** — 覆盖 Avalonia Fluent 的 `SystemControl*` / `ComboBox*` / `ListBox*` 资源键，下拉框、列表、按钮不再露出默认暗色暖灰。语言选择窗的列表底色单独修过。

**品牌** — 显示名统一为 SCHALE Charge Plus，默认标签文案 `/// SCHALE MONITOR`。

程序集名、命名空间、`avares://` 资源路径、可执行文件名都保持 `EndfieldChargePlus` 不变——这样旧的配置、开机启动项和更新通道不会因为改名而断掉。

---

## 与上游的差异边界

改动集中在视觉层与主题层，具体是这些文件：

- 新增 `Customization/HudThemeCatalog.cs`（主题定义、光环几何、运行时配色注入）
- 新增 `Assets/Fonts/NunitoSansHud-{Regular,Medium,SemiBold,Bold}.ttf` 与 OFL 授权
- `Styles/HudTheme.axaml`、`Styles/Geometries.axaml`、`App.axaml`、`Views/HudWindow.axaml(.cs)`、`Animations/HudAnimations.cs` 的主题化改造
- `Settings/`、`Customization/` 下的配色与文案统一
- 设置窗新增「角色主题」卡片与下拉框

`Settings/AppSettings.cs` 新增 `HudThemeId` 字段，`SettingsManager.Normalize` 里做了合法性校验，旧版 settings.json 可直接加载。

---

## 下载

| 架构 | 文件 | 适用 |
| :-- | :-- | :-- |
| x64 | `SCHALE-Charge-Plus-v0.1.2-ba-win-x64-portable.zip` | 绝大多数 Windows 10 / 11 设备 |
| x86 | `SCHALE-Charge-Plus-v0.1.2-ba-win-x86-portable.zip` | 32 位系统 |
| ARM64 | `SCHALE-Charge-Plus-v0.1.2-ba-win-arm64-portable.zip` | Windows on ARM |

解压后直接运行 `EndfieldChargePlus.exe`。无需安装，无需另装 .NET 运行时。

附带 `SHA256SUMS.txt`：

```powershell
Get-FileHash .\SCHALE-Charge-Plus-v0.1.2-ba-win-x64-portable.zip -Algorithm SHA256
```

---

## 首次使用

1. 运行 `EndfieldChargePlus.exe`，程序驻留系统托盘。
2. 托盘菜单 → **设置**。
3. 「显示与位置」里选目标显示器、HUD 位置、显示方式，顺便挑一个角色主题。
4. 「HUD 显示内容」里选内置方案或新建自己的 HUD，点**预览**看效果。
5. 点**保存并应用**。配置自动保存，重启后继续生效。

默认使用「系统 - 内存」方案；界面语言跟随系统，中文系统走简体中文，其他走 English。

---

## 验证情况

仓库自带回归套件在本机（Windows 10.0.19041 / .NET SDK 8.0.426）的运行结果：

| 套件 | 结果 |
| :-- | :-- |
| `--appearance` | **PASS** 33 项外观与语言检查 |
| `--layers` | **PASS** 104 项多 HUD 层级检查 |
| `--localization` | 资源层 **PASS**：21 个语区 × 1728 条消息；GUI 层在 `ar-SA` 的 `SaveBtn` 可见性断言处中止 |
| `--rendering` | 在透明像素断言处中止 |

后两项不是本次改造引入的。在未改动的上游基线 `GlacierGlimmer/zmd-charge-plus` 上跑同一套件、同一台机器，中止点完全相同（`--localization` 都是 `ar-SA SaveBtn`，`--rendering` 都是 `Real desktop pixels outside both HUD capsules stay transparent`）。这类断言依赖具体桌面会话与 DPI 环境，本机不成立。这里明写，而不是只列通过的那几条。

三种架构的 EXE 都已实际启动，日志记录 `SCHALE Charge Plus process started. Version=0.1.2`。

---

## 已知限制

- 仅 Windows。Linux / macOS / Android 由上游独立仓库维护。
- 微软商店版由上游维护，本仓库只出便携版。
- 没有代码签名证书，首次运行可能触发 SmartScreen 提示。

---

## 许可与声明

本项目新增的代码与主题改造采用 [MIT License](https://github.com/chenmoulaile/schale-charge-plus/blob/master/LICENSE)。上游来源与署名完整保留在 [NOTICE.md](https://github.com/chenmoulaile/schale-charge-plus/blob/master/NOTICE.md)。

**Nunito Sans** 采用 SIL Open Font License 1.1，授权全文见 `Assets/Fonts/NunitoSans-OFL.txt`。

**《蔚蓝档案》的角色、名称与美术资产**权利归 NEXON Games 及发行方所有。本项目为非官方社区作品，与其无隶属、授权或背书关系；仓库内不含游戏原始美术资源，只含由此采样得出的配色数值和自绘的光环几何。

数据采集范围见 [PRIVACY.md](https://github.com/chenmoulaile/schale-charge-plus/blob/master/PRIVACY.md)。

---

## 上游

- 原始 HUD：[QinAnze/zmd-charge](https://github.com/QinAnze/zmd-charge)
- 二次开发基线：[GlacierGlimmer/zmd-charge-plus](https://github.com/GlacierGlimmer/zmd-charge-plus)
