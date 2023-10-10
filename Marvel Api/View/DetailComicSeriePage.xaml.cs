
using Marvel_Api.Model;
using Marvel_Api.ViewModel;


namespace Marvel_Api.View;

public  partial class DetailComicSeriePage : ContentPage
{
	public  DetailComicSeriePage(DetailComicSerieVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel; 
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var control = sender as VerticalStackLayout;
        var uri = ((TapGestureRecognizer)control.GestureRecognizers[0]).CommandParameter;
        if (uri != null) 
        {
            ((DetailComicSerieVM)BindingContext).GotoCharacterasync(this,uri.ToString() );
        }
    }
}