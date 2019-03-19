using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TaiKhoan
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public int IdTrangThai { get; set; }

        [StringLength(50)]
        public int IdNhom { get; set; }

    }

    /// <summary>
    /// Class dùng để chứa thông tin tìm kiếm của danh sách tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TaiKhoanConditionSearch
    {
        public int IdNhom { get; set; }
        public int IdTrangThai { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }

        public TaiKhoanConditionSearch()
        {
            this.IdNhom = 0;
            this.IdTrangThai = 0;
            this.Username = "";
            this.Email = "";
            this.PageSize = 10;
            this.CurrentPage = 1;
        }
    }
}