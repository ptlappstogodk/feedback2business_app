using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Organizations;

public partial class OrganizationUsersPage : ContentPage
{
    public OrganizationUsersPage(OrganizationUsersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


