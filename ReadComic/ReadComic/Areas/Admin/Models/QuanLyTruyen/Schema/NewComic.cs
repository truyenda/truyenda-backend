using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class NewComic
    {
        public int Id { get; set; }

        public int Id_Nhom { get; set; }

        public int Id_TrangThai { get; set; }

        public int Id_ChuKy { get; set; }

        [Required]
        [StringLength(256)]
        public string TenTruyen { get; set; }

        [Required]
        [StringLength(256)]
        public string TenKhac { get; set; }

        public int NamPhatHanh { get; set; }

        public string AnhBia { get; set; }

        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }

        public string AnhDaiDienName { get; set; }

        public string AnhBiaName { get; set; }

    }
}