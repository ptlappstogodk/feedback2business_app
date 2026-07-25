using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Shared;

public partial class QuestionPropertyPanel : ContentView
{
    public QuestionPropertyPanel()
    {
        InitializeComponent();
    }

    private void OnDeleteQuestionClicked(object? sender, EventArgs e)
    {
        if (BindingContext is SurveyQuestionEditorViewModel editorVm)
        {
            FindViewModel()?.SletSpoergsmaalDirect(editorVm);
        }
    }

    private OrganizationBrandsViewModel? FindViewModel()
    {
        Element? parent = this;
        while (parent != null)
        {
            if (parent.BindingContext is OrganizationBrandsViewModel vm)
            {
                return vm;
            }
            parent = parent.Parent;
        }
        return null;
    }
}


