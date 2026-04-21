namespace Social;

public partial class PostDetailPage : ContentPage
{
    SocialCL post;

    public PostDetailPage(SocialCL selectedPost)
    {
        InitializeComponent();

        post = selectedPost;
        BindingContext = post;

        CommentList.ItemsSource = post.Comments;
    }

    // 🟢 POST COMMENT
    private void OnPostComment(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CommentBox.Text))
        {
            post.Comments.Add(CommentBox.Text);

            CommentBox.Text = "";

            // refresh UI
            CommentList.ItemsSource = null;
            CommentList.ItemsSource = post.Comments;
        }
    }

    // 🟢 CLOSE MODAL
    private async void OnClose(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}