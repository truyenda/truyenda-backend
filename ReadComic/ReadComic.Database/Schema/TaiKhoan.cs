using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("TaiKhoan")]
    public class TaiKhoan : TableHaveIdInt
    {
        public TaiKhoan()
        {
            PhanQuyens = new HashSet<PhanQuyen>();
            Bookmarks = new HashSet<Bookmark>();
            BinhLuans = new HashSet<BinhLuan>();
            DanhGiaTruyens = new HashSet<DanhGiaTruyen>();
            Tokens = new HashSet<Token>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_TrangThai { get; set; }

        public int Id_User { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string hash_Pass { get; set; }

        [Required]
        [StringLength(10)]
        public string salt_Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Id_Face { get; set; }

        [StringLength(50)]
        public string Id_Google { get; set; }



        

        [ForeignKey("Id_TrangThai")]
        public virtual TrangThaiTaiKhoan TrangThaiTaiKhoan { get; set; }

        [ForeignKey("Id_User")]
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }

        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }

    }
}