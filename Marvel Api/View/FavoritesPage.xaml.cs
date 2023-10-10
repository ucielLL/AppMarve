using Marvel_Api.ViewModel;

namespace Marvel_Api.View;

public partial class FavoritesPage : ContentPage
{
	public FavoritesPage(FavoritesVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		((FavoritesVM)BindingContext).Init();
    }



}