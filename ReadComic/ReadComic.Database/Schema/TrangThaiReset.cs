using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("TrangThaiReset")]
    public class TrangThaiReset
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string TenTrangThai { get; set; }

        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }
    }
}