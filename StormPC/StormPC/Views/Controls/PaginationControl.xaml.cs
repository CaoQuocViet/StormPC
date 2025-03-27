using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace StormPC.Controls
{
    public sealed partial class PaginationControl : UserControl
    {
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(int), typeof(PaginationControl),
                new PropertyMetadata(1, OnCurrentPageChanged));

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register(nameof(TotalPages), typeof(int), typeof(PaginationControl),
                new PropertyMetadata(1, OnTotalPagesChanged));

        public event EventHandler<int> PageChanged;

        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public int TotalPages
        {
            get => (int)GetValue(TotalPagesProperty);
            set => SetValue(TotalPagesProperty, value);
        }

        public PaginationControl()
        {
            this.InitializeComponent();
            UpdateButtonStates();
        }

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PaginationControl)d;
            control.UpdateButtonStates();
        }

        private static void OnTotalPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PaginationControl)d;
            control.UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            PreviousButton.IsEnabled = CurrentPage > 1;
            NextButton.IsEnabled = CurrentPage < TotalPages;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                PageChanged?.Invoke(this, CurrentPage);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                PageChanged?.Invoke(this, CurrentPage);
            }
        }

        private void CurrentPageBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = !args.NewText.All(c => char.IsDigit(c));
        }

        private void CurrentPageBox_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                var textBox = (TextBox)sender;
                if (int.TryParse(textBox.Text, out int page))
                {
                    if (page >= 1 && page <= TotalPages)
                    {
                        CurrentPage = page;
                        PageChanged?.Invoke(this, CurrentPage);
                    }
                    else
                    {
                        textBox.Text = CurrentPage.ToString();
                    }
                }
            }
        }
    }
} 