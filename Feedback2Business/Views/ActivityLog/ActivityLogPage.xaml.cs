using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.ActivityLog;

public partial class ActivityLogPage : ContentPage
{
    public ActivityLogPage(ActivityLogViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}