using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema
{
    public class GetChuong
    {
        public int Id { get; set; }
        public string TenChuong { get; set; }
        public float SoThuTu { get; set; }
        public string LinkAnh { get; set; }
        public long LuotXem { get; set; }
        public DateTime NgayTao { get; set; }
    }
}