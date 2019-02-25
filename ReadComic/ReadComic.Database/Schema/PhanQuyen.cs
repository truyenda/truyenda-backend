using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        [Key]
        public int Id { get; set; }

        public int Id_VaiTro { get; set; }
        public int Id_TaiKhoan { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual VaiTro VaiTro { get; set; }
    }
}