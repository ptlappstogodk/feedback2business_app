using SurveyHub.ViewModels;

namespace SurveyHub.Views.Variables;

public partial class VariablesPage : ContentPage
{
    public VariablesPage(VariablesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


