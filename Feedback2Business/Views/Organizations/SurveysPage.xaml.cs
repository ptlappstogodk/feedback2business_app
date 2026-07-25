using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Organizations;

public partial class SurveysPage : ContentPage
{
    public SurveysPage(SurveysViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


