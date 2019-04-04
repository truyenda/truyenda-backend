using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema
{
    public class ChuongCuaTruyen
    {
        public int Id { get; set; }
        public int IdTruyen { get; set; }
        public string TenChuong { get; set; }
        public float SoThuTu { get; set; }
        public string LinkAnh { get; set; }
        public long LuotXem { get; set; }

    }
}