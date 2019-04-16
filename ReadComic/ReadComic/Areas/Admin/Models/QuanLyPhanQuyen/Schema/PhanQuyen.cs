using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyPhanQuyen.Schema
{
    public class PhanQuyen
    {
        public int Id { get; set; }
        public string TenVaiTro { get; set; }
        public decimal TongQuyen { get; set; }
    }
}