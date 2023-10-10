using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Model
{
    public class ItemCharacter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
    }
}
