using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom.Schema
{
    public class DanhSachThanhVien
    {
        public int Id_NhomDich { get; set; }
        public List<ThanhVien> ThanhVienList { get; set; }
    }
}