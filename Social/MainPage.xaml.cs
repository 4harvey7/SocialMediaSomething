using System.Collections.ObjectModel;

namespace Social;

public partial class MainPage : ContentPage
{
    public ObservableCollection<SocialCL> Posts { get; set; }

    public MainPage()
    {
        InitializeComponent();

        Posts = new ObservableCollection<SocialCL>
        {
            new SocialCL
            {
                Name = "The Godfather",
                Description = "One of the greatest movies ever",
                Image = "hey.jpg",
                CreatedAt = DateTime.Now,
                IsPublic = true
            },
            new SocialCL
            {
                Name = "Minecraft Story Mode",
                Description = "One of my favorite games",
                Image = "mcsm.png",
                CreatedAt = DateTime.Now,
                IsPublic = true
            }
        };

        PostsCollectionView.ItemsSource = Posts;
    }

    // 🟢 ADD POST
    private async void OnAddPostClicked(object sender, EventArgs e)
    {
        var modal = new PostCreatePage();

        var task = modal.GetResultAsync();

        await Navigation.PushModalAsync(modal);

        var newPost = await task;

        if (newPost != null)
        {
            Posts.Insert(0, newPost);
        }
    }

    // 🟢 TAP POST (THIS IS THE CLICK FIX)
    private async void OnPostTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;

        if (frame?.BindingContext is SocialCL post)
        {
            await Navigation.PushModalAsync(new PostDetailPage(post));
        }
    }
}