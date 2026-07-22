using SurveyHub.ViewModels;

namespace SurveyHub.Views.ActivityLog;

public partial class ActivityLogPage : ContentPage
{
    public ActivityLogPage(ActivityLogViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}