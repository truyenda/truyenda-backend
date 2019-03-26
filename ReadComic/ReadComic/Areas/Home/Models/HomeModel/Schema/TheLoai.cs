using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của thể loại
    /// Author       :   HoangNM - 26/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   HomeModel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TheLoai
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTheLoai { get; set; }

        public string MoTa { get; set; }
    }
}