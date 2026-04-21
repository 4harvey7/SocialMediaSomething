using System;
using System.Collections.ObjectModel;

public class SocialCL
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string Image { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }

    public string VisibilityText =>
        IsPublic ? "Public" : "Private";

    public string FormattedDate =>
        CreatedAt.ToString("MMMM dd, yyyy HH:mm");

    public ObservableCollection<string> Comments { get; set; } = new();
}