﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("LoaiTruyen")]
    public class LoaiTruyen
    {
        public LoaiTruyen()
        {
            Truyens = new HashSet<Truyen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; }

        public virtual ICollection<Truyen> Truyens { get; set; }
    }
}