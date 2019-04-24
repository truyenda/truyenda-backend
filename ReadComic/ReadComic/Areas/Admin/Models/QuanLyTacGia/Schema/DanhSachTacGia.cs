using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema
{
    /// <summary>
    /// Class dùng để lấy danh sách tác giả
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team HoangC#
    /// Version      :   1.0.0
    /// </remarks>
    public class DanhSachTacGia
    {
        public List<TacGia> listTacGia { set; get; }
        public Paging Paging { set; get; }

        public DanhSachTacGia()
        {
            this.listTacGia = new List<TacGia>();
            this.Paging = new Paging();
        }
    }
}