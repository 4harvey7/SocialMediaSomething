namespace Social;
using Social.Entities;
public partial class ModalPage : ContentPage
{
    public SocialCL ResultPost { get; private set; }

    public ModalPage()
    {
        InitializeComponent();
        TimeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy HH:mm");
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        ResultPost = new SocialCL
        {
            Name = NameEntry.Text,
            Description = DescriptionEditor.Text,
            CreatedAt = DateTime.Now,
            IsPublic = VisibilitySwitch.IsToggled
        };

        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        ResultPost = null;
        await Navigation.PopModalAsync();
    }
}