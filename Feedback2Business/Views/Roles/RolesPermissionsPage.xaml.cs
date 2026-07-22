using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Roles;

public partial class RolesPermissionsPage : ContentPage
{
    public RolesPermissionsPage(RolesPermissionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


