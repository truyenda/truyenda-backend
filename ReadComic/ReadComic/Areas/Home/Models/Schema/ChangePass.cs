using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    public class ChangePass
    {
        public string tokenReset { get; set; }
        public string NewPass { get; set; }
    }
}