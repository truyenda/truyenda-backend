using ReadComic.DataBase.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("TheoDoiTruyen")]
    public class TheoDoiTruyen : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_NguoiDoc { get; set; }

        public int Id_Truyen { get; set; }

        public int? Id_ChuongDanhDau { get; set; }

        [ForeignKey("Id_NguoiDoc")]
        public virtual TaiKhoan TaiKhoan { get; set; }


        [ForeignKey("Id_ChuongDanhDau")]
        public virtual Chuong Chuong { get; set; }

        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }
    }
}