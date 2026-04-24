using Social.Entities;
using Social.Services;

namespace Social;

public partial class PostDetailPage : ContentPage
{
    SocialCL _post;
    DatabaseServices _db;

    public PostDetailPage(SocialCL selectedPost, DatabaseServices db)
    {
        InitializeComponent();
        _post = selectedPost;
        _db = db;

        BindingContext = _post;
        CommentList.ItemsSource = _post.Comments;
    }

    private async void OnPostComment(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CommentBox.Text))
        {
            _post.Comments.Add(CommentBox.Text);
            CommentBox.Text = "";

            // Save the post back to DB to persist the new comment
            await _db.SavePost(_post);

            // Refresh UI
            CommentList.ItemsSource = null;
            CommentList.ItemsSource = _post.Comments;
        }
    }

    private async void OnDeletePost(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete", "Delete this post forever?", "Yes", "No");
        if (answer)
        {
            await _db.DeletePost(_post);
            await Navigation.PopModalAsync();
        }
    }

    private async void OnClose(object sender, EventArgs e) => await Navigation.PopModalAsync();
}