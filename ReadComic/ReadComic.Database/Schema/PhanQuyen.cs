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
        public PhanQuyen()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TenVaiTro { get; set; }

        public decimal TongQuyen { get; set; }

        public virtual ICollection<TaiKhoan>  TaiKhoans { get; set; }

        
    }
}