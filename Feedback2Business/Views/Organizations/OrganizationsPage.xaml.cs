using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Organizations;

public partial class OrganizationsPage : ContentPage
{
    public OrganizationsPage(OrganizationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


