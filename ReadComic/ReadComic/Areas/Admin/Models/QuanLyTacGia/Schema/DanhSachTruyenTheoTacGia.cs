using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema
{
    public class DanhSachTruyenTheoTacGia
    {
        public int Id_TacGia { get; set; }

        public string TenTacGia { get; set; }

        public List<Truyen_TacGia> listTruyen { get; set; }
    }
}