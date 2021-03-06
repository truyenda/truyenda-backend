﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("TrangThaiTruyen")]
    public class TrangThaiTruyen : TableHaveIdInt
    {
        public TrangThaiTruyen()
        {
            Truyens = new HashSet<Truyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTrangThai { get; set; }

        public virtual ICollection<Truyen> Truyens { get; set; }
    }
}