using SQLite;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Social.Entities;

public class SocialCL
{
    [PrimaryKey, AutoIncrement]
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // We store the file path string here to save storage
    public string Image { get; set; }

    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }

    [Ignore]
    public ObservableCollection<string> Comments { get; set; } = new();

    // SQLite saves/loads this string automatically
    public string CommentsJson
    {
        get => JsonSerializer.Serialize(Comments);
        set => Comments = string.IsNullOrEmpty(value)
                ? new()
                : JsonSerializer.Deserialize<ObservableCollection<string>>(value);
    }

    [Ignore]
    public string VisibilityText => IsPublic ? "Public" : "Private";

    [Ignore]
    public string FormattedDate => CreatedAt.ToString("MMMM dd, yyyy HH:mm");
}