using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class TemplatesViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<TemplateModel> Templates { get; } = new();

    public ICommand OpretTemplateCommand { get; }

    public TemplatesViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var item in data.GetTemplates())
            Templates.Add(item);

        OpretTemplateCommand = new RelayCommand(async () => await OpretTemplateAsync());
    }

    private async Task OpretTemplateAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret skabelon", "Indtast skabelonnavn:", "Næste", "Annuller", "Navn");
        if (string.IsNullOrWhiteSpace(name)) return;

        var description = await Application.Current!.MainPage!.DisplayPromptAsync("Opret skabelon", "Indtast beskrivelse:", "Gem", "Annuller", "Beskrivelse");
        if (description == null) return;

        var template = new TemplateModel
        {
            Name = name.Trim(),
            Type = "Survey",
            Description = description.Trim(),
            UpdatedAt = DateTime.Now
        };

        _data.CreateTemplate(template);
        Templates.Add(template);
    }
}


