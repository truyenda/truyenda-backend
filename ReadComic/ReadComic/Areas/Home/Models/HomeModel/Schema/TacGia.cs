using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.HomeModel.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của tác giả
    /// Author       :   HoangNM - 26/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   HomeModel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TacGia
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TenTacGia { get; set; }
    }
}