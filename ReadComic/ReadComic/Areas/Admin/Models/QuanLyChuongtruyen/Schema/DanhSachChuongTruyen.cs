using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema
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
    public class DanhSachChuongTruyen
    {
        public List<GetChuongTruyen> listChuongTruyen { set; get; }
        public Paging Paging { set; get; }
        public ChuongConditionSearch Condition { set; get; }

        public DanhSachChuongTruyen()
        {
            this.listChuongTruyen = new List<GetChuongTruyen>();
            this.Condition = new ChuongConditionSearch();
            this.Paging = new Paging();
        }
    }
}