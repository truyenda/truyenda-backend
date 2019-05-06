using ReadComic.Areas.Admin.Models.HomeModel.Schema;
using ReadComic.Areas.Home.Models.HomeModel.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class SearchTruyen
    {

        public int Id { get; set; }

        public string TenTruyen { get; set; }

        public string TenKhac { get; set; }

        public int Id_TrangThai { get; set; }

        public string TrangThai { get; set; }

        public int Id_ChuKy { get; set; }

        public List<TacGia> DanhSachTacGia { get; set; }

        public List<TheLoai> DanhSachTheLoai { get; set; }

        public string AnhDaiDien { get; set; }

        public string AnhBia { get; set; }



        public int NamPhatHanh { get; set; }

        public string MoTa { get; set; }

        public int Id_Nhom { get; set; }

        public string TenNhom { get; set; }

        public long View { get; set; }

        public DateTime NgayTao { get; set; }
    }
}