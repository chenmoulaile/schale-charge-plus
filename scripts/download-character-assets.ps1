# Character Background Assets Downloader
# Downloads Blue Archive character assets for SCHALE Charge Plus

$HinaBackgrounds = @{
    "background_hina.png" = "https://webkoi.com/ba/Hina_Sorasaki_1920x1080.jpg"
    "background_swimsuit.png" = "https://webkoi.com/ba/Hina_Swimsuit_1920x1080.jpg"
    "background_dress.png" = "https://webkoi.com/ba/Hina_Dress_1920x1080.jpg"
    "background_nightwear.png" = "https://webkoi.com/ba/Hina_Nightwear_1920x1080.jpg"
}

$KisakiBackgrounds = @{
    "background_kisaki.png" = "https://webkoi.com/ba/Kisaki_Ryuge_1920x1080.jpg"
    "background_swimsuit.png" = "https://webkoi.com/ba/Kisaki_Swimsuit_1920x1080.jpg"
    "background_kindergarten.png" = "https://webkoi.com/ba/Kisaki_Children_1920x1080.jpg"
}

$OutputDir = Join-Path $PSScriptRoot "..\Assets\Themes"

Write-Host "=== Downloading Character Background Assets ===" -ForegroundColor Cyan

foreach ($character in @("Hina", "Kisaki")) {
    $charDir = Join-Path $OutputDir $character
    
    if ($character -eq "Hina") {
        foreach (($filename, $url) in $HinaBackgrounds.GetEnumerator()) {
            $path = Join-Path $charDir $filename
            Write-Host "Downloading $character: $filename..." -ForegroundColor Yellow
            
            try {
                Invoke-WebRequest -Uri $url -OutFile $path -UseBasicParsing
                Write-Host "✓ Downloaded: $filename" -ForegroundColor Green
            } catch {
                Write-Host "✗ Failed: $filename - $_" -ForegroundColor Red
            }
        }
    } elseif ($character -eq "Kisaki") {
        foreach (($filename, $url) in $KisakiBackgrounds.GetEnumerator()) {
            $path = Join-Path $charDir $filename
            Write-Host "Downloading $character: $filename..." -ForegroundColor Yellow
            
            try {
                Invoke-WebRequest -Uri $url -OutFile $path -UseBasicParsing
                Write-Host "✓ Downloaded: $filename" -ForegroundColor Green
            } catch {
                Write-Host "✗ Failed: $filename - $_" -ForegroundColor Red
            }
        }
    }
    
    # Create index.txt describing assets
    Add-Content -Path (Join-Path $charDir "index.txt") -Value @"
$character Character Background Assets
=====================================

This directory contains character-specific background images for the HUD theme system.

Files:
$((Get-ChildItem $charDir *.png | ForEach-Object { "- $($_.Name)"}) -join "`n")

Usage:
These images are loaded as background layers when the corresponding character theme is selected.
They should be high-quality wallpapers or memory hall scenes featuring the character.

Note:
All image rights belong to their respective owners (NEXON Games / Yostar).
This project uses fair use for educational/community purposes only.
"@
}

Write-Host "`n=== Asset Download Complete ===" -ForegroundColor Cyan
Write-Host "Assets saved to: $OutputDir" -ForegroundColor Gray

# Generate README with credits
Add-Content -Path (Join-Path $OutputDir "README.md") -Value @"
# Character Theme Assets

## Overview
This directory contains background assets for Blue Archive character themes in SCHALE Charge Plus.

## Character Directories

### Hina (空崎日奈)
- **Default** - Sorasaki Hina standard uniform with crown halo
- **Swimsuit** - Summer beach version with light blue theme  
- **Dress** - Elegant evening gown variant
- **Nightwear** - Cozy home clothes with warm tones

### Kisaki (龙华妃咲)
- **Default** - Ryuge Kisaki standard form with crescent moon halo
- **Swimsuit** - Seasonal swimwear design
- **Kindergarten** - Cute kindergarten version with yellow cap motif

## Visual Enhancements

Each character theme includes:
- **Memory Scene Background**: Atmospheric background from in-game memories or official art
- **Gradient Overlay**: Semi-transparent color tint matching the character's palette
- **Parallax Effect**: Background scales larger than visible area for depth illusion
- **Dynamic Halo**: Character-specific halo shape that stays constant across outfits

## Credits

All visual assets are property of:
- **Original Creator**: NEXON Games
- **Publisher**: Yostar Limited
- **Game**: Blue Archive (ブルーアーカイブ -Blue Archive-)

This project is a fan-made modification and has no affiliation with the original creators.
Asset usage falls under fair use for community/demo purposes.

## License

Original game assets: © NEXON Games / Yostar
Code modifications: MIT License

---
SCHALE Charge Plus - A Blue Archive themed status bar HUD
"@

Write-Host "`nGenerated documentation files." -ForegroundColor Green
