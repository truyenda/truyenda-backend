﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyLoaiTruyen.Schema
{
    /// <summary>
    /// Class dùng để chứa thông tin của một loại truyện
    /// Author       :   HoangNM - 10/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class LoaiTruyen
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TenLoaiTruyen { get; set; }

        public string MoTa { get; set; }

    }
}