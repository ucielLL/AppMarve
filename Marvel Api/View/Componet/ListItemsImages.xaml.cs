using Marvel_Api.Model;

namespace Marvel_Api.View.Componet;
public partial class ListItemsImages : ContentView
{

	public ListItemsImages()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty Nameforlist = BindableProperty.Create(nameof(NameList), typeof(string), typeof(ListItemsImages), string.Empty);
    
    public string NameList
    {
        get => (string)GetValue(Nameforlist);
        set => SetValue(Nameforlist, value);
    
    }

}