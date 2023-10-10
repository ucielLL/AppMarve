

using CommunityToolkit.Maui.Views;
using Marvel_Api.Helpers;
using Marvel_Api.Model;
using Marvel_Api.Repositiry;
using Marvel_Api.Service;
using Marvel_Api.View;
using Marvel_Api.ViewModel.Base;
using Marvel_Api.ViewModel.Build;
using Microsoft.VisualBasic;
using System.Windows.Input;

namespace Marvel_Api.ViewModel
{
    public class DetailComicSerieVM : BaseViewModel

    {
        IApiMarvel _apiMarvel;
        RepositoryFavoriteGeneric _RepositoryFavorite;
        ValidateInternet _ValidateInternet = new ValidateInternet();

        public DetailComicSerieVM(IApiMarvel apiMarvel, RepositoryFavoriteGeneric repositoryFavorite)
        {
            _apiMarvel = apiMarvel;
            _RepositoryFavorite = repositoryFavorite;
            SerieVisible = false;
            _ValidateInternet.DefineActions(
             async () =>
             {

                 WithOutIntenet = false;
                 IsContentVisible = true;
                 if (SereiOrComoc == TypeSereiOrComoc.SERIE)
                 {
                     IsFavorite = await _RepositoryFavorite.ItemExist<ItemResultSeries>(id);
                     SerieVisible = true;
                     Item = new ItemBuild(await _apiMarvel.GetSerieById(id));
                 }
                 else
                 {
                     IsFavorite = await _RepositoryFavorite.ItemExist<ItemResultBase>(id);
                     Item = new ItemBuild(await _apiMarvel.GetComicById(id));
                 }
                 ImageLike = IsFavorite ? "likered.png" : "likegray.png";
             },
              async () =>
              {
                  if (SereiOrComoc == TypeSereiOrComoc.SERIE)
                  {
                      IsFavorite = await _RepositoryFavorite.ItemExist<ItemResultSeries>(id);
                      Item = new ItemBuild(IsFavorite ? await _RepositoryFavorite.GetItemById<ItemResultSeries>(id) : null);
                  }
                  else 
                  {
                      IsFavorite = await _RepositoryFavorite.ItemExist<ItemResultBase>(id);
                      Item = new ItemBuild(IsFavorite ? await _RepositoryFavorite.GetItemById<ItemResultBase>(id) : null);
                  }
                  ImageLike = IsFavorite ? "likered.png" : "likegray.png";
                  IsContentVisible = IsFavorite;
                  WithOutIntenet = !IsFavorite;
              }
             );
        }
        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            base.ApplyQueryAttributes(query);
            id = Int32.Parse(query["Id"].ToString());
            SereiOrComoc = (TypeSereiOrComoc)Enum.Parse(typeof(TypeSereiOrComoc), (query["type"]).ToString());
            _ValidateInternet.Validate();
        }
      
        public async void GotoCharacterasync(Page page, string uri)
        {
            var popup = new CharacterPopUp(uri);
            await PopupExtensions.ShowPopupAsync<CharacterPopUp>(page, popup);
        }
        public ICommand AddToFavirite => new Command(async () =>
        {
            if (!IsFavorite)
            { 
                var isAded = false;
                if (SereiOrComoc == TypeSereiOrComoc.COMIC)
                {
                    isAded = await _RepositoryFavorite.SaveOrRemplaceItemWithChildrenAsync<ItemResultBase>(Item.Base);

                }
                else
                {
                    isAded = await _RepositoryFavorite.SaveOrRemplaceItemWithChildrenAsync<ItemResultSeries>(Item.Series);

                }
                IsFavorite = isAded;
                ImageLike = isAded ? "likered.png" : "likegray.png";
                await DisplayAlert("Lista de favoritos", isAded ? "Agregado" : "No agregado", "Ok");
                return;
            }
            else
            {
                var delete = false;
                if (SereiOrComoc == TypeSereiOrComoc.COMIC)
                {
                    delete = await _RepositoryFavorite.DelateWithChindenAsync<ItemResultBase>(Item.Base.id);
                }
                else
                {
                    delete = await _RepositoryFavorite.DelateWithChindenAsync<ItemResultSeries>(Item.Series.id);
                }
                IsFavorite = delete;
                ImageLike = delete ? "likegray.png" : "likered.png";
                await DisplayAlert("Lista de favoritos", delete ? "Eliminado" : "No Eliminado", "Ok");
                return;

            }

        });
        bool isContentVisible;
        public bool IsContentVisible
        {
            get { return isContentVisible; }
            set { SetProperty(ref isContentVisible, value); }
        }

        bool withOutIntenet;
        public bool WithOutIntenet
        {
            get { return withOutIntenet; }        
            set { SetProperty(ref withOutIntenet, value); }   
        }
        TypeSereiOrComoc SereiOrComoc;
        ItemBuild item;
        public ItemBuild Item
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }

        bool serieVisible;
        public bool SerieVisible 
        {
            get { return serieVisible; }
            set { SetProperty(ref serieVisible, value); }

        }

        string imageLike;
        public string ImageLike 
        {
            get { return imageLike; }
            set { SetProperty(ref imageLike, value); }
        }
        public int startYear { get; set; }
        public int endYear { get; set; }
        public string type { get; set; }

        bool IsFavorite = false;
        int id = 0;
    }
}
