using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    public class ChuongTruyen
    {
        public int IdTruyen { get; set; }
        public string AnhBia { get; set; }
        public string TenTruyen { get; set; }
        public List<TheLoai> listTheLoai { get; set; }
        public List<Chuong> listChuong { get; set; }
    }
}