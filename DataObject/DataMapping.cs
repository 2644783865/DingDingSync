using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DataObject
{
    public class Token
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
    }
}