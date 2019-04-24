using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    /// <summary>
    /// Class dùng để lấy danh sách truyện
    /// Author       :   HoangNM - 1/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team HoangC#
    /// Version      :   1.0.0
    /// </remarks>
    public class DanhSachTruyen
    {
        public List<Truyen> listTruyen { set; get; }
        public Paging Paging { set; get; }

        public DanhSachTruyen()
        {
            this.listTruyen = new List<Truyen>();
            this.Paging = new Paging();
        }
    }
}