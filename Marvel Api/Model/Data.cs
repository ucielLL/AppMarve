using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Model
{
    internal class Data
    {
            public int limit { get; set; }
            public int total { get; set; }
            public int count { get; set; }
            public List<object> results { get; set; }
    }
}
