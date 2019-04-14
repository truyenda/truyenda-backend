using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một truyện
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class Truyen
    {
        public int Id { get; set; }

        public int Id_TrangThai { get; set; }

        public int Id_ChuKy { get; set; }

        public string TenTruyen { get; set; }

        public string AnhDaiDien { get; set; }

        public string TenNhom { get; set; }
    }

    /// <summary>
    /// Class dùng để chứa thông tin tìm kiếm của danh sách truyện
    /// Author       :   HoangNM - 1/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlAdmin.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TruyenConditionSearch
    {
        public int CurrentPage { set; get; }
        public int IdNhom { get; set; }
        public int IdTrangThai { get; set; }
        public int IdChuKy { get; set; }
        public string TenTruyen { get; set; }
        public TruyenConditionSearch()
        {
            this.CurrentPage = 1;
            this.IdNhom = 0;
            this.IdChuKy = 0;
            this.IdTrangThai = 0;
            this.TenTruyen = "";
        }
    }
}