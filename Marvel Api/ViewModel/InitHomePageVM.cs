using Marvel_Api.Helpers;
using Marvel_Api.Model;
using Marvel_Api.Repositiry;
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
    public class InitHomePageVM  : BaseViewModel 
    {
        IApiMarvel _ApiMarvel;
        RepositoryFavoriteGeneric _RepositoryFavorite;
        ValidateInternet _ValidateInternet = new ValidateInternet();
        public InitHomePageVM(IApiMarvel api, RepositoryFavoriteGeneric repositoryFavoriteGeneric)
        {
            _ApiMarvel = api;
            _RepositoryFavorite = repositoryFavoriteGeneric;
            _ValidateInternet.DefineActions(
                async () => 
                {
                    var filter = new OptionFilters();
                    filter.Limit(10);
                    WithOutIntenet = false;
                    IsContentVisible = true;
                    ComicsItems = await _ApiMarvel.GetComics(filter);
                    SerieItems = await _ApiMarvel.GetSeries(filter);
                    
                },
                () => 
                {
                    IsContentVisible = false;
                    WithOutIntenet = true;
                }
                );
        }
       public  async void Init() 
        {
          FavoritesItems = await  _RepositoryFavorite.GetAsSimpleItemsAsync<ItemResultBase>();
            if (FavoritesItems == null)
            {
                FavoriteTitle = "Without Favorites";
                FavoriteVisible = false;
                EmptyVisible = true;
            }
            else 
            {
                FavoriteTitle = "Favorites: Comics";
                EmptyVisible = false;
                FavoriteVisible = true;
            }
            _ValidateInternet.Validate();
        }
        public ICommand SelcetedComicItemCommand => new Command((id) => 
        {
            var parameters = new NavigationParameters("Id", id);
            parameters.Add("type", TypeSereiOrComoc.COMIC);
             Navigate(parameters);
        });
        public ICommand SelcetedSerieItemCommand => new Command((id) => 
        {
            var parameters = new NavigationParameters("Id", id);
            parameters.Add("type", TypeSereiOrComoc.SERIE);
            Navigate(parameters);
        });

        async void Navigate(NavigationParameters parameters) 
        {
            await NavigationTo("DetailComicSerie", parameters);
        }
        List<ItemBase> favoritesItems;
        public List<ItemBase> FavoritesItems 
        {
            get { return favoritesItems; }
            set { SetProperty(ref favoritesItems, value); }
        }

        List<ItemBase> comicsItems;
        public List<ItemBase> ComicsItems 
        {
            get { return comicsItems; }
            set { SetProperty(ref comicsItems, value); }
        }

        List<ItemBase> serieItems;
        public List<ItemBase> SerieItems
        {
            get { return serieItems; }
            set { SetProperty(ref serieItems, value); }
        }


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

        string favoriteTitle;
        public string FavoriteTitle 
        {
            get { return favoriteTitle; }
            set { SetProperty(ref favoriteTitle, value); }
        }
        bool favoriteVisible;
        public bool FavoriteVisible
        {
            get { return favoriteVisible; }
            set { SetProperty(ref favoriteVisible, value); }
        }
        bool emptyVisible;
        public bool EmptyVisible
        {
            get { return emptyVisible; }
            set { SetProperty(ref emptyVisible, value); }
        }


    }
}
