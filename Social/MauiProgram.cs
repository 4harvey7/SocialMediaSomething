using Microsoft.Extensions.Logging;
using Social.Services; // Ensure this matches your namespace
using System.IO;

namespace Social;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. Define where the database file lives on the device
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "data.db");

        // 2. Register DatabaseServices as a Singleton (one instance for the whole app)
        builder.Services.AddSingleton(s => new DatabaseServices(dbPath));

        // 3. Register your Pages so they can receive the database service
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<PostDetailPage>();
        builder.Services.AddTransient<PostCreatePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}