using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class ActivityLogViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> Actions { get; } = new() { "Alle", "Opdateret", "Oprettet", "Uploadet", "Eksporteret" };
    public ObservableCollection<string> Users { get; } = new() { "Alle", "Anders Kirk", "Maria Jensen", "Lars Petersen" };
    public ObservableCollection<string> Periods { get; } = new() { "7 dage", "30 dage", "90 dage" };

    public ObservableCollection<ActivityEventModel> ActivityEvents { get; } = new();

    public ActivityLogViewModel(IMockDataService data)
    {
        foreach (var item in data.GetActivityEvents())
            ActivityEvents.Add(item);
    }
}


