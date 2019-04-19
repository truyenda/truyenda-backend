using ReadComic.Areas.Home.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Information.Schema
{
    public class GetAccount
    {
        public int Id_TrangThai { get; set; }

        public string TenTrangThai { get; set; }

        public int Id_NhomDich { get; set; }

        public string TenNhom { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Id_Face { get; set; }

        public string Id_google { get; set; }

        public string Ten { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        public DateTime NgayHetHan { get; set; }

        public QuyenProFile Permissions { get; set; }

        public string Token { get; set; }

    }
}