using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

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


