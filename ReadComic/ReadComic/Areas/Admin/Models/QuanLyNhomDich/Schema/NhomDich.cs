using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một nhóm dịch
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class NhomDich
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TenNhomDich { get; set; }

        public string MoTa { get; set; }

        [StringLength(256)]
        public string Logo { get; set; }
    }
}