using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Model
{
    [Table("TableSeries")]
    public class ItemResultSeries : ItemResultBase
    {
        public int startYear { get; set; }
        public int endYear { get; set; }
        public string type { get; set; }

  

    }

   
}
