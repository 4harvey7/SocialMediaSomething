using System.Collections.ObjectModel;
using Social.Entities;
using Social.Services;

namespace Social;

public partial class MainPage : ContentPage
{
    private readonly DatabaseServices _db;
    public ObservableCollection<SocialCL> Posts { get; set; } = new();

    // The DB service is injected via the constructor
    public MainPage(DatabaseServices db)
    {
        InitializeComponent();
        _db = db;
        PostsCollectionView.ItemsSource = Posts;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPosts();
    }

    private async Task LoadPosts()
    {
        var items = await _db.GetPosts();
        Posts.Clear();
        foreach (var item in items)
        {
            Posts.Add(item);
        }
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
            await _db.SavePost(newPost); // Save to SQLite
            await LoadPosts(); // Refresh list
        }
    }

    // 🟢 VIEW DETAIL
    private async void OnPostTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        if (frame?.BindingContext is SocialCL post)
        {
            // Pass the DB service to the detail page
            await Navigation.PushModalAsync(new PostDetailPage(post, _db));
        }
    }
}