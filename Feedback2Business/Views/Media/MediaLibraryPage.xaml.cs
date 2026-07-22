using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Media;

public partial class MediaLibraryPage : ContentPage
{
    public MediaLibraryPage(MediaLibraryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


