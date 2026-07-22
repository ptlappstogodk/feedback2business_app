using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Templates;

public partial class TemplatesPage : ContentPage
{
    public TemplatesPage(TemplatesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


