
using Marvel_Api.Helpers;
using Marvel_Api.Model;
using Marvel_Api.Service;
using Marvel_Api.View.Componet;
using Marvel_Api.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Marvel_Api.ViewModel
{
    public class ComicsMV : BaseViewModel
    {
        IApiMarvel _apiMarvel;
        ValidateInternet _ValidateInternet = new ValidateInternet();
        public ComicsMV(IApiMarvel api)
        {
            _apiMarvel = api;
            _ValidateInternet.DefineActions(
                async () => 
                {

                    WithOutIntenet = false;
                    IsContentVisible = true;
                    if (ComicsList is null || ComicsList.Count == 0) 
                    {
                        Load = true;
                        ComicsList = await _apiMarvel.GetComics(filter);
                        Load = false;
                    }
                    
                },
                 () => 
                {
                    IsContentVisible = false;
                    WithOutIntenet = true;
                }
                );
            Init();
        
        }
         void Init()
        {
           
            FilterVisible = false;
            IconFilter = ImageSource.FromFile("filter.png");
           
            _ValidateInternet.Validate();
          
          
        }
        public ICommand ShowHideFilterCommand => new Command(() => 
        {
            FilterVisible = !FilterVisible;
            if (FilterVisible)
            {
                IconFilter = ImageSource.FromFile("cancel.png");
                return;
            }
            IconFilter = ImageSource.FromFile("filter.png");

        });
     
        public ICommand SelectFilter => new Command(async (i) =>
        {
            int select =   Int32.Parse(i.ToString());
           
            switch (select)
            {
                case 1:
                    filter.OrderBy(filter.OnsaleDate);
                    break;
                case 2:
                    filter.OrderBy(filter.IssueNumber);
                    break;
                case 3:
                    filter.OrderBy(filter.Title);
                    break;
            }
            ComicsList.Clear();
            FilterVisible = false;
            IconFilter = ImageSource.FromFile("filter.png");
            _ValidateInternet.Validate();
        } );

        public ICommand SelectedItemCommand => new Command(async (i) => 
        {
            var parameters = new NavigationParameters("Id", i);
            parameters.Add("type", TypeSereiOrComoc.COMIC);
            await NavigationTo("DetailComicSerie", parameters);
           
        });
         OptionFilters filter = new OptionFilters();
        List<ItemBase> comicsList;
        public List<ItemBase> ComicsList 
        {
            get { return comicsList; }
            set { SetProperty(ref comicsList, value); }

        }
        bool load;
        public bool Load 
        {
            get { return load; }
            set { SetProperty(ref load, value); }
        }
        bool filterVisible;
        public bool FilterVisible
        {
            get { return filterVisible; }
            set { SetProperty(ref filterVisible, value); }
        }
        ImageSource iconfilter;
        public ImageSource IconFilter 
        {
            get => iconfilter;  
            set { SetProperty(ref iconfilter, value);}
        }
        bool withOutIntenet;
        public bool WithOutIntenet
        {
            get { return withOutIntenet; }
            set { SetProperty(ref withOutIntenet, value); }
        }
        bool isContentVisible;
        public bool IsContentVisible
        {
            get { return isContentVisible; }
            set { SetProperty(ref isContentVisible, value); }
        }
    }
}
