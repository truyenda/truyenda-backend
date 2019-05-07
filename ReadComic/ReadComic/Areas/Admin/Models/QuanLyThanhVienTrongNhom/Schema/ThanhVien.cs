using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom.Schema
{
    public class ThanhVien
    {
        public int Id_TaiKhoanThanhVien { get; set; }
        public string Username { get; set; }
        public int Id_Role { get; set; }
    }
}