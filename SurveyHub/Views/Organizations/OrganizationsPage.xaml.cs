using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationsPage : ContentPage
{
    public OrganizationsPage(OrganizationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


