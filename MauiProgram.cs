using TodoMaui.AppCtx;
using TodoMaui.DbContexts;

namespace TodoMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        DirectoriesCtx.Provision();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

        builder.Services.AddDbContext<TodoMauiDbContext>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

		MauiApp mauiApp = builder.Build();

        TodoMauiDbContext todoMauiDbContext = mauiApp.Services.GetService<TodoMauiDbContext>();

        todoMauiDbContext.Database.EnsureCreated();

        return mauiApp;
	}
}
