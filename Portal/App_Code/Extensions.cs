using System;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class Extensions
{
    public static DateTime GetVerionDate(this Version version)
    {
        var initialDate = new DateTime(2000, 1, 1);
        var versionDate = initialDate
            .AddDays(version.Build)
            .AddSeconds(version.Revision * 2);
        return versionDate;
    }
}