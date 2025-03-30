using Microsoft.UI.Xaml.Data;
using System;

namespace StormPC.Helpers;

public class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Nếu targetType là string, trả về chuỗi định dạng
        if (targetType == typeof(string))
        {
            if (value is DateTime dateTime)
            {
                if (dateTime == DateTime.MinValue)
                    return "Chưa bán";
                return Format(dateTime);
            }
            if (value is DateTimeOffset dateOffset)
            {
                if (dateOffset.DateTime == DateTime.MinValue)
                    return "Chưa bán";
                return Format(dateOffset.DateTime);
            }
            return "Chưa bán";
        }

        // Nếu value là null hoặc DateTime.MinValue, trả về null
        if (value == null || 
            (value is DateTime dt && dt == DateTime.MinValue) ||
            (value is DateTimeOffset dto && dto.DateTime == DateTime.MinValue))
            return null;

        // Chuyển đổi sang DateTimeOffset cho CalendarDatePicker
        if (targetType == typeof(DateTimeOffset) || targetType == typeof(DateTimeOffset?))
        {
            if (value is DateTime dateTime)
                return new DateTimeOffset(dateTime);
            if (value is DateTimeOffset dateOffset)
                return dateOffset;
        }

        // Chuyển đổi sang DateTime cho ViewModel
        if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
        {
            if (value is DateTime dateTime)
                return dateTime;
            if (value is DateTimeOffset dateOffset)
                return dateOffset.DateTime;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        // Nếu value là null, trả về null
        if (value == null)
            return null;

        // Chuyển đổi từ DateTimeOffset về DateTime cho ViewModel
        if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
        {
            if (value is DateTime dateTime)
                return dateTime == DateTime.MinValue ? null : dateTime;
            if (value is DateTimeOffset dateOffset)
                return dateOffset.DateTime == DateTime.MinValue ? null : dateOffset.DateTime;
        }

        // Chuyển đổi từ DateTime sang DateTimeOffset cho UI
        if (targetType == typeof(DateTimeOffset) || targetType == typeof(DateTimeOffset?))
        {
            if (value is DateTime dateTime)
                return dateTime == DateTime.MinValue ? null : new DateTimeOffset(dateTime);
            if (value is DateTimeOffset dateOffset)
                return dateOffset.DateTime == DateTime.MinValue ? null : dateOffset;
        }

        return null;
    }

    public static string Format(DateTime value)
    {
        return value.ToString("dd/MM/yyyy");
    }
} 