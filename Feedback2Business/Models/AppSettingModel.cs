namespace Feedback2Business.Models;

public class AppSettingModel
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public bool OfflineEnabled { get; set; } = true;
    public bool AutoSyncEnabled { get; set; } = true;
    public bool RequireLocationAtStart { get; set; } = true;
    public bool UseLocationOnObservations { get; set; }
    public bool CompressImages { get; set; } = true;
    public string SelectedMaxFileSize { get; set; } = "2 MB";
    public bool ShowSurveyProgress { get; set; } = true;
    public bool AllowSkipForward { get; set; }
    public string Language { get; set; } = "Dansk";
    public string Timezone { get; set; } = "UTC+01:00 København";
    public string DateFormat { get; set; } = "DD-MM-YYYY";
}
