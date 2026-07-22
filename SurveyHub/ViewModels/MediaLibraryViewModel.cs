using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class MediaLibraryViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> FileTypes { get; } = new() { "Alle", "Billeder", "PDF", "DOCX", "XLSX" };
    public ObservableCollection<MediaItemModel> MediaItems { get; } = new();

    public MediaLibraryViewModel(IMockDataService data)
    {
        foreach (var item in data.GetMediaItems())
            MediaItems.Add(item);
    }
}


