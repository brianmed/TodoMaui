using TodoMaui.DbContexts;

namespace TodoMaui;

public partial class App : Application
{
	public App(
        MainPage mainPage,
        TodoMauiDbContext todoMauiDbContext)
	{
		InitializeComponent();

        MainPage = mainPage;
	}
}
