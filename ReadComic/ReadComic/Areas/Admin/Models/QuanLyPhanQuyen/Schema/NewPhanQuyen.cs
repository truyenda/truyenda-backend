using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyPhanQuyen.Schema
{
    public class NewPhanQuyen
    {
        public string TenVaiTro { get; set; }
        public List<int> Id_QuyenList { get; set; }
    }
}