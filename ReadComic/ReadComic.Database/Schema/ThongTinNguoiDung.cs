using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("ThongTinNguoiDung")]
    public class ThongTinNguoiDung
    {
        public ThongTinNguoiDung()
        {
            TrangThaiResets = new HashSet<TrangThaiReset>();
            TrangThaiTaiKhoans = new HashSet<TrangThaiTaiKhoan>();
            PhanQuyens = new HashSet<PhanQuyen>();
            Bookmarks = new HashSet<Bookmark>();
            NhomDiches = new HashSet<NhomDich>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        [StringLength(50)]
        public string Id_Face { get; set; }
        
        public string token_EmailResetPass { get; set; }

        public int Id_TrangThaiTK { get; set; }
        public int Id_TrangThaiReset { get; set; }

        [ForeignKey("Id_TrangThaiReset")]
        public virtual ICollection<TrangThaiReset> TrangThaiResets { get; set; }
        [ForeignKey("Id_TrangThaiTK")]
        public virtual ICollection<TrangThaiTaiKhoan> TrangThaiTaiKhoans { get; set; }

        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<NhomDich> NhomDiches { get; set; }
    }
}