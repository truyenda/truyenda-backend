using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("Bookmark")]
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }
        public int Id_NguoiDoc { get; set; }
        public int Id_truyen { get; set; }
        public int Id_ChuongDanhDau { get; set; }

        [ForeignKey("Id_NguoiDoc")]
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }

        [ForeignKey("Id_truyen")]
        public virtual Truyen Truyen { get; set; }

        [ForeignKey("Id_ChuongDanhDau")]
        public virtual Chuong Chuong { get; set; }
    }
}