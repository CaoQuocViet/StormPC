using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace StormPC.Core.Helpers;

/// <summary>
/// Lớp hỗ trợ sắp xếp DataGrid với nhiều cột
/// </summary>
public static class DataGridSortHelper
{
    /// <summary>
    /// Sắp xếp một tập hợp dựa trên tên thuộc tính và hướng sắp xếp
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của các phần tử trong tập hợp</typeparam>
    /// <param name="items">Tập hợp gốc cần sắp xếp</param>
    /// <param name="sortProperties">Danh sách tên thuộc tính để sắp xếp theo</param>
    /// <param name="sortDirections">Danh sách hướng sắp xếp tương ứng với thuộc tính</param>
    /// <returns>Một ObservableCollection mới đã được sắp xếp</returns>
    public static ObservableCollection<T> ApplySort<T>(
        IEnumerable<T>? items, 
        IList<string> sortProperties, 
        IList<ListSortDirection> sortDirections)
    {
        if (items == null || !sortProperties.Any() || sortProperties.Count != sortDirections.Count)
            return new ObservableCollection<T>(items ?? Array.Empty<T>());

        // Dùng LINQ để sắp xếp collection
        IOrderedEnumerable<T>? sortedItems = null;
        var firstSort = true;

        for (var i = 0; i < sortProperties.Count; i++)
        {
            var propertyName = sortProperties[i];
            var direction = sortDirections[i];
            
            if (string.IsNullOrEmpty(propertyName))
                continue;

            // Lấy thông tin thuộc tính cho tên thuộc tính
            PropertyInfo? prop = typeof(T).GetProperty(propertyName);
            if (prop == null)
                continue;

            if (firstSort)
            {
                // Sắp xếp lần đầu
                sortedItems = direction == ListSortDirection.Ascending
                    ? items.OrderBy(item => GetPropertyValue(item, prop))
                    : items.OrderByDescending(item => GetPropertyValue(item, prop));
                
                firstSort = false;
            }
            else
            {
                // Sắp xếp tiếp theo (ThenBy)
                if (sortedItems != null)
                {
                    sortedItems = direction == ListSortDirection.Ascending
                        ? sortedItems.ThenBy(item => GetPropertyValue(item, prop))
                        : sortedItems.ThenByDescending(item => GetPropertyValue(item, prop));
                }
            }
        }

        return new ObservableCollection<T>(sortedItems ?? items);
    }

    /// <summary>
    /// Lấy giá trị thuộc tính một cách an toàn, xử lý cả giá trị null
    /// </summary>
    private static object GetPropertyValue<T>(T item, PropertyInfo property)
    {
        try
        {
            return property.GetValue(item) ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
