using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("BinhLuan")]
    public class BinhLuan : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_TaiKhoan { get; set; }

        public int Id_Truyen { get; set; }

        [Required]
        [StringLength(1000)]
        public string NoiDung { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual TaiKhoan TaiKhoan { get; set; }

        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }
    }
}