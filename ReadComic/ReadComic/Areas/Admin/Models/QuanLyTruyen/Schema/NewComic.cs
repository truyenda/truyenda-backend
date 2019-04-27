using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class NewComic
    {
        [Required]
        [StringLength(256)]
        public string TenTruyen { get; set; }

        [StringLength(256)]
        public string TenKhac { get; set; }

        public List<int> TheLoai { get; set; }

        public string TacGia { get; set; }

        
        public int Id_TrangThai { get; set; }

        public int NamPhatHanh { get; set; }

        public int Id_ChuKy { get; set; }


        public string AnhBia { get; set; }

        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }


    }
}