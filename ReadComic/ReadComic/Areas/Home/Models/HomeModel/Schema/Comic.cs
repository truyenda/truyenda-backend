using ReadComic.Areas.Admin.Models.HomeModel.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một truyện
    /// Author       :   HoangNM - 26/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   HomeModel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class Comic
    {
        public int Id { get; set; }

        
        public string TenTruyen { get; set; }

        public string TenKhac { get; set; }

        public int NamPhatHanh { get; set; }

        public string AnhBia { get; set; }

        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }

        public DateTime NgayTao { get; set; }

        public string TrangThai { get; set; }

        public string ChuKyPhatHanh { get; set; }

        public List<TacGia> DanhSachTacGia { get; set; }

        public List<TheLoai> DanhSachTheLoai { get; set; }

    }
}