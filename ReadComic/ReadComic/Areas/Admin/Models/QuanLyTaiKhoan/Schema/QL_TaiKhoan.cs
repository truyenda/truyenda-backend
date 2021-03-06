﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QL_TaiKhoan
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Email { get; set; }

        public int IdTrangThai { get; set; }

        public int IdNhom { get; set; }

        public string TenNhom { get; set; }

        public int IdQuyen { get; set; }

        public string TenQuyen { get; set; }


    }

    /// <summary>
    /// Class dùng để chứa thông tin tìm kiếm của danh sách tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    //public class TaiKhoanConditionSearch
    //{
    //    public int IdNhom { get; set; }
    //    public int IdTrangThai { get; set; }
    //    public string Username { get; set; }
    //    public string Email { get; set; }
    //    public int CurrentPage { set; get; }
    //    public string Token { get; set; }

    //    public TaiKhoanConditionSearch()
    //    {
    //        this.IdNhom = 0;
    //        this.IdTrangThai = 0;
    //        this.Username = "";
    //        this.Email = "";
    //        this.CurrentPage = 1;
    //    }
    //}
}