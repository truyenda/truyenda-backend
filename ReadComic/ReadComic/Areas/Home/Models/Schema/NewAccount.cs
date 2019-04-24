using ReadComic.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    /// <summary>
    /// Class thông tin của việc đăng ký tài khoản.
    /// Author       :   HoangNM - 28/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class NewAccount
    {
        [Required(ErrorMessage = "1")]
        [MaxLength(34, ErrorMessage = "2")]
        [RegularExpression("^[a-zA-Z0-9_.-]{8,24}$", ErrorMessage = "34")]
        public string Username { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(32, ErrorMessage = "2")]
        public string Ten { set; get; }

        [Required(ErrorMessage = "1")]
        public bool GioiTinh { set; get; }

        [Required(ErrorMessage = "1")]
        public DateTime NgaySinh { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(255, ErrorMessage = "2")]
        [EmailAddress(ErrorMessage = "5")]
        public string Email { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(50, ErrorMessage = "2")]
        // [RegularExpression("^(?=.*\\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{6,32}$", ErrorMessage = "19")]
        [RegularExpression("^[a-zA-Z0-9_.-]{6,32}$", ErrorMessage = "34")]
        public string Password { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(50, ErrorMessage = "2")]
        [Compare("Password", ErrorMessage = "20")]
        public string ConfirmPassword { set; get; }

        //[AgreeValidate(ErrorMessage = "35")]
        //public bool Agree { set; get; }
    }
}