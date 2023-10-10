using Marvel_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Service
{
    public interface IApiMarvel
    {
        Task<List<ItemBase>> GetComics(OptionFilters optionfilters = null);
        Task<List<ItemBase>> GetSeries(OptionFilters optionfilters = null);
        Task<ItemResultBase> GetComicById(int Id);
        Task<ItemResultSeries> GetSerieById(int Id);
        Task<ItemCharacter> GetCharacterWhitUri(string uri);
    }
   
}
