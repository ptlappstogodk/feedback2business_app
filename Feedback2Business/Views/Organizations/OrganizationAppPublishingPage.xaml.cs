using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Organizations;

public partial class OrganizationAppPublishingPage : ContentPage
{
    public OrganizationAppPublishingPage(OrganizationAppPublishingViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
