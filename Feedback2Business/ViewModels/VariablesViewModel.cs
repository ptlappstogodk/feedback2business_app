using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class VariablesViewModel : ObservableObject
{
    public ObservableCollection<VariableModel> Variables { get; } = new();

    public VariablesViewModel(IMockDataService data)
    {
        foreach (var item in data.GetVariables())
            Variables.Add(item);
    }
}


