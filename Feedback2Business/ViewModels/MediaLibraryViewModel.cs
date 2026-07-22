using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

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


