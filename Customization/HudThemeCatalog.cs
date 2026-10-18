using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace EndfieldChargePlus.Customization;

/// <summary>
/// One selectable HUD skin. Every colour here was sampled from that character's own
/// official sprite or key art — the halo is treated as a property of the character
/// (constant across outfits) while the panel, plate and accent follow the outfit,
/// which is how Blue Archive itself separates a girl from her alts.
/// </summary>
public sealed record HudThemeDefinition
{
    public required string Id { get; init; }
    public required string NameZh { get; init; }
    public required string NameEn { get; init; }

    /// <summary>Pill fill, running top-left to bottom-right.</summary>
    public required Color PillTop { get; init; }
    public required Color PillMid { get; init; }
    public required Color PillBottom { get; init; }

    /// <summary>Hairline border around the pill.</summary>
    public required Color PillEdge { get; init; }

    /// <summary>Outer glow drop-shadow, same hue as the edge.</summary>
    public required Color PillGlow { get; init; }

    public required Color IconPlate { get; init; }
    public required Color GlyphDark { get; init; }
    public required Color Accent { get; init; }

    /// <summary>Halo stroke. Character-constant, so it can differ from <see cref="Accent"/>.</summary>
    public required Color HaloAccent { get; init; }

    public required Color HaloGlow { get; init; }
    public required Color BadgeDark { get; init; }
    public required Color Ripple { get; init; }

    /// <summary>
    /// Halo contour authored in a 30x9 box with Stretch=None, so the numbers are the
    /// rendered pixel positions. Stroke-only: no fill rule to fight.
    /// </summary>
    public required string HaloPath { get; init; }

    public string Display => $"{NameZh}  ·  {NameEn}";
}

/// <summary>
/// The theme roster. Kept in code rather than XAML because the halo contour is a
/// geometry string each entry owns; the applier pushes the rest into window resources.
/// </summary>
public static class HudThemeCatalog
{
    public const string DefaultId = "schale";

    // A halo ring flattened into perspective: wide, shallow, closed.
    private const string HaloRingPath =
        "M1 4.2 A14 3.4 0 1 1 29 4.2 A14 3.4 0 1 1 1 4.2 Z";

    // Hina's regalia: the same flattened ring, but crowned with seven tapering spikes.
    private const string HaloCrownPath =
        "M3.4 5 A11.6 2.9 0 1 1 26.6 5 A11.6 2.9 0 1 1 3.4 5 Z " +
        "M6 3 L4.6 0.4 M9.4 2.4 L8.6 0 M12.4 2.2 L12 0 M15 2.1 L15 0 " +
        "M17.6 2.2 L18 0 M20.6 2.4 L21.4 0 M24 3 L25.4 0.4";

    // Kisaki's is not a ring at all: a single brushed crescent that curls back on
    // itself, with a companion droplet drifting below — the cloud-scroll in her art.
    private const string HaloCrescentPath =
        "M2.2 7.4 A13.5 5.2 0 0 1 26.4 3.6 A4.2 3.4 0 0 1 21.6 1.2 " +
        "M7.6 8 A1 1 0 1 1 9.6 8 A1 1 0 1 1 7.6 8 Z";

    public static readonly IReadOnlyList<HudThemeDefinition> All = new[]
    {
        new HudThemeDefinition
        {
            Id = DefaultId,
            NameZh = "夏莱（默认）",
            NameEn = "SCHALE",
            PillTop = Color.Parse("#1D3557"),
            PillMid = Color.Parse("#152740"),
            PillBottom = Color.Parse("#0B1828"),
            PillEdge = Color.Parse("#9800D9FA"),
            PillGlow = Color.Parse("#3D00D9FA"),
            IconPlate = Color.Parse("#00D9FA"),
            GlyphDark = Color.Parse("#0A1626"),
            Accent = Color.Parse("#00D9FA"),
            HaloAccent = Color.Parse("#00D9FA"),
            HaloGlow = Color.Parse("#7FE9FF"),
            BadgeDark = Color.Parse("#102A46"),
            Ripple = Color.Parse("#2E7FB4"),
            HaloPath = HaloRingPath,
        },
        new HudThemeDefinition
        {
            Id = "hina",
            NameZh = "空崎日奈",
            NameEn = "Sorasaki Hina",
            PillTop = Color.Parse("#3E1249"),
            PillMid = Color.Parse("#2A0D33"),
            PillBottom = Color.Parse("#17061D"),
            PillEdge = Color.Parse("#B3E448F6"),
            PillGlow = Color.Parse("#4DE448F6"),
            IconPlate = Color.Parse("#E448F6"),
            GlyphDark = Color.Parse("#1A0520"),
            Accent = Color.Parse("#E448F6"),
            HaloAccent = Color.Parse("#E448F6"),
            HaloGlow = Color.Parse("#F7B6FF"),
            BadgeDark = Color.Parse("#2C0E38"),
            Ripple = Color.Parse("#8E32B0"),
            HaloPath = HaloCrownPath,
        },
        new HudThemeDefinition
        {
            Id = "hina-swimsuit",
            NameZh = "空崎日奈（泳装）",
            NameEn = "Hina (Swimsuit)",
            PillTop = Color.Parse("#0F4E7C"),
            PillMid = Color.Parse("#0B3A5C"),
            PillBottom = Color.Parse("#05202F"),
            PillEdge = Color.Parse("#B33CB4FA"),
            PillGlow = Color.Parse("#4D3CB4FA"),
            IconPlate = Color.Parse("#3CB4FA"),
            GlyphDark = Color.Parse("#04202F"),
            Accent = Color.Parse("#3CB4FA"),
            HaloAccent = Color.Parse("#E448F6"),
            HaloGlow = Color.Parse("#F7B6FF"),
            BadgeDark = Color.Parse("#0A3352"),
            Ripple = Color.Parse("#1E7FB4"),
            HaloPath = HaloCrownPath,
        },
        new HudThemeDefinition
        {
            Id = "hina-dress",
            NameZh = "空崎日奈（礼服）",
            NameEn = "Hina (Dress)",
            PillTop = Color.Parse("#382A6B"),
            PillMid = Color.Parse("#291E51"),
            PillBottom = Color.Parse("#150E2C"),
            PillEdge = Color.Parse("#B3E47EEA"),
            PillGlow = Color.Parse("#4DE47EEA"),
            IconPlate = Color.Parse("#E47EEA"),
            GlyphDark = Color.Parse("#1C0A22"),
            Accent = Color.Parse("#E47EEA"),
            HaloAccent = Color.Parse("#E448F6"),
            HaloGlow = Color.Parse("#F7C4FB"),
            BadgeDark = Color.Parse("#241A4A"),
            Ripple = Color.Parse("#6B5FB0"),
            HaloPath = HaloCrownPath,
        },
        new HudThemeDefinition
        {
            Id = "hina-nightwear",
            NameZh = "空崎日奈（睡衣）",
            NameEn = "Hina (Nightwear)",
            PillTop = Color.Parse("#3C2C1C"),
            PillMid = Color.Parse("#2B1F13"),
            PillBottom = Color.Parse("#19110A"),
            PillEdge = Color.Parse("#B3FCEA96"),
            PillGlow = Color.Parse("#4DFCEA96"),
            IconPlate = Color.Parse("#FCEA96"),
            GlyphDark = Color.Parse("#2A1E08"),
            Accent = Color.Parse("#FCEA96"),
            HaloAccent = Color.Parse("#E47EEA"),
            HaloGlow = Color.Parse("#FFF0C6"),
            BadgeDark = Color.Parse("#33260F"),
            Ripple = Color.Parse("#B08A3C"),
            HaloPath = HaloCrownPath,
        },
        new HudThemeDefinition
        {
            Id = "kisaki",
            NameZh = "龙华妃咲",
            NameEn = "Ryuge Kisaki",
            PillTop = Color.Parse("#20265A"),
            PillMid = Color.Parse("#171B41"),
            PillBottom = Color.Parse("#0C0E28"),
            PillEdge = Color.Parse("#B366B4D2"),
            PillGlow = Color.Parse("#4D66B4D2"),
            IconPlate = Color.Parse("#66B4D2"),
            GlyphDark = Color.Parse("#0A1424"),
            Accent = Color.Parse("#66B4D2"),
            HaloAccent = Color.Parse("#EAEAF0"),
            HaloGlow = Color.Parse("#FFFFFF"),
            BadgeDark = Color.Parse("#161C3E"),
            Ripple = Color.Parse("#3A5A8C"),
            HaloPath = HaloCrescentPath,
        },
        new HudThemeDefinition
        {
            Id = "kisaki-swimsuit",
            NameZh = "龙华妃咲（泳装）",
            NameEn = "Kisaki (Swimsuit)",
            PillTop = Color.Parse("#3C3C58"),
            PillMid = Color.Parse("#2B2B42"),
            PillBottom = Color.Parse("#17172B"),
            PillEdge = Color.Parse("#B3DCAAAA"),
            PillGlow = Color.Parse("#4DDCAAAA"),
            IconPlate = Color.Parse("#E8B8B8"),
            GlyphDark = Color.Parse("#241820"),
            Accent = Color.Parse("#DCAAAA"),
            HaloAccent = Color.Parse("#EAEAF0"),
            HaloGlow = Color.Parse("#FFFFFF"),
            BadgeDark = Color.Parse("#262640"),
            Ripple = Color.Parse("#7A6A8A"),
            HaloPath = HaloCrescentPath,
        },
        new HudThemeDefinition
        {
            Id = "kisaki-kindergarten",
            NameZh = "龙华妃咲（妃妃）",
            NameEn = "Kisaki (Kindergarten)",
            PillTop = Color.Parse("#56411F"),
            PillMid = Color.Parse("#3C2D15"),
            PillBottom = Color.Parse("#221909"),
            PillEdge = Color.Parse("#B3FCEA96"),
            PillGlow = Color.Parse("#4DFCEA96"),
            IconPlate = Color.Parse("#FCEA96"),
            GlyphDark = Color.Parse("#2A1E08"),
            Accent = Color.Parse("#FABEC8"),
            HaloAccent = Color.Parse("#FFFFFF"),
            HaloGlow = Color.Parse("#FFF6DC"),
            BadgeDark = Color.Parse("#3A2C10"),
            Ripple = Color.Parse("#C08A4A"),
            HaloPath = HaloCrescentPath,
        },
    };

    public static HudThemeDefinition Get(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var match = All.FirstOrDefault(t => string.Equals(t.Id, id.Trim(), StringComparison.OrdinalIgnoreCase));
            if (match is not null) return match;
        }
        return All[0];
    }

    /// <summary>Coerces an unknown or empty id to a real theme id.</summary>
    public static string NormalizeId(string? id) => Get(id).Id;
}

/// <summary>
/// Pushes a theme into a window's resource dictionary. The HUD reads every themed
/// colour through DynamicResource, so writing the keys here recolours it in place —
/// no rebuild of the visual tree, no re-run of the intro animation.
/// </summary>
public static class HudThemeApplier
{
    public static void Apply(Window window, HudThemeDefinition theme)
    {
        var resources = window.Resources;
        resources["Hud.PillFill"] = BuildPillFill(theme);
        resources["Hud.PillEdge"] = theme.PillEdge;
        resources["Hud.IconPlate"] = theme.IconPlate;
        resources["Hud.BoltDark"] = theme.GlyphDark;
        resources["Hud.Accent"] = theme.Accent;
        resources["Hud.HaloAccent"] = theme.HaloAccent;
        resources["Hud.HaloGlow"] = theme.HaloGlow;
        resources["Hud.BadgeDark"] = theme.BadgeDark;
        resources["Hud.Ripple"] = theme.Ripple;

        if (window.FindControl<Control>("Pill") is { } pill)
        {
            // BorderBrush and BoxShadow are not DynamicResource-friendly (the latter is a
            // struct), so they are written straight onto the control.
            pill.SetValue(Border.BorderBrushProperty, new SolidColorBrush(theme.PillEdge));
            pill.SetValue(Border.BoxShadowProperty, new BoxShadows(new BoxShadow
            {
                OffsetX = 0,
                OffsetY = 0,
                Blur = 22,
                Spread = 0,
                Color = theme.PillGlow,
            }));
        }

        if (Geometry.Parse(theme.HaloPath) is { } halo)
        {
            foreach (var name in new[] { "HaloGlowPath", "HaloRing", "HaloHighlight" })
                if (window.FindControl<Avalonia.Controls.Shapes.Path>(name) is { } path)
                    path.Data = halo;
        }
    }

    private static LinearGradientBrush BuildPillFill(HudThemeDefinition theme)
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(1, 1, RelativeUnit.Relative),
        };
        brush.GradientStops.Add(new GradientStop(theme.PillTop, 0));
        brush.GradientStops.Add(new GradientStop(theme.PillMid, 0.55));
        brush.GradientStops.Add(new GradientStop(theme.PillBottom, 1));
        return brush;
    }
}
