using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class TheLoaiChoTruyen
    {
        public int IdTruyen { get; set; }

        public List<int> listTheLoai { get; set; }
    }
}