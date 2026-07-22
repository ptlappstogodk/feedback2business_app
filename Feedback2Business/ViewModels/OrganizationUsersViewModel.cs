using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationUsersViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> Roles { get; } = new() { "Alle", "Admin", "Manager", "Editor", "Viewer" };
    public ObservableCollection<string> Statuses { get; } = new() { "Alle", "Aktiv", "Inviteret", "Deaktiveret" };
    public ObservableCollection<UserModel> Users { get; } = new();

    public OrganizationUsersViewModel(IMockDataService data)
    {
        foreach (var item in data.GetUsers())
            Users.Add(item);
    }
}


