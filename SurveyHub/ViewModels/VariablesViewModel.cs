using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class VariablesViewModel : ObservableObject
{
    public ObservableCollection<VariableModel> Variables { get; } = new();

    public VariablesViewModel(IMockDataService data)
    {
        foreach (var item in data.GetVariables())
            Variables.Add(item);
    }
}


