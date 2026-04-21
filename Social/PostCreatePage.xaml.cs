namespace Social;

public partial class PostCreatePage : ContentPage
{
    private TaskCompletionSource<SocialCL> tcs;

    private string imagePath = "dotnet_bot.png";

    public PostCreatePage()
    {
        InitializeComponent();
    }

    // 🔥 THIS IS THE RETURN PIPELINE
    public Task<SocialCL> GetResultAsync()
    {
        tcs = new TaskCompletionSource<SocialCL>();
        return tcs.Task;
    }

    private async void OnPost(object sender, EventArgs e)
    {
        var post = new SocialCL
        {
            Name = TitleEntry.Text,
            Description = DescEntry.Text,
            Image = imagePath,
            CreatedAt = DateTime.Now,
            IsPublic = VisibilitySwitch.IsToggled,
            Comments = new System.Collections.ObjectModel.ObservableCollection<string>()
        };

        tcs.TrySetResult(post);

        await Navigation.PopModalAsync();
    }

    private async void OnCancel(object sender, EventArgs e)
    {
        tcs.TrySetResult(null);
        await Navigation.PopModalAsync();
    }

    private async void OnPickImage(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync();

        if (result != null)
        {
            imagePath = result.FileName;
            PreviewImage.Source = ImageSource.FromFile(result.FullPath);
        }
    }
}