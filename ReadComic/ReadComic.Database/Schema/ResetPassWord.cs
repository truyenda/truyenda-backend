using ReadComic.DataBase.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("ResetPassWord")]
    public class ResetPassWord : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_TaiKhoan { get; set; }

        public DateTime ThoiGianHetHan { get; set; }

        [StringLength(50)]
        public string TokenReset { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}