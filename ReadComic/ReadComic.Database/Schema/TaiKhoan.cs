using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string hash_Pass { get; set; }
        [StringLength(50)]
        public string salt_Pass { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }

    }
}