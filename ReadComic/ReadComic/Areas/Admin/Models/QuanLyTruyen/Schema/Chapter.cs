using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class Chapter
    {
        public string title { get; set; }
        public List<string> data { get;set; }
    }
}