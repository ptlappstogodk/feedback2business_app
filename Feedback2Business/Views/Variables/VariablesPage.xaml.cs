using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Variables;

public partial class VariablesPage : ContentPage
{
    public VariablesPage(VariablesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


