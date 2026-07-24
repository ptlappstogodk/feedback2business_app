namespace Feedback2Business.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? LastActiveAt { get; set; }
    public int OrganizationId { get; set; }

    public string LastActiveText => LastActiveAt?.ToString("dd. MM yy") ?? "-";
}


