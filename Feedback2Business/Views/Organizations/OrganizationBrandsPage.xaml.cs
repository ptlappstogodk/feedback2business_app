using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Organizations;

public partial class OrganizationBrandsPage : ContentPage
{
    public OrganizationBrandsPage(OrganizationBrandsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


