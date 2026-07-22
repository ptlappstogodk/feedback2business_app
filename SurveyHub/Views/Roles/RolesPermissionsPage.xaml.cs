using SurveyHub.ViewModels;

namespace SurveyHub.Views.Roles;

public partial class RolesPermissionsPage : ContentPage
{
    public RolesPermissionsPage(RolesPermissionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


