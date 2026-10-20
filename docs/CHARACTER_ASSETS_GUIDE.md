# Blue Archive Character Theme Assets Guide

## Overview
This guide explains how to obtain and install character-specific background assets for **Hina (空崎日奈)** and **Kisaki (龙华妃咲)** themes in SCHALE Charge Plus.

## Current Status

### What's Already Implemented ✓
- ✅ Background system infrastructure for all character themes
- ✅ Gradient-based fallback backgrounds (no external images needed)
- ✅ Halo shape configuration (Crown for Hina, Crescent Moon for Kisaki)
- ✅ Color-matched gradient overlays
- ✅ Parallax background scaling effects
- ✅ Glow effect enhancements

### What You Need to Add
Character-themed background images/videos stored in the theme directories:
```
Assets/Themes/Hina/
  - background_hina.png       (Default uniform scene)
  - background_swimsuit.png   (Swimsuit version)
  - background_dress.png      (Dress/evening gown)
  - background_nightwear.png  (Nightwear/casual)

Assets/Themes/Kisaki/
  - background_kisaki.png     (Default uniform scene)
  - background_swimsuit.png   (Swimsuit version)
  - background_kindergarten.png (Kindergarten version)
```

## How to Obtain Background Images

### Option 1: Official Game Memory Scenes (Recommended)
Download from official or fan Wiki sources:

#### For Hina (空崎日奈):
1. Visit **Blue Archive Wiki**: https://bluearchive.wiki/wiki/Sorasaki_Hina
2. Look for "Memorial Lobby" screenshots in different outfits
3. Search for official artwork with her signature purple/pink color scheme
4. Recommended image size: **1920x1080** or higher

**Key visual elements to look for:**
- **Default uniform**: Purple/magenta tones with crown halo reference
- **Swimsuit**: Ocean blue/cyan beach scenes  
- **Dress**: Elegant evening atmosphere with violet highlights
- **Nightwear**: Warm golden/yellow home setting

#### For Kisaki (龙华妃咲):
1. Visit **Blue Archive Wiki**: https://bluearchive.wiki/wiki/Ryuge_Kisaki
2. Find "Memorial Lobby" or character art with crescent moon motif
3. Look for deep indigo, cyan/silver color schemes

**Key visual elements to look for:**
- **Default uniform**: Deep indigo office/study setting with cloud-scroll crescent
- **Swimsuit**: Summer beach/vacation with coral and soft pink accents
- **Kindergarten**: Bright school environment with yellow/orange lighting

### Option 2: Fan Art & Community Resources
Some reputable sources:
- **KivoWiki**: https://kivo.wiki/data/character/[ID] (Chinese community wiki)
- **Archive.gg**: Character galleries
- **Reddit r/bluearchive**: Official asset discussions

### Option 3: In-Game Screenshots
If you own the game:
1. Use in-game camera function during memory hall visits
2. Select character → Memorial Lobby tab
3. Take high-resolution screenshots matching outfit themes

## Image Requirements

### Technical Specifications
- **Resolution**: Minimum 1920x1080, recommended 2560x1440 or 4K
- **Format**: PNG (lossless quality) or MP4/MOV (video loops)
- **Color Space**: sRGB for consistency
- **Aspect Ratio**: 16:9 (widescreen)

### Quality Guidelines
- ✅ Clean, high-quality source (avoid heavily compressed images)
- ✅ Soft-focus backgrounds that don't distract from HUD data
- ✅ Colors complementing character palette
- ❌ Overly busy backgrounds (text-heavy, chaotic patterns)
- ❌ Images with strong contrast that might interfere with HUD visibility

## Installation Steps

### Step 1: Create Directory Structure
The directories are already created:
```bash
mkdir -p Assets/Themes/Hina
mkdir -p Assets/Themes/Kisaki
```

### Step 2: Place Background Images
Copy your downloaded images with these exact filenames:
```
Assets/Themes/Hina/background_hina.png         ← Downloaded Hina default scene
Assets/Themes/Hina/background_swimsuit.png     ← Swimsuit beach scene
Assets/Themes/Hina/background_dress.png        ← Evening gown scene
Assets/Themes/Hina/background_nightwear.png    ← Home setting scene

Assets/Themes/Kisaki/background_kisaki.png     ← Downloaded Kisaki default scene
Assets/Themes/Kisaki/background_swimsuit.png   ← Swimsuit beach scene
Assets/Themes/Kisaki/background_kindergarten.png ← School setting scene
```

### Step 3: Verify Asset Loading
The system automatically loads backgrounds when:
1. Theme is selected in Settings → Display & Position → Character Theme
2. HUD becomes visible (persistent mode or mouse-hover activation)

Backgrounds appear with:
- Default opacity: **0.25** (adjustable per-theme in `HudThemeCatalog.cs`)
- Scaled 1.5x beyond visible area for smooth parallax effect
- Semi-transparent gradient overlay for text readability

## Configuration Reference

### Opacity Controls
Edit `Customization/HudThemeCatalog.cs` to adjust background appearance:

```csharp
// Example: Increase Hina default background visibility
new HudThemeDefinition
{
    // ... other properties ...
    HasBackground = true,
    BackgroundImageUri = "avares://EndfieldChargePlus/Assets/Themes/Hina/background_hina.mp4",
    BackgroundOpacity = 0.35,  // Higher = more visible
    UseGradientOverlay = true,
    GradientOverlayColor = Color.Parse("#6017061D"),
}
```

### Fallback Behavior
If background image fails to load:
1. System gracefully falls back to gradient overlay
2. Character-specific tint preserved (`GradientOverlayColor`)
3. Pill gradient remains functional

## Visual Enhancement Tips

### For Best Results:
1. **Match mood to outfit**: Choose background scenes reflecting character personality
   - Hina default: Calm, elegant, slightly melancholic atmosphere
   - Kisaki default: Professional, mysterious, office-like setting

2. **Consider readability**: Background should enhance, not compete with HUD data
   - Avoid bright colors directly behind numeric displays
   - Soft blur effects work better than sharp details

3. **Consistent color grading**: All variants of same character should share aesthetic tone
   - Hina's four outfits all use purples, pinks, golds
   - Kisaki's three outfits all use indigos, cyans, silvers

## Credits & Legal Notice

All Blue Archive visual assets are property of:
- **Original Creator**: NEXON Games Co., Ltd.
- **Publisher**: Yostar Limited
- **Game Title**: BLUE ARCHIVE (ブルーアーカイブ)

This project uses fair use principles for educational/community demonstration purposes only. All character names, designs, and associated intellectual property remain copyright of their respective owners.

---

## Alternative: Pure Gradient Approach

If obtaining external images proves difficult, the current implementation works perfectly with **gradient-only backgrounds**. The solid-color gradients already match each character's official color palette sampled from artwork:

### Hina Themes Work Perfectly With:
- **Purple→Violet→Dark** vertical gradients (default)
- **Ocean blue→Deep sea→Abyss** (swimsuit)
- **Royal purple→Indigo→Night** (dress)
- **Warm amber→Gold→Brown** (nightwear)

### Kisaki Themes Work Perfectly With:
- **Deep indigo→Midnight→Ink black** (default)
- **Blue-violet→Steel blue→Navy** (swimsuit)
- **Golden brown→Caramel→Chocolate** (kindergarten)

The gradient system provides beautiful, professional results without requiring external image files. Background layer will auto-fallback if images aren't present.

---

**SCHALE Charge Plus** · A Blue Archive themed desktop status bar HUD
