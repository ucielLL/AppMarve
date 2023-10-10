using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel_Api.Model
{
    internal class RootResponse
    {
      
            public int code { get; set; }
            public string status { get; set; }
            public string etag { get; set; }
            public Data data { get; set; }
    }
    
}
