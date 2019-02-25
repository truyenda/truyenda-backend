using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("DanhGiaTruyen")]
    public class DanhGiaTruyen
    {
        

        [Key]
        public int Id { get; set; }
        public int Id_NguoiDanhGia { get; set; }
        public int Id_Truyen { get; set; }
        public int Diem { get; set; }

        [ForeignKey("Id_NguoiDanhGia")]
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }
        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }
    }
}