using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    public class GetOneChapter
    {
        public int Id_Chuong { get; set; }
        public string TenChuong { get; set; }

        public int Id_Truyen { get; set; }
        public string TenTruyen { get; set; }

        public int Id_NhomDich { get; set; }
        public string TenNhomDich { get; set; }

        public float SoThuTu { get; set; }
        public string LinkAnh { get; set; }

        public long LuotXem { get; set; }
        public DateTime NgayTao { get; set; }
    }
}