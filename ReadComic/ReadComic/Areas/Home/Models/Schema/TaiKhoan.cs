using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    /// <summary>
    /// Class dùng để lấy data từ request của trang đăng nhập
    /// Author       :   HoangNM - 25/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TaiKhoan
    {
        [Required(ErrorMessage = "1")]
        [MaxLength(24, ErrorMessage = "2")]
        public string Username { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(50, ErrorMessage = "2")]
        public string Password { set; get; }
    }
}