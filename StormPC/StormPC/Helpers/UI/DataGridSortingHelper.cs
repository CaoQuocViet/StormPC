using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.Core.Helpers;
using Windows.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Linq;

namespace StormPC.Helpers.UI
{
    /// <summary>
    /// Helper class for handling DataGrid sorting in the UI layer
    /// </summary>
    public static class DataGridSortingHelper
    {
        // Flag to track multi-column sorting mode - always initialize to false
        private static bool _isMultiColumnSortMode = false;
        
        // Keep track of the sorting order (which column was clicked first, second, etc.)
        private static List<string> _sortOrder = new List<string>();
        
        // Dictionary to track which columns in each DataGrid are sorted
        private static Dictionary<DataGrid, List<DataGridColumn>> _sortedColumns = 
            new Dictionary<DataGrid, List<DataGridColumn>>();

        /// <summary>
        /// Enable or disable multi-column sort mode
        /// </summary>
        public static void SetMultiColumnSortMode(bool enable)
        {
            // Set multi-column sort mode
            _isMultiColumnSortMode = enable;
            
            // When disabling, clear the sort order
            if (!enable)
            {
                _sortOrder.Clear();
            }
        }
        
        /// <summary>
        /// Gets the current multi-column sort mode
        /// </summary>
        public static bool IsMultiColumnSortMode()
        {
            return _isMultiColumnSortMode;
        }

        /// <summary>
        /// Process sorting for DataGrid control using Tag property instead of SortMemberPath
        /// </summary>
        /// <typeparam name="T">Type of items in the collection</typeparam>
        /// <param name="dataGrid">The DataGrid that triggered the sorting</param>
        /// <param name="e">Sort event arguments</param>
        /// <param name="originalItems">The original unsorted items</param>
        /// <returns>A new sorted ObservableCollection</returns>
        public static ObservableCollection<T> ProcessSorting<T>(
            DataGrid dataGrid, 
            DataGridColumnEventArgs e, 
            IEnumerable<T> originalItems)
        {
            var column = e.Column;
            
            // Get property name from Tag property
            string? propertyName = column.Tag?.ToString();
            if (string.IsNullOrEmpty(propertyName))
                return new ObservableCollection<T>(originalItems);
                
            // Determine the new sort direction
            ListSortDirection direction;
            if (column.SortDirection == null || column.SortDirection == DataGridSortDirection.Descending)
                direction = ListSortDirection.Ascending;
            else
                direction = ListSortDirection.Descending;
                
            // Update the column's sort direction
            column.SortDirection = direction == ListSortDirection.Ascending 
                ? DataGridSortDirection.Ascending 
                : DataGridSortDirection.Descending;

            // Add this DataGrid to tracking dictionary if not present
            if (!_sortedColumns.ContainsKey(dataGrid))
            {
                _sortedColumns[dataGrid] = new List<DataGridColumn>();
            }
            
            // Add this column to the sorted columns for this DataGrid if not already there
            if (!_sortedColumns[dataGrid].Contains(column))
            {
                _sortedColumns[dataGrid].Add(column);
            }

            if (_isMultiColumnSortMode)
            {
                // *** Multi-column sort mode ***
                
                // If this column is already in the sort order, keep it in the same position
                if (!_sortOrder.Contains(propertyName))
                {
                    // Add this column to the sort order
                    _sortOrder.Add(propertyName);
                }
                
                // Maintain sort order based on click sequence
                var sortProperties = new List<string>();
                var sortDirections = new List<ListSortDirection>();
                
                // Go through columns in the order they were clicked
                foreach (var propName in _sortOrder)
                {
                    // Find the column with this property name
                    var col = dataGrid.Columns.FirstOrDefault(c => c.Tag?.ToString() == propName);
                    if (col != null && col.SortDirection != null)
                    {
                        sortProperties.Add(propName);
                        sortDirections.Add(col.SortDirection == DataGridSortDirection.Ascending 
                            ? ListSortDirection.Ascending 
                            : ListSortDirection.Descending);
                    }
                }
                
                // Apply sorting using the helper
                return DataGridSortHelper.ApplySort(originalItems, sortProperties, sortDirections);
            }
            else
            {
                // *** Single-column sort mode ***
                
                // Clear all other columns' sort directions
                foreach (var col in dataGrid.Columns)
                {
                    if (col != column && col.SortDirection != null)
                    {
                        col.SortDirection = null;
                    }
                }
                
                // Remove other columns from tracking for this DataGrid
                _sortedColumns[dataGrid].Clear();
                _sortedColumns[dataGrid].Add(column);
                
                // Update sort order for single column
                _sortOrder.Clear();
                _sortOrder.Add(propertyName);
                
                // Apply single-column sorting
                return DataGridSortHelper.ApplySort(
                    originalItems, 
                    new List<string> { propertyName }, 
                    new List<ListSortDirection> { direction });
            }
        }
        
        /// <summary>
        /// Forces single-column mode on a DataGrid, keeping only the first sorted column
        /// </summary>
        public static void ForceSingleColumnMode(DataGrid dataGrid)
        {
            // Find the first column with a sort direction
            var primarySortColumn = dataGrid.Columns.FirstOrDefault(col => col.SortDirection != null);
            
            // Clear all other column sort directions
            foreach (var column in dataGrid.Columns)
            {
                if (column != primarySortColumn)
                {
                    column.SortDirection = null;
                }
            }
            
            // Update tracking
            if (primarySortColumn != null)
            {
                if (!_sortedColumns.ContainsKey(dataGrid))
                {
                    _sortedColumns[dataGrid] = new List<DataGridColumn>();
                }
                else
                {
                    _sortedColumns[dataGrid].Clear();
                }
                
                _sortedColumns[dataGrid].Add(primarySortColumn);
                
                // Update sort order for single column
                _sortOrder.Clear();
                string? propName = primarySortColumn.Tag?.ToString();
                if (!string.IsNullOrEmpty(propName))
                {
                    _sortOrder.Add(propName);
                }
            }
        }
    }
} 