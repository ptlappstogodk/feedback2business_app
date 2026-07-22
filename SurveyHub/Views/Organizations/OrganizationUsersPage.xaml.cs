using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationUsersPage : ContentPage
{
    public OrganizationUsersPage(OrganizationUsersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


