using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationBrandsPage : ContentPage
{
    public OrganizationBrandsPage(OrganizationBrandsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


