namespace EndfieldChargePlus;

internal static class ProductInfo
{
    internal const string LinuxName = "SCHALE Charge Plus For Linux";
    internal static string Name => OperatingSystem.IsLinux() ? LinuxName : "SCHALE Charge Plus";
    internal static string Brand(string value)
    {
        if (!OperatingSystem.IsLinux()) return value;
        return value.Replace(LinuxName, "SCHALE Charge Plus", StringComparison.Ordinal)
            .Replace("SCHALE Charge Plus", LinuxName, StringComparison.Ordinal)
            .Replace("SCHALE CHARGE PLUS FOR LINUX", "SCHALE CHARGE PLUS", StringComparison.Ordinal)
            .Replace("SCHALE CHARGE PLUS", "SCHALE CHARGE PLUS FOR LINUX", StringComparison.Ordinal);
    }
}
