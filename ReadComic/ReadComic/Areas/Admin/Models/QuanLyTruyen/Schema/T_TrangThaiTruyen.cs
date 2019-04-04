using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class T_TrangThaiTruyen
    {
        public int IdTruyen { get; set; }

        public int IdTrangThai { get; set; }

        public string Token { get; set; }
    }
}