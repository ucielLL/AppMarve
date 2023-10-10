using CommunityToolkit.Maui;
using Marvel_Api.Model;
using Marvel_Api.Repositiry;
using Marvel_Api.Service;
using Marvel_Api.View;
using Marvel_Api.ViewModel;
using Microsoft.Extensions.Logging;

namespace Marvel_Api;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
       
        builder.Services.AddSingleton<InitHomePageVM>();
        builder.Services.AddSingleton<InitHomePage>();
        builder.Services.AddSingleton<IApiMarvel>(new ApiMarvel());
        builder.Services.AddTransient<ComicsMV>();
        builder.Services.AddTransient<ComicsPage>();
        builder.Services.AddTransient<SeriesVM>();
        builder.Services.AddTransient<SeriesPage>();
		builder.Services.AddTransient<DetailComicSerieVM>();
		builder.Services.AddTransient<DetailComicSeriePage>();
		builder.Services.AddTransient<FavoritesPage>();
		builder.Services.AddTransient<FavoritesVM>();
		builder.Services.AddSingleton<RepositoryFavoriteGeneric>();
		
	
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
