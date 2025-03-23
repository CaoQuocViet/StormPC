using System.Text.RegularExpressions;

namespace StormPC.Core.Helpers;

public static class CurrencyHelper
{
    public static string FormatCurrency(decimal amount)
    {
        return amount.ToString("#,##0").Replace(",", ".");
    }
} 