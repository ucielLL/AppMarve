
using Marvel_Api.ViewModel;
using Microsoft.Maui.Controls;

namespace Marvel_Api.View;

public partial class CharacterPopUp : CommunityToolkit.Maui.Views.Popup
{
    public CharacterPopUp(string uriCharacter)
    {
        InitializeComponent();
        var viewmodel = new CharacterVM();
        BindingContext = viewmodel;
        viewmodel.Init(uriCharacter);

    }


    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        int x = 0;
        switch (e.Direction)
        {
            case SwipeDirection.Up:
                await ImageImG.ScaleTo(0.3, 1000, Easing.CubicInOut);
                await descrition.TranslateTo(0,-100, 1000, Easing.CubicInOut);
             
                break;
            case SwipeDirection.Down:
                await descrition.TranslateTo(0,0, 1000, Easing.CubicInOut);
                await ImageImG.ScaleTo(1, 500, Easing.Linear);
               
                break;
        }
    }
}