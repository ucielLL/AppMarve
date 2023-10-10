using Marvel_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.ViewModel.Build
{
    public class ItemBuild
    {
        public ItemBuild(ItemResultBase Item)
        {
                Base = Item;    
        }
        public ItemResultBase Base { get; set; }

        public ItemResultSeries Series { get { return Base as ItemResultSeries; } }
      
    }
}
