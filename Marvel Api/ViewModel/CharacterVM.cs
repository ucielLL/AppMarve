using Marvel_Api.Model;
using Marvel_Api.Service;
using Marvel_Api.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Marvel_Api.ViewModel
{
    public class CharacterVM : BaseViewModel
    {
        IApiMarvel _apiMarvel;

        public CharacterVM()
        {
            _apiMarvel = new ApiMarvel();   
        }
        public async void Init(string uri)
        {
            Item = await _apiMarvel.GetCharacterWhitUri(uri);
            Thumbnail = Item.thumbnail.path.Replace("http", "https") + "." + item.thumbnail.extension;
        }
        ItemCharacter item;
        public ItemCharacter Item 
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }
        string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }
    }
}
