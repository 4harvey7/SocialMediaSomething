using SQLite;
using Social.Entities;

namespace Social.Services;

public class DatabaseServices
{
    private SQLiteAsyncConnection _dbcon;

    public DatabaseServices(string dbPath)
    {
        _dbcon = new SQLiteAsyncConnection(dbPath);
        _dbcon.CreateTableAsync<SocialCL>().Wait();
    }

    public async Task<List<SocialCL>> GetPosts() =>
        await _dbcon.Table<SocialCL>().OrderByDescending(x => x.CreatedAt).ToListAsync();

    public async Task<int> SavePost(SocialCL social) =>
        social.PostId != 0 ? await _dbcon.UpdateAsync(social) : await _dbcon.InsertAsync(social);

    public async Task<int> DeletePost(SocialCL social) =>
        await _dbcon.DeleteAsync(social);
}