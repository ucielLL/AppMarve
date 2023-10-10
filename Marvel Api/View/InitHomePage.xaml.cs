using Marvel_Api.ViewModel;

namespace Marvel_Api.View;

public partial class InitHomePage : ContentPage
{
	public InitHomePage(InitHomePageVM ViewModel)
    {
		InitializeComponent();

		BindingContext = ViewModel;	
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((InitHomePageVM)BindingContext).Init();
    }
}