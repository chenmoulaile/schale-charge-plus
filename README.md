# SCHALE Charge Plus · 蔚蓝档案风格状态栏 HUD

> 把桌面状态栏 HUD 换成《蔚蓝档案》的视觉语言：深灰底盘、标志性青色、角色光环。
> 内置 8 套可切换角色皮肤，光环形状跟随角色、配色跟随服装。

**简体中文** · [English](README.en.md)　｜　**Windows 10 / 11 · x64 / x86 / ARM64**

![八套主题一览](docs/screenshots/themes-all.png)

---

## 这是什么

SCHALE Charge Plus（**ECP**）是一个常驻桌面的状态栏 HUD：CPU、GPU、内存、磁盘、网络、时间、DeepSeek API 余额、自定义 HTTP/JSON 数据，都能组合成一条横贯屏幕顶部的胶囊，随时唤出、随时收起、按队列自动轮播。

它由 [QinAnze/zmd-charge](https://github.com/QinAnze/zmd-charge) 经 [GlacierGlimmer/zmd-charge-plus](https://github.com/GlacierGlimmer/zmd-charge-plus) 二次开发而来。**本仓库只做一件事：把整个界面重绘成《蔚蓝档案》的风格，并加上按角色形态切换的主题层。** 功能、变量库、方案系统、动画时间线全部沿用上游，没有删改。

---

## 主题

设置 → **显示与位置 → 角色主题**。切换即预览，点「保存并应用」后持久化。

| 主题 | 配色来源 | 光环形状 |
| :-- | :-- | :-- |
| 夏莱（默认）SCHALE | `#212529` 底盘 + `#00D9FA` 青 | 素扁椭圆环 |
| 空崎日奈 Hina | 立绘采样：光环品红 `#E448F6`、深紫 `#3C0048` | 尖刺冠冕 |
| 日奈（泳装）Swimsuit | `#1EAAF0` 天蓝 + `#D2E6F0` 淡蓝 + `#F0B4D2` 粉 | 尖刺冠冕 |
| 日奈（礼服）Dress | `#E47EEA` 淡紫粉 + `#463C78` 靛紫 | 尖刺冠冕 |
| 日奈（睡衣）Nightwear | `#FCEA96` 奶黄 + `#F0B4C8` 粉 + `#241A2E` 暗 | 尖刺冠冕 |
| 龙华妃咲 Kisaki | `#1E244E` 深靛 + `#66AECC` 青蓝 + `#CCD2D2` 银白 | 云气卷曲月牙 |
| 妃咲（泳装）Swimsuit | `#464664` 蓝紫 + `#DCAAAA` 珊瑚 + `#EAEAF0` 近白 | 云气卷曲月牙 |
| 妃咲（妃妃）Kindergarten | `#FCEA96` 小黄帽 + `#FABEC8` 樱粉 + `#E4A854` 橙 | 云气卷曲月牙 |

配色不是套同一色相旋转出来的。7 套角色皮肤的关键色都取自对应形态官方立绘的像素统计（高饱和色众数 + 高光/暗部采样），所以泳装是泳装的蓝、睡衣是睡衣的奶黄，而不是把紫色调暗三次。

光环形状绑角色，不绑服装——日奈四个形态共用尖刺冠冕，妃咲三个形态共用云气月牙。这是原作里的设定，也是肉眼一眼能区分角色的那个符号。

![八套光环并列](docs/screenshots/halos.png)

---

## 视觉改动清单

- **配色**：以 SchaleDB 官方暗色主题的设计令牌为准。`#212529` 深灰底、`#00D9FA` 标志青、`#006BFF` 蓝强调，替换原版终末地黄绿体系。
- **光环**：HUD 左侧图标盘上方加一圈扁椭圆光环，三层描边叠出发光感。完整动画与简洁动画两条时间线都会点亮它。
- **胶囊**：`LinearGradientBrush` 深海蓝渐变 + 青色描边 + 外发光，随主题实时重新着色。
- **图标**：应用图标与托盘图标重绘为「青色光环 + 深色角色盘」。ICO 覆盖 16 / 20 / 24 / 32 / 40 / 48 / 64 / 128 / 256 px 九档，最小档也能看出环孔。
- **字体**：HUD 数字与标题改用 **Nunito Sans**（SchaleDB 的界面字体），从上游可变字体实例化成 400/500/600/700 四个静态字重，中文回退到系统雅黑 / 苹方。
- **控件**：覆盖了 Avalonia Fluent 的 `SystemControl*` / `ComboBox*` / `ListBox*` 资源键，下拉框、列表、按钮不再露出默认暗色暖灰。语言选择窗的列表底色单独修过。
- **品牌**：显示名统一为 SCHALE Charge Plus；默认标签文案 `/// SCHALE MONITOR`。

程序集名、命名空间、`avares://` 资源路径、可执行文件名都保持 `EndfieldChargePlus` 不变——这样旧的配置、开机启动项和更新通道不会因为改名而断掉。

![改动前后对比](docs/screenshots/compare-hud.png)

---

## 下载与运行

前往本仓库 [Releases](https://github.com/chenmoulaile/schale-charge-plus/releases) 下载对应架构的便携版 ZIP：

| 架构 | 文件 | 适用 |
| :-- | :-- | :-- |
| x64 | `SCHALE-Charge-Plus-v0.1.2-ba-win-x64-portable.zip` | 绝大多数 Windows 10 / 11 电脑 |
| x86 | `SCHALE-Charge-Plus-v0.1.2-ba-win-x86-portable.zip` | 32 位系统 |
| ARM64 | `SCHALE-Charge-Plus-v0.1.2-ba-win-arm64-portable.zip` | Windows on ARM |

解压后直接运行 `EndfieldChargePlus.exe`，无需安装，也无需另装 .NET 运行时。下载页附带 `SHA256SUMS.txt`，可用它校验：

```powershell
Get-FileHash .\SCHALE-Charge-Plus-v0.1.2-ba-win-x64-portable.zip -Algorithm SHA256
```

**首次使用：**

1. 运行 `EndfieldChargePlus.exe`，程序驻留系统托盘。
2. 托盘菜单 → **设置**。
3. 「显示与位置」里选目标显示器、HUD 位置、显示方式，顺便挑一个角色主题。
4. 「HUD 显示内容」里选内置方案或新建自己的 HUD，点**预览**看效果。
5. 点**保存并应用**。配置自动保存，重启后继续生效。

首次启动默认使用「系统 - 内存」方案；界面语言跟随系统，中文系统走简体中文，其他走 English。

> 微软商店、Linux、macOS、Android 的发行版由上游仓库维护，不在本仓库分发。本仓库只出 Windows 便携版。

---

## 自行编译

需要 Windows 与 [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)。

```powershell
# 编译
dotnet build EndfieldChargePlus.csproj -c Release

# 打包 x64 自包含单文件
./scripts/package-windows.ps1 -Runtime win-x64
```

把 `win-x64` 换成 `win-x86` 或 `win-arm64` 即可打另外两种架构。产物落在 `publish/<runtime>-portable/`，目录里只有单个 EXE。

帧率采集器按系统架构自动选择内置的 x64 或 ARM64 版本。

---

## 功能一览

功能与上游完全一致，这里只列会用到的那部分。

| 功能 | 说明 |
| :-- | :-- |
| **两种显示方式** | 关闭「一直显示」时按事件或鼠标唤出后自动收回；开启后持续更新当前方案 |
| **置顶 / 桌面层** | 持续显示可选始终置顶或桌面层（不置顶）；HUD 点击穿透，不挡操作 |
| **鼠标唤出** | 鼠标移到目标屏幕顶部中央，临时显示当前非电池方案 |
| **多显示器与位置** | 指定显示器，九宫格预设或自定义 X/Y，可调缩放与不透明度 |
| **方案与动画** | 内置电池 / CPU / GPU / 内存 / 磁盘 / 网络 / 时间 / DeepSeek 等方案；完整与简洁两种动画 |
| **自动轮播** | 按自定义队列循环切换，另设轮播间隔与切换动画 |
| **变量库** | 内置变量、17 类按实际盘符生成的磁盘模板、按实际传感器生成的风扇变量 |
| **自由组合** | 标题、主副数值、百分比圆环、左右图标均可按变量模板配置；支持格式化、表达式、条件颜色规则 |
| **网络包探测** | 指定 IPv4 / IPv6 / 域名及端口，用 ICMP、TCP 或 UDP 探测 |
| **DeepSeek API** | 填自己的 Key，显示余额、高峰 / 低谷、时段进度与倒计时 |
| **HTTP / JSON 数据源** | 配置地址、Header、刷新间隔与字段映射，把自有接口接进 HUD |

```text
{cpu.usage|0}                 CPU 使用率
{memory.used_bytes|gb:1}      已用内存（GB）
{network.download_bps|speed}  网络下载速度
{deepseek.balance|0.00}       DeepSeek API 余额
```

变量不是模拟数值，读不到时会显示不可用，不会伪造一个 0。完整 Key 与说明在软件内「变量库」查看。

---

## 验证情况

发布前在本机（Windows 10.0.19041 / .NET SDK 8.0.426）跑过仓库自带的回归套件：

| 套件 | 结果 |
| :-- | :-- |
| `--appearance` | **PASS** 33 项外观与语言检查 |
| `--layers` | **PASS** 104 项多 HUD 层级检查 |
| `--localization` | 资源层 **PASS**：21 个语区 × 1728 条消息全部通过；GUI 层在 `ar-SA` 的 `SaveBtn` 可见性断言处中止 |
| `--rendering` | 在透明像素断言处中止 |

后两项不是本次改造引入的：在未改动的上游基线 `GlacierGlimmer/zmd-charge-plus` 上跑同一套件、同一台机器，中止点完全相同（`--localization` 都是 `ar-SA SaveBtn`，`--rendering` 都是 `Real desktop pixels outside both HUD capsules stay transparent`）。这类断言依赖具体桌面会话与 DPI 环境，本机不成立。为免误会，这里明写出来，而不是只贴通过的那几条。

三种架构的 EXE 都已实际启动验证，日志记录 `SCHALE Charge Plus process started. Version=0.1.2`。

---

## 来源、许可与声明

- 上游 HUD：[QinAnze/zmd-charge](https://github.com/QinAnze/zmd-charge)，原作者 **QinAnze**。
- 二次开发基线：[GlacierGlimmer/zmd-charge-plus](https://github.com/GlacierGlimmer/zmd-charge-plus)，作者 **GlacierGlimmer_冰川雪貓**。
- 本项目新增的代码与主题改造采用 [MIT License](LICENSE)。上游来源与署名在 [NOTICE.md](NOTICE.md) 中完整保留。
- **Nunito Sans** 采用 SIL Open Font License 1.1，授权全文见 `Assets/Fonts/NunitoSans-OFL.txt`。
- **《蔚蓝档案》的角色、名称与美术资产**权利归 NEXON Games 及发行方所有。本项目为非官方社区作品，与其无隶属、授权或背书关系；仓库内不含游戏原始美术资源，只含由此采样得出的配色数值和自绘的光环几何。

数据处理说明见 [PRIVACY.md](PRIVACY.md)。

---

## 找回上游

想回到未改色的原版，或需要 Linux / macOS / Android 版本：

- 未改色的 Windows 原版：[GlacierGlimmer/zmd-charge-plus](https://github.com/GlacierGlimmer/zmd-charge-plus)
- Linux：[zmd-charge-plus-for-linux](https://github.com/GlacierGlimmer/zmd-charge-plus-for-linux)
- macOS：[zmd-charge-plus-for-macos](https://github.com/GlacierGlimmer/zmd-charge-plus-for-macos)
- Android：[zmd-charge-plus-for-android](https://github.com/GlacierGlimmer/zmd-charge-plus-for-android)
