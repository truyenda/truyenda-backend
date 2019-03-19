using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin để cập nhật trạng thái tài khoản
    /// Author       :   HoangNM - 19/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TrangThaiTaiKhoan
    {
        public int Id { get; set; }
        
        public int IdTrangThai { get; set; }
    }
}