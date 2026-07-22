using SurveyHub.ViewModels;

namespace SurveyHub.Views.Media;

public partial class MediaLibraryPage : ContentPage
{
    public MediaLibraryPage(MediaLibraryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


