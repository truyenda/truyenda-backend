using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class ThemTacGiaChoTruyen
    {
        public int IdTruyen { get; set; }

        public List<int> listIdTacGia { get; set; }
    }
}