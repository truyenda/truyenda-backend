using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("Truyen")]
    public class Truyen
    {
        public Truyen()
        {
            Chuongs = new HashSet<Chuong>();
            DanhGiaTruyens = new HashSet<DanhGiaTruyen>();
            Bookmarks = new HashSet<Bookmark>();
            LuuTheLoais = new HashSet<LuuTheLoai>();
            LuuTacGias = new HashSet<LuuTacGia>();
        }

        [Key]
        public int Id { get; set; }

        public int Id_Nhom { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTruyen { get; set; }

        [StringLength(50)]
        public string TenKhac { get; set; }

        [Required]
        [StringLength(256)]
        public string DuongDan { get; set; }

        public int NamPhatHanh { get; set; }
        [Required]

        public string AnhBia { get; set; }
        [Required]
        [StringLength(256)]
        public string AnhDaiDien { get; set; }
        public string MoTa { get; set; }

        public int Id_ChuKy { get; set; }
        public int Id_TrangThai { get; set; }
        public int Id_LoaiTruyen { get; set; }
        public DateTime NgayTao { get; set; }

        [ForeignKey("Id_LoaiTruyen")]
        public virtual LoaiTruyen LoaiTruyen { get; set; }

        [ForeignKey("Id_TrangThai")]
        public virtual TrangThaiTruyen TrangThaiTruyen { get; set; }

        [ForeignKey("Id_Nhom")]
        public virtual NhomDich NhomDich { get; set; }

        public virtual ICollection<Chuong> Chuongs { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<LuuTheLoai> LuuTheLoais { get; set; }
        public virtual ICollection<LuuTacGia> LuuTacGias { get; set; }
    }
}