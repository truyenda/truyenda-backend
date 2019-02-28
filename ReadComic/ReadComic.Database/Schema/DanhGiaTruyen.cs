using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("DanhGiaTruyen")]
    public class DanhGiaTruyen : TableHaveIdInt
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_NguoiDanhGia { get; set; }

        public int Id_Truyen { get; set; }

        public int Diem { get; set; }

        [ForeignKey("Id_NguoiDanhGia")]
        public virtual TaiKhoan TaiKhoan { get; set; }
        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }
    }
}