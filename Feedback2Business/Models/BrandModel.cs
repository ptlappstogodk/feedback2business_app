using System;
using Microsoft.Maui.Graphics;
using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class BrandModel : ObservableObject
{
    private int _id;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _logoUrl = "🏬";
    private string _status = "Aktiv";
    private int _surveyCount;
    private int _organizationId;
    private DateTime _updatedAt = DateTime.Now;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string LogoUrl
    {
        get => _logoUrl;
        set => SetProperty(ref _logoUrl, value);
    }

    public string Status
    {
        get => _status;
        set
        {
            if (SetProperty(ref _status, value))
            {
                Raise(nameof(StatusBadgeColor));
                Raise(nameof(StatusBadgeTextColor));
            }
        }
    }

    public int SurveyCount
    {
        get => _surveyCount;
        set
        {
            if (SetProperty(ref _surveyCount, value))
            {
                Raise(nameof(SurveyCountText));
            }
        }
    }

    public int OrganizationId
    {
        get => _organizationId;
        set => SetProperty(ref _organizationId, value);
    }

    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set
        {
            if (SetProperty(ref _updatedAt, value))
            {
                Raise(nameof(FormattedUpdatedAt));
            }
        }
    }

    public string SurveyCountText => $"{SurveyCount} surveys";
    public string FormattedUpdatedAt => UpdatedAt.ToString("dd. MMM yyyy HH.mm");

    public Color StatusBadgeColor => Status switch
    {
        "Aktiv" => Color.FromArgb("#D9F5E5"),
        "Inaktiv" => Color.FromArgb("#F1F5F9"),
        _ => Color.FromArgb("#D9F5E5")
    };

    public Color StatusBadgeTextColor => Status switch
    {
        "Aktiv" => Color.FromArgb("#1E8E5A"),
        "Inaktiv" => Color.FromArgb("#64748B"),
        _ => Color.FromArgb("#1E8E5A")
    };
}


