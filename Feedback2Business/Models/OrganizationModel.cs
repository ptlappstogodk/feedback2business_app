using System;
using Microsoft.Maui.Graphics;
using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class OrganizationModel : ObservableObject
{
    private int _id;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _status = "Aktiv";
    private int _brandCount;
    private int _surveyCount;
    private int _userCount;
    private DateTime _updatedAt = DateTime.Now;
    private string _logoText = string.Empty;

    // Contact and address details for drawer / details view
    private string _contactPerson = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;
    private string _address = string.Empty;
    private string _postalCode = string.Empty;
    private string _city = string.Empty;
    private string _country = "Danmark";

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value))
            {
                Raise(nameof(LogoText));
            }
        }
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
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

    public int BrandCount
    {
        get => _brandCount;
        set => SetProperty(ref _brandCount, value);
    }

    public int SurveyCount
    {
        get => _surveyCount;
        set => SetProperty(ref _surveyCount, value);
    }

    public int UserCount
    {
        get => _userCount;
        set => SetProperty(ref _userCount, value);
    }

    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set
        {
            if (SetProperty(ref _updatedAt, value))
            {
                Raise(nameof(UpdatedText));
                Raise(nameof(FormattedUpdatedAt));
            }
        }
    }

    public string LogoText
    {
        get => !string.IsNullOrEmpty(_logoText) ? _logoText : GetInitials(Name);
        set => SetProperty(ref _logoText, value);
    }

    public string ContactPerson
    {
        get => _contactPerson;
        set => SetProperty(ref _contactPerson, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    public string PostalCode
    {
        get => _postalCode;
        set => SetProperty(ref _postalCode, value);
    }

    public string City
    {
        get => _city;
        set => SetProperty(ref _city, value);
    }

    public string Country
    {
        get => _country;
        set => SetProperty(ref _country, value);
    }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
    public string FormattedUpdatedAt => UpdatedAt.ToString("dd. MMM yyyy HH.mm");

    public Color StatusBadgeColor => Status switch
    {
        "Aktiv" => Color.FromArgb("#D9F5E5"),
        "Inviteret" => Color.FromArgb("#E0E7FF"),
        "Inaktiv" => Color.FromArgb("#F1F5F9"),
        _ => Color.FromArgb("#F1F5F9")
    };

    public Color StatusBadgeTextColor => Status switch
    {
        "Aktiv" => Color.FromArgb("#1E8E5A"),
        "Inviteret" => Color.FromArgb("#4F46E5"),
        "Inaktiv" => Color.FromArgb("#64748B"),
        _ => Color.FromArgb("#64748B")
    };

    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "OG";
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1) return parts[0].Length >= 2 ? parts[0][..2].ToUpper() : parts[0].ToUpper();
        return $"{parts[0][0]}{parts[1][0]}".ToUpper();
    }
}


