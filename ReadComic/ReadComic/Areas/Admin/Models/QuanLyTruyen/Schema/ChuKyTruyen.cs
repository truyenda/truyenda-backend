using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class ChuKyTruyen
    {
        public int IdTruyen { get; set; }
        public int IdChuKy { get; set; }
        public string Token { get; set; }
    }
}