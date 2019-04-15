using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyErrorMgs.Schema
{
    public class ErrorMgs
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public string Msg { get; set; }
    }
}