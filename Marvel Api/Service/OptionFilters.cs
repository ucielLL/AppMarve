using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marvel_Api.Service
{
    public  class OptionFilters
    {
        
        string Filter = string.Empty;
        public OptionFilters() 
        {
        }
       
        public void NameCharacterStartsWith( string name) 
        {
            if (string.IsNullOrEmpty(name)) return;
            Filter = $"nameStartsWith={name}";
        }

        public void TitleStartsWith( string name) 
        {
            if (string.IsNullOrEmpty(name)) return;
            Filter = $"titleStartsWith={name}";
        }
        public void OrderBy(string filter)
        {
          Filter = "orderBy="+filter; 
        }
        public void FilterBySeriesType(string seriesType)
        {
            Filter = "seriesType=" + seriesType;
        }

        public void Limit(int limitVal) 
        {
            Filter = $"limit={limitVal}";
        }
        public string GetFilter() 
        {
            return Filter;
        }
       public  readonly string OnsaleDate = "onsaleDate";
        public readonly string Title = "title";
        public readonly string IssueNumber = "issueNumber";
        public readonly string SERIE_ONGOING = "ongoing";
        public readonly string SERIE_ONESHOT = "one shot";




    }
}
