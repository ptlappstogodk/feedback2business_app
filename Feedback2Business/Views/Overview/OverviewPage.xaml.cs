using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Overview;

public partial class OverviewPage : ContentPage
{
    public OverviewPage(OverviewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
