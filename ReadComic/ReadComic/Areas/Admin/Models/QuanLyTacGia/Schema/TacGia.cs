using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một tác giả
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
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

    /// <summary>
    /// Class dùng để chứa thông tin tìm kiếm của danh sách tài khoản
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class TacGiaConditionSearch
    {
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }

        public TacGiaConditionSearch()
        {
            this.PageSize = 10;
            this.CurrentPage = 1;
        }
    }
}