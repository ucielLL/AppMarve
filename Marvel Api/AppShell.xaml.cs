using Marvel_Api.View;

namespace Marvel_Api;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("ComicsPage/DetailComicSerie", typeof(DetailComicSeriePage));
        Routing.RegisterRoute("SeriesPage/DetailComicSerie", typeof(DetailComicSeriePage));
        Routing.RegisterRoute("FavoritesPage/DetailComicSerie", typeof(DetailComicSeriePage));
        Routing.RegisterRoute("HomePage/DetailComicSerie", typeof(DetailComicSeriePage));
    }
}




