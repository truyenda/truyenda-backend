using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema
{
    /// <summary>
    /// Class dùng để lấy danh sách tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team HoangC#
    /// Version      :   1.0.0
    /// </remarks>
    public class DanhSachTaiKhoan
    {
        public List<TaiKhoan> listTaiKhoan { set; get; }
        public Paging Paging { set; get; }
        public TaiKhoanConditionSearch Condition { set; get; }

        public DanhSachTaiKhoan()
        {
            this.listTaiKhoan = new List<TaiKhoan>();
            this.Condition = new TaiKhoanConditionSearch();
            this.Paging = new Paging();
        }
    }
}