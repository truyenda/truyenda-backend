using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class SearchTruyen
    {
        public int Id { get; set; }

        public int Id_TrangThai { get; set; }

        public int Id_ChuKy { get; set; }

        public string TenTruyen { get; set; }

        public string AnhDaiDien { get; set; }

        public string AnhBia { get; set; }

        public string TenNhom { get; set; }

        public long View { get; set; }

        public DateTime NgayTao { get; set; }
    }
}