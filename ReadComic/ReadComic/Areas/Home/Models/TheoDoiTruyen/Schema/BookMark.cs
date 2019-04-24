using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.TheoDoiTruyen.Schema
{
    public class BookMark
    {
        public int Id_BookMark { get; set; }

        public int Id_Truyen { get; set; }

        public string TenTruyen { get; set; }

        public int Id_NhomDich { get; set; }

        public string TenNhom { get; set; }

        public int? Id_ChuongDanhDau { get; set; }

        public string TenChuongDanhDau { get; set; }

        public int Id_ChuongMoiNhat { get; set; }

        public string TenChuongMoiNhat { get; set; }
    }
}