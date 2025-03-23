using System;

namespace StormPC.Core.Helpers;

public static class NumberHelper
{
    public static string FormatPercent(decimal number)
    {
        return Math.Floor(number).ToString("0");
    }
} 