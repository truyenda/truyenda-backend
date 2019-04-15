using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema
{
    public class Truyen_TacGia
    {
        public int Id_Truyen { get; set; }

        public string TenTruyen { get; set; }

        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }
    }
}