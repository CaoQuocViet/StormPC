using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.ActivityLog;

namespace StormPC.Views.ActivityLog;

public sealed partial class ActivityLogPage : Page
{
    public ActivityLogViewModel ViewModel { get; }

    public ActivityLogPage()
    {
        ViewModel = App.GetService<ActivityLogViewModel>();
        this.InitializeComponent();
        this.DataContext = ViewModel;
    }
} 