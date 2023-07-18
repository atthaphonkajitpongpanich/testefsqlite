using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqliteClassLibrary;

namespace testefsqlite;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddDbContext<ApplicationDBContext>(
			options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(SqliteClassLibrary))));
		return builder.Build();
	}

	public static string GetDatabasePath()
	{
		var databasePath = "";
		var databaseName = "maui.sqlitedb";

		if(DeviceInfo.Platform == DevicePlatform.Android)
		{
			databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
		}

		return databasePath;
	}
}
