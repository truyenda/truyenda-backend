using ReadComic.Database.Schema;
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
            TheoDoiTruyens = new HashSet<TheoDoiTruyen>();
            BinhLuans = new HashSet<BinhLuan>();
            DanhGiaTruyens = new HashSet<DanhGiaTruyen>();
            Tokens = new HashSet<Token>();
            ResetPassWords = new HashSet<ResetPassWord>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("ThongTinNguoiDung")]
        public int Id { get; set; }

        public int Id_TrangThai { get; set; }

       
        //public int Id_User { get; set; }

        public int Id_NhomDich { get; set; }

        [Required]
        [StringLength(24)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string hash_Pass { get; set; }

        [Required]
        [StringLength(10)]
        public string salt_Pass { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Id_Face { get; set; }

        [StringLength(256)]
        public string Id_Google { get; set; }



        

        [ForeignKey("Id_TrangThai")]
        public virtual TrangThaiTaiKhoan TrangThaiTaiKhoan { get; set; }

        
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }

        public virtual NhomDich NhomDich { get; set; }

        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<TheoDoiTruyen> TheoDoiTruyens { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<ResetPassWord> ResetPassWords { get; set; }

    }
}