using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class VariablesViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    public ObservableCollection<VariableModel> Variables { get; } = new();

    public ICommand OpretVariableCommand { get; }

    public VariablesViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var item in data.GetVariables())
            Variables.Add(item);

        OpretVariableCommand = new RelayCommand(async () => await OpretVariableAsync());
    }

    private async Task OpretVariableAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret variabel", "Indtast variabelnavn:", "Næste", "Annuller", "Navn");
        if (string.IsNullOrWhiteSpace(name)) return;

        var key = await Application.Current!.MainPage!.DisplayPromptAsync("Opret variabel", "Indtast variabel nøgle (Key):", "Næste", "Annuller", "Key");
        if (string.IsNullOrWhiteSpace(key)) return;

        var defaultValue = await Application.Current!.MainPage!.DisplayPromptAsync("Opret variabel", "Indtast standardværdi:", "Gem", "Annuller", "Standardværdi");
        if (defaultValue == null) return;

        var variable = new VariableModel
        {
            Name = name.Trim(),
            Key = key.Trim(),
            Type = "Tekst",
            DefaultValue = defaultValue.Trim(),
            UsageCount = 0
        };

        _data.CreateVariable(variable);
        Variables.Add(variable);
    }
}


