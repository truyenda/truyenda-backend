using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema
{
    /// <summary>
    /// Class dùng get thông tin của một chương truyện
    /// Author       :   HoangNM - 14/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class GetChuongTruyen
    {
        public int Id { get; set; }
        public string TenTruyen { get; set; }
        public int IdTruyen { get; set; }
        public string TenChuong { get; set; }
        public int IdNhomDich { get; set; }
        public string TenNhomDich { get; set; }
        public float SoThuTu { get; set; }
        public string LinkAnh { get; set; }
        public long LuotXem { get; set; }
        public DateTime NgayTao { get; set; }
    }

    /// <summary>
    /// Class dùng để chứa thông tin tìm kiếm của danh sách chương truyện
    /// Author       :   HoangNM - 04/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlAdmin.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class ChuongConditionSearch
    {
        public int CurrentPage { set; get; }
        public float SoThuTu { get; set; }
        public string TenChuong { get; set; }
        public ChuongConditionSearch()
        {
            this.CurrentPage = 1;
            this.SoThuTu = 0;
            this.TenChuong = "";
        }
    }
}