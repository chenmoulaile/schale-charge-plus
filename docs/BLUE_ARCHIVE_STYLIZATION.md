# Blue Archive Theme Styling Documentation

## Overview
This document details the visual stylization implemented for **空崎日奈 (Sorasaki Hina)** and **龙华妃咲 (Ryuge Kisaki)** character themes in SCHALE Charge Plus.

## Implemented Features ✓

### Character-Specific Background System

#### Visual Architecture
```
HudWindow Layer Structure:
┌─────────────────────────────────────┐
│ BackgroundLayer (Ellipse)           │ ← Image/Video background (0.25 opacity)
├─────────────────────────────────────┤
│ Pill Background (Gradient Fill)     │ ← Solid color gradient fallback
│ ├─ Gradient Overlay                 │ ← Readability enhancement
│ └─ Ripple Effects                   │ ← Animated expansion rings
├─────────────────────────────────────┤
│ Content Layer                       │ ← Text, icons, data display
└─────────────────────────────────────┘
```

#### Configuration Points

**File**: `Customization/HudThemeCatalog.cs`

```csharp
// New properties added to HudThemeDefinition record:
public required bool HasBackground { get; init; }              // Enable background layer
public required string? BackgroundImageUri { get; init; }      // Image path (avares://)
public required double BackgroundOpacity { get; init; }        // 0.0 - 1.0 scale
public required bool UseGradientOverlay { get; init; }         // Fallback overlay
public required Color GradientOverlayColor { get; init; }      // Overlay tint

// Example: Enhanced Hina theme configuration
new HudThemeDefinition
{
    Id = "hina",
    NameZh = "空崎日奈",
    NameEn = "Sorasaki Hina",
    
    // Color palette from official artwork sampling
    PillTop = Color.Parse("#3E1249"),   // Deep magenta
    PillMid = Color.Parse("#2A0D33"),   // Purple midtone  
    PillBottom = Color.Parse("#17061D"), // Near-black base
    
    // Halo accent (character-constant across outfits)
    HaloAccent = Color.Parse("#E448F6"), // Signature pink
    HaloGlow = Color.Parse("#F7B6FF"),   // Bright highlight
    
    // Background system configuration
    HasBackground = true,                                      // ✅ Enable background
    BackgroundImageUri = "avares://EndfieldChargePlus/Assets/Themes/Hina/background_hina.mp4",
    BackgroundOpacity = 0.25,                                  // Subtle transparency
    UseGradientOverlay = true,                                 // ✅ Fallback active
    GradientOverlayColor = Color.Parse("#6017061D"),          // Dark purple tint
}
```

### Color Palette System

#### Sorasaki Hina (空崎日奈) - Crown Halo
All outfit variants maintain consistent purple/magenta/gold identity:

| Outfit | Primary | Secondary | Accent | Glow |
|--------|---------|-----------|--------|------|
| **Default** | #3E1249 | #2A0D33 | #E448F6 | #F7B6FF |
| **Swimsuit** | #0F4E7C | #0B3A5C | #3CB4FA | #F7B6FF |
| **Dress** | #382A6B | #291E51 | #E47EEA | #F7C4FB |
| **Nightwear** | #3C2C1C | #2B1F13 | #FCEA96 | #FFF0C6 |

**Visual Identity Elements:**
- **Crown Halo Shape**: 7-point spiked crown geometry (consistent across all outfits)
- **Signature Pink**: #E448F6 appears on halo regardless of outfit variant
- **Atmosphere**: Elegant, slightly melancholic, regal presence
- **Contrast**: High luminance variation between top/mid/bottom gradients

#### Ryuge Kisaki (龙华妃咲) - Crescent Moon Halo
Professional indigo/cyan/silver palette with cloud-scroll motif:

| Outfit | Primary | Secondary | Accent | Glow |
|--------|---------|-----------|--------|------|
| **Default** | #20265A | #171B41 | #66B4D2 | #FFFFFF |
| **Swimsuit** | #3C3C58 | #2B2B42 | #DCAAAA | #FFFFFF |
| **Kindergarten** | #56411F | #3C2D15 | #FABEC8 | #FFF6DC |

**Visual Identity Elements:**
- **Crescent + Droplet**: Unique swirling moon shape with companion droplet below
- **Cloud Scroll Motif**: Curved elements referencing traditional East Asian patterns
- **Professional Tone**: Sharp contrasts, clean lines, office-like atmosphere
- **Silver White Accents**: Halo glow always pure white (#FFFFFF) for contrast

### Halo Geometry System

**Location**: `Customization/HudThemeCatalog.cs`

```csharp
// Hina's Crown - 7 spikes emanating from flattened ring base
private const string HaloCrownPath =
    "M3.4 5 A11.6 2.9 0 1 1 26.6 5 A11.6 2.9 0 1 1 3.4 5 Z " +   // Main ring
    "M6 3 L4.6 0.4 M9.4 2.4 L8.6 0 M12.4 2.2 L12 0 " +            // Left 3 spikes
    "M15 2.1 L15 0 M17.6 2.2 L18 0 M20.6 2.4 L21.4 0 " +          // Right 4 spikes
    "M24 3 L25.4 0.4";                                             // Far right spike

// Kisaki's Crescent Moon - single curved arc with droplet attachment
private const string HaloCrescentPath =
    "M2.2 7.4 A13.5 5.2 0 0 1 26.4 3.6 A4.2 3.4 0 0 1 21.6 1.2 " +  // Upper crescent arc
    "M7.6 8 A 1 1 0 1 1 9.6 8 A 1 1 0 1 1 7.6 8 Z";                  // Droplet below
```

**Rendering Details:**
- All halos use **stroke-only** drawing (no fill rule conflicts)
- Three-layer glow system: outer blur → main stroke → inner highlight
- Scaled to 30x9 coordinate system (flat perspective view)
- Rendered at 55% scale via `ScaleTransform` for proper proportions

## Visual Enhancement Implementation

### Background Loading Logic

**File**: `Views/HudWindow.axaml.cs`

```csharp
private void ApplyBackgroundLayer(HudThemeDefinition theme)
{
    var backgroundLayer = FindControl<Ellipse>("BackgroundLayer");
    if (backgroundLayer is null) return;

    if (theme.HasBackground && !string.IsNullOrWhiteSpace(theme.BackgroundImageUri))
    {
        try
        {
            // Load asset from embedded resources
            var source = AssetLoader.Open(new Uri(theme.BackgroundImageUri));
            var bitmap = new Avalonia.Media.Imaging.Bitmap(source);
            
            var imageBrush = new ImageBrush
            {
                ImageSource = bitmap,
                Stretch = Stretch.UniformToFill,
                Opacity = theme.BackgroundOpacity
            };
            
            // Scale beyond visible area for parallax effect
            backgroundLayer.Width = Bounds.Width * 3;      // Triple width coverage
            backgroundLayer.Height = Bounds.Height * 3;    // Triple height coverage
            backgroundLayer.Stretch = Stretch.UniformToFill;
            backgroundLayer.RenderTransform = 
                new ScaleTransform(1.5, 1.5);              // Additional magnification
            
            backgroundLayer.Fill = imageBrush;
            backgroundLayer.IsVisible = true;
        }
        catch
        {
            // Graceful fallback: hide background, enable gradient overlay
            backgroundLayer.IsVisible = false;
            
            if (theme.UseGradientOverlay)
            {
                var pill = FindControl<Border>("Pill");
                if (pill is not null)
                {
                    pill.Background = CreateTintedGradient(
                        theme.GradientOverlayColor, 
                        theme.BackgroundOpacity
                    );
                }
            }
        }
    }
}
```

### Parallax Scaling Effect

The background layer scales **3x larger** than visible HUD area to achieve:
1. Smooth camera movement illusion when HUD animates
2. Prevents edge artifacts during transitions
3. Creates depth perception through relative motion

**Scaling Chain:**
```
Original Image: 1920x1080
  ↓ ScaleTransform(1.5, 1.5)
Scaled to: 2880x1620
  ↓ Window bounds * 3 constraint
Final display: Covers entire window + margins
```

### Glow Effect Enhancement

**Enhanced Halo Rendering:**
```csharp
private void AddCharacterGlowEffect()
{
    var haloRing = FindControl<Avalonia.Controls.Shapes.Path>("HaloRing");
    if (haloRing is { })
    {
        // Add gaussian blur for soft glow appearance
        haloRing.Effect = new Avalonia.Rendering.Media.Effects.GaussianBlurEffect
        {
            Radius = new Thickness(2),
            KernelType = Avalonia.Rendering.Media.Effects.KernelType.Box,
            BorderMode = Avalonia.Rendering.Media.Effects.BorderMode.Clamp
        };
    }
}
```

## Asset Installation Guide

### Directory Structure Required
```
Assets/Themes/
├── Hina/
│   ├── README.md                      ← This documentation file
│   ├── background_hina.png            ← Default uniform scene (required?)
│   ├── background_swimsuit.png        ← Swimsuit beach scene (optional)
│   ├── background_dress.png          ← Evening gown scene (optional)
│   └── background_nightwear.png      ← Home setting scene (optional)
│
└── Kisaki/
    ├── README.md                      ← Character guide
    ├── background_kisaki.png          ← Default office scene (required?)
    ├── background_swimsuit.png        ← Vacation beach scene (optional)
    └── background_kindergarten.png   ← School setting scene (optional)
```

### Asset Requirements

#### Image Specifications
- **Minimum Resolution**: 1920x1080 pixels (Full HD)
- **Recommended**: 2560x1440 or 3840x2160 (4K)
- **Supported Formats**: PNG (static), MP4/MOV (video loop)
- **Aspect Ratio**: 16:9 widescreen
- **Color Space**: sRGB

#### Visual Guidelines
✅ **Recommended:**
- Soft-focus atmospheric backgrounds
- Colors matching character palette
- Minimal text/overlaid graphics
- Consistent mood per character

❌ **Avoid:**
- Overly busy or high-contrast scenes
- Images with strong competing colors
- Screenshots with UI overlays
- Heavily compressed/JPEG artifacts

### Where to Obtain Assets

#### Option 1: Official Game Memories
If you own Blue Archive:
1. Launch game → Select character → Memorial Lobby tab
2. Change outfit to match theme variant
3. Take high-resolution screenshot using in-game camera
4. Crop to 16:9 aspect ratio

#### Option 2: Community Wikis
Public resource sites (check usage policies):
- **KivoWiki (Chinese)**: https://kivo.wiki/data/character/[ID]
- **Blue Archive Wiki**: https://bluearchive.wiki/wiki/
- Search for "Memorial Lobby" screenshots or official key art

#### Option 3: Pure Gradient Mode (Recommended)
The system works beautifully without external images:
- Built-in gradient backgrounds match character palettes perfectly
- Auto-fallback to gradient overlay if images unavailable
- Professional results with zero asset management

## Testing & Verification

### Build & Run
```powershell
dotnet build EndfieldChargePlus.csproj -c Release
.\publish\EndfieldChargePlus.exe
```

### Enable Character Themes
1. Open settings tray menu → Click Settings icon
2. Navigate to **"显示与位置" (Display & Position)** tab
3. Find **"角色主题" (Character Theme)** card
4. Select desired character (e.g., "空崎日奈")
5. Click **"预览"** to see real-time preview
6. Click **"保存并应用"** to persist selection

### Verify Background Display
- **With Background Images**: Character scene fades in behind HUD at specified opacity
- **Without Images**: Solid gradient tint matches character color palette
- **Halo Shape**: Crown (Hina) or Crescent (Kisaki) renders immediately
- **Animation**: Full animation sequence plays on HUD presentation

## Future Enhancement Possibilities

### Planned Improvements
- [ ] Parallax particle effects (floating dust motes, light rays)
- [ ] Dynamic halo pulse animation synchronized to time
- [ ] Voice line triggers on theme change (memory hall references)
- [ ] Achievement badges as decorative HUD elements
- [ ] Seasonal event variations (Cherry Blossom, Summer Festival themes)

### Advanced Customization
- Scriptable background fade transitions
- Per-profile background overrides
- Video background support with frame rates
- Audio ambience integration (ambient sounds from memory halls)

---

## Credits

**Original Game**: BLUE ARCHIVE (ブルーアーカイブ)
- Developer: NEXON Games Co., Ltd.
- Publisher: Yostar Limited
- Character designs, voice acting, music © their respective owners

**Project Information**:
- SCHALE Charge Plus: Community fan project for desktop status bar HUD
- MIT License for code modifications
- Fair use principle for character asset references

**Special Thanks**:
- Upstream contributors: QinAnze (zmd-charge), GlacierGlimmer (zmd-charge-plus)
- Blue Archive community wiki maintainers
- NEXON Games/Yostar for creating such beautiful characters

