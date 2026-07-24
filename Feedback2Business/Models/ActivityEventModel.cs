namespace Feedback2Business.Models;

public class ActivityEventModel
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int OrganizationId { get; set; }

    public string TimestampText => Timestamp.ToString("dd. MMM yyyy, HH:mm");
}


