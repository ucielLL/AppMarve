using Marvel_Api.Helpers;
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
    public class SeriesVM :BaseViewModel
    {
        IApiMarvel _apiMarvel;
        ValidateInternet _ValidateInternet = new ValidateInternet();
        public SeriesVM(IApiMarvel apiMarvel)
        {
                _apiMarvel = apiMarvel;
            _ValidateInternet.DefineActions(
               async () =>
               {

                   WithOutIntenet = false;
                   IsContentVisible = true;
                   if (SeriesList is null || SeriesList.Count == 0)
                   {
                       Load = true;
                       SeriesList = await _apiMarvel.GetSeries(filter);
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

        public ICommand ShowHideFilter => new Command(() =>
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
            int select = Int32.Parse(i.ToString());
            switch (select)
            {
                case 1:
                    filter.FilterBySeriesType(filter.SERIE_ONESHOT);
                    break;
                case 2:
                    filter.FilterBySeriesType(filter.SERIE_ONGOING);
                    break;
                default:
                    break;
            }
            FilterVisible = false;
            SeriesList.Clear();
            IconFilter = ImageSource.FromFile("filter.png");
            _ValidateInternet.Validate();
        });
        public ICommand SelectedItemCommand => new Command(async (i) =>
        {
            var parameters = new NavigationParameters("Id", i);
            parameters.Add("type", TypeSereiOrComoc.SERIE);
            await NavigationTo("DetailComicSerie",parameters);

        });
        OptionFilters filter = new OptionFilters();
        List <ItemBase> seriesList;
        public List<ItemBase> SeriesList
        {
            get { return seriesList; }
            set { SetProperty(ref seriesList, value); }
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
            set { SetProperty(ref iconfilter, value); }
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
    enum TypeSereiOrComoc {SERIE,COMIC }
}
