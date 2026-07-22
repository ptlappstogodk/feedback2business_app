using System.Collections.ObjectModel;

namespace Feedback2Business.ViewModels;

public class SettingsAppViewModel : ObservableObject
{
    private bool _offlineEnabled = true;
    private bool _autoSyncEnabled = true;
    private bool _requireLocationAtStart = true;
    private bool _useLocationOnObservations = false;
    private bool _compressImages = true;
    private string _selectedMaxFileSize = "2 MB";
    private bool _showSurveyProgress = true;
    private bool _allowSkipForward = false;

    public bool OfflineEnabled
    {
        get => _offlineEnabled;
        set => SetProperty(ref _offlineEnabled, value);
    }

    public bool AutoSyncEnabled
    {
        get => _autoSyncEnabled;
        set => SetProperty(ref _autoSyncEnabled, value);
    }

    public bool RequireLocationAtStart
    {
        get => _requireLocationAtStart;
        set => SetProperty(ref _requireLocationAtStart, value);
    }

    public bool UseLocationOnObservations
    {
        get => _useLocationOnObservations;
        set => SetProperty(ref _useLocationOnObservations, value);
    }

    public bool CompressImages
    {
        get => _compressImages;
        set => SetProperty(ref _compressImages, value);
    }

    public ObservableCollection<string> MaxFileSizeOptions { get; } = new() { "1 MB", "2 MB", "5 MB", "10 MB" };

    public string SelectedMaxFileSize
    {
        get => _selectedMaxFileSize;
        set => SetProperty(ref _selectedMaxFileSize, value);
    }

    public bool ShowSurveyProgress
    {
        get => _showSurveyProgress;
        set => SetProperty(ref _showSurveyProgress, value);
    }

    public bool AllowSkipForward
    {
        get => _allowSkipForward;
        set => SetProperty(ref _allowSkipForward, value);
    }
}


