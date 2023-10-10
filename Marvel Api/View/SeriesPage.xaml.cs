using Marvel_Api.ViewModel;

namespace Marvel_Api.View;

public partial class SeriesPage : ContentPage
{
	public SeriesPage(SeriesVM viewModel )
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}