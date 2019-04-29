using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class DataSearch
    {
        public int Status { get; set; }
        public int Sort { get; set; }
        public int Rank { get; set; }
        public int[] Category { get; set; }
    }
}