using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("PhanQuyen")]
    public class PhanQuyen : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TenVaiTro { get; set; }

        public ulong TongQuyen { get; set; }

        public int Id_TaiKhoan { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        
    }
}