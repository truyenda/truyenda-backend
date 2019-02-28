using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("Token")]
    public class Token : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_TaiKhoan { get; set; }

        public DateTime ThoiGianHetHan { get; set; }

        [StringLength(50)]
        public string TokenTaiKhoan { get; set; }

        [ForeignKey("Id_TaiKhoan")]
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }
    }
}