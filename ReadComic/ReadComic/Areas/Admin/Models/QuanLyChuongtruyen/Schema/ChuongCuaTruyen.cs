using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một chương truyện
    /// Author       :   HoangNM - 04/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class ChuongCuaTruyen
    {
        public int Id { get; set; }
        public int IdTruyen { get; set; }
        public string TenChuong { get; set; }
        public float SoThuTu { get; set; }
        public string LinkAnh { get; set; }

    }

   
}