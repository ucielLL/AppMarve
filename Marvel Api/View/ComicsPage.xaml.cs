using Marvel_Api.ViewModel;

namespace Marvel_Api.View;

public partial class ComicsPage : ContentPage
{
	public ComicsPage(ComicsMV vm)
	{
		InitializeComponent();
		BindingContext = vm;	
	}
 
	
}