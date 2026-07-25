using System.Collections;
using Feedback2Business.Models;
using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Shared;

public partial class SectionCardView : ContentView
{
    private static SectionModel? _draggedSection;
    private static SurveyQuestionModel? _draggedQuestion;

    public static readonly BindableProperty SectionProperty =
        BindableProperty.Create(
            nameof(Section),
            typeof(SectionModel),
            typeof(SectionCardView),
            null,
            propertyChanged: OnSectionChanged);

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SectionCardView), string.Empty);

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SectionCardView), null);

    public SectionModel? Section
    {
        get => (SectionModel?)GetValue(SectionProperty);
        set => SetValue(SectionProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public string SectionTitle
    {
        get => Section?.Title ?? Title;
        set
        {
            if (Section != null)
            {
                Section.Title = value;
            }
            Title = value;
        }
    }

    public IEnumerable? SectionQuestions => Section?.Questions ?? ItemsSource;

    private static void OnSectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SectionCardView card)
        {
            card.OnPropertyChanged(nameof(SectionTitle));
            card.OnPropertyChanged(nameof(SectionQuestions));
        }
    }

    public SectionCardView()
    {
        InitializeComponent();
    }

    private void OnAddQuestionClicked(object? sender, EventArgs e)
    {
        if (Section != null)
        {
            FindViewModel()?.TilfoejSpoergsmaalTilSektionDirect(Section);
        }
    }

    private void OnMoveSectionUpClicked(object? sender, EventArgs e)
    {
        if (Section != null)
        {
            FindViewModel()?.MoveSectionUpDirect(Section);
        }
    }

    private void OnMoveSectionDownClicked(object? sender, EventArgs e)
    {
        if (Section != null)
        {
            FindViewModel()?.MoveSectionDownDirect(Section);
        }
    }

    private void OnDeleteSectionClicked(object? sender, EventArgs e)
    {
        if (Section != null)
        {
            FindViewModel()?.SletSektionDirect(Section);
        }
    }

    private void OnQuestionTapped(object? sender, EventArgs e)
    {
        if (sender is Element element && element.BindingContext is SurveyQuestionModel question)
        {
            FindViewModel()?.SelectQuestionDirect(question);
        }
    }

    private void OnMoveQuestionUpClicked(object? sender, EventArgs e)
    {
        if (sender is Element element && element.BindingContext is SurveyQuestionModel question)
        {
            FindViewModel()?.MoveQuestionUpDirect(question);
        }
    }

    private void OnMoveQuestionDownClicked(object? sender, EventArgs e)
    {
        if (sender is Element element && element.BindingContext is SurveyQuestionModel question)
        {
            FindViewModel()?.MoveQuestionDownDirect(question);
        }
    }

    private void OnDeleteQuestionClicked(object? sender, EventArgs e)
    {
        if (sender is Element element && element.BindingContext is SurveyQuestionModel question)
        {
            FindViewModel()?.SletSpoergsmaalDirect(question);
        }
    }

    private void OnSectionDragStarting(object? sender, DragStartingEventArgs e)
    {
        if (Section != null)
        {
            _draggedSection = Section;
        }
    }

    private void OnSectionDrop(object? sender, DropEventArgs e)
    {
        if (_draggedSection != null && Section != null && _draggedSection != Section)
        {
            FindViewModel()?.ReorderSection(_draggedSection, Section);
            _draggedSection = null;
        }
    }

    private void OnQuestionDragStarting(object? sender, DragStartingEventArgs e)
    {
        if (sender is Element element && element.BindingContext is SurveyQuestionModel question)
        {
            _draggedQuestion = question;
        }
    }

    private void OnQuestionDrop(object? sender, DropEventArgs e)
    {
        if (_draggedQuestion != null && sender is Element element && element.BindingContext is SurveyQuestionModel targetQuestion)
        {
            FindViewModel()?.ReorderQuestion(_draggedQuestion, targetQuestion);
            _draggedQuestion = null;
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


