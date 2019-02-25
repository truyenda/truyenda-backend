﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("LuuTheLoai")]
    public class LuuTheLoai
    {
        [Key]
        public int Id { get; set; }
        public int IdTruyen { get; set; }
        public int IdTheLoai { get; set; }

        [ForeignKey("IdTruyen")]
        public virtual Truyen Truyen { get; set; }
        [ForeignKey("IdTheLoai")]
        public virtual TheLoai TheLoai { get; set; }
    }
}