using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationUsersViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> Roles { get; } = new() { "Alle", "Admin", "Manager", "Editor", "Viewer" };
    public ObservableCollection<string> Statuses { get; } = new() { "Alle", "Aktiv", "Inviteret", "Deaktiveret" };
    public ObservableCollection<UserModel> Users { get; } = new();

    public ICommand InviterUserCommand { get; }

    public OrganizationUsersViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var item in data.GetUsers())
            Users.Add(item);

        InviterUserCommand = new RelayCommand(async () => await InviterUserAsync());
    }

    private async Task InviterUserAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Inviter bruger", "Indtast navn:", "Næste", "Annuller", "Navn");
        if (string.IsNullOrWhiteSpace(name)) return;

        var email = await Application.Current!.MainPage!.DisplayPromptAsync("Inviter bruger", "Indtast email:", "Inviter", "Annuller", "Email");
        if (string.IsNullOrWhiteSpace(email)) return;

        var user = new UserModel
        {
            Name = name.Trim(),
            Email = email.Trim(),
            Role = "Viewer",
            Status = "Inviteret",
            LastActiveAt = null
        };

        _data.CreateUser(user);
        Users.Add(user);
    }
}


