using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class TemplatesViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<TemplateModel> Templates { get; } = new();

    public TemplatesViewModel(IMockDataService data)
    {
        foreach (var item in data.GetTemplates())
            Templates.Add(item);
    }
}


