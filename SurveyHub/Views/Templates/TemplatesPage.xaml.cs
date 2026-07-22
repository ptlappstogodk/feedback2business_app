using SurveyHub.ViewModels;

namespace SurveyHub.Views.Templates;

public partial class TemplatesPage : ContentPage
{
    public TemplatesPage(TemplatesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


