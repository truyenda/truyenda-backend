using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    public class Chuong
    {
        public int IdChuong { get; set; }
        public string TenChuong { get; set; }
        public float soThuTu { get; set; }
        public long luotXem { get; set; }
        public DateTime ngayTao { get; set; }
        public string linkAnh { get; set; }
    }
}