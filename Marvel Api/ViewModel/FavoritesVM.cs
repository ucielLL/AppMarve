using Marvel_Api.Model;
using Marvel_Api.Repositiry;
using Marvel_Api.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Marvel_Api.ViewModel
{
   public class FavoritesVM: BaseViewModel
    {
        RepositoryFavoriteGeneric _RepositoryFavorite;
        public FavoritesVM(RepositoryFavoriteGeneric repositoryFavorite) 
        {
           _RepositoryFavorite = repositoryFavorite;
           _RepositoryFavorite.initTables();   
          
        }
        public async void Init() 
        {
            StateList = ShowComicsOrSeries.COMICS;
           await ChangeList(StateList);
         }

        public ICommand ChangeListCommand => new Command(async () => 
        {
            if (StateList == ShowComicsOrSeries.COMICS)
            { StateList = ShowComicsOrSeries.SERIES; }
            else { StateList = ShowComicsOrSeries.COMICS; }
            await ChangeList(StateList);
            
          });
        public ICommand SelcetedItemCommand => new Command(async (ID) => 
        {
            var parameters = new NavigationParameters("Id", ID);
            if (StateList == ShowComicsOrSeries.COMICS)
            {
                parameters.Add("type", TypeSereiOrComoc.COMIC);
            }
            else 
            {
                parameters.Add("type", TypeSereiOrComoc.SERIE);
            }
            await NavigationTo("DetailComicSerie", parameters);
        });
        public ICommand DeleteFavoriteCommand => new Command(async (parameter) => 
        {
            var isdeleted = false;
            var Id = int.Parse(parameter.ToString());
            if (StateList == ShowComicsOrSeries.COMICS)
            {
                isdeleted = await _RepositoryFavorite.DelateWithChindenAsync<ItemResultBase>(Id);
                Items = isdeleted ? await _RepositoryFavorite.GetAsSimpleItemsAsync<ItemResultBase>() : Items; 
            }
            else  
            {
                isdeleted = await _RepositoryFavorite.DelateWithChindenAsync<ItemResultSeries>(Id);
                Items = isdeleted ? await _RepositoryFavorite.GetAsSimpleItemsAsync<ItemResultSeries>() : Items;
            }
            await DisplayAlert("Mensaje", isdeleted ? "Eliminado" : " No fue posible eliminar elemento", "Ok");
           
        });
       

        async Task ChangeList(ShowComicsOrSeries showComicsOrSeries) 
        {
           
            if (StateList == ShowComicsOrSeries.COMICS) 
            {
                Title = "favorite comics";
                Items = await _RepositoryFavorite.GetAsSimpleItemsAsync<ItemResultBase>();
                return;
            }
            Title = "favorite series";
            Items = await _RepositoryFavorite.GetAsSimpleItemsAsync<ItemResultSeries>();
        }
        ShowComicsOrSeries StateList;
        enum ShowComicsOrSeries 
        {
            SERIES,
            COMICS
        }

        string title;
        public string Title 
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        List<ItemBase> items;

        public List<ItemBase> Items 
        {
            get { return items; }    
            set => SetProperty(ref items, value);

        }


    }
}
