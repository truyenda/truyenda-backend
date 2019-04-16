using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyQuyen.Schema
{
    public class Quyen
    {
        public int Id { get; set; }
        public decimal BitQuyen { get; set; }
        public string TenQuyen { get; set; }
    }
}