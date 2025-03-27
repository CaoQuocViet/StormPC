using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace StormPC.Core.Helpers
{
    /// <summary>
    /// A helper class for sorting DataGrid with multiple columns
    /// </summary>
    public static class DataGridSortHelper
    {
        /// <summary>
        /// Sorts a collection based on property names and sort directions
        /// </summary>
        /// <typeparam name="T">The type of items in the collection</typeparam>
        /// <param name="items">The original collection to sort</param>
        /// <param name="sortProperties">List of property names to sort by</param>
        /// <param name="sortDirections">List of sort directions corresponding to properties</param>
        /// <returns>A new sorted ObservableCollection</returns>
        public static ObservableCollection<T> ApplySort<T>(
            IEnumerable<T> items, 
            IList<string> sortProperties, 
            IList<ListSortDirection> sortDirections)
        {
            if (items == null || !sortProperties.Any() || sortProperties.Count != sortDirections.Count)
                return new ObservableCollection<T>(items);

            // Use LINQ to sort the collection
            IOrderedEnumerable<T> sortedItems = null;
            bool firstSort = true;

            for (int i = 0; i < sortProperties.Count; i++)
            {
                var propertyName = sortProperties[i];
                var direction = sortDirections[i];
                
                if (string.IsNullOrEmpty(propertyName))
                    continue;

                // Get the property info for the property name
                PropertyInfo prop = typeof(T).GetProperty(propertyName);
                if (prop == null)
                    continue;

                if (firstSort)
                {
                    // First sort
                    sortedItems = direction == ListSortDirection.Ascending
                        ? items.OrderBy(item => GetPropertyValue(item, prop))
                        : items.OrderByDescending(item => GetPropertyValue(item, prop));
                    
                    firstSort = false;
                }
                else
                {
                    // Subsequent sorts (ThenBy)
                    sortedItems = direction == ListSortDirection.Ascending
                        ? sortedItems.ThenBy(item => GetPropertyValue(item, prop))
                        : sortedItems.ThenByDescending(item => GetPropertyValue(item, prop));
                }
            }

            return new ObservableCollection<T>(sortedItems ?? items);
        }

        /// <summary>
        /// Gets a property value safely handling null values
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
} 