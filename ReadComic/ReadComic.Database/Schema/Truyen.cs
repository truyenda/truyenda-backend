using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("Truyen")]
    public class Truyen : TableHaveIdInt
    {
        public Truyen()
        {
            Chuongs = new HashSet<Chuong>();
            DanhGiaTruyens = new HashSet<DanhGiaTruyen>();
            LuuLoaiTruyens = new HashSet<LuuLoaiTruyen>();
            LuuTacGias = new HashSet<LuuTacGia>();
            BinhLuans = new HashSet<BinhLuan>();
            TheoDoiTruyens = new HashSet<TheoDoiTruyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_Nhom { get; set; }

        public int Id_ChuKy { get; set; }

        public int Id_TrangThai { get; set; }

        public int Id_LoaiTruyen { get; set; }

        [Required]
        [StringLength(256)]
        public string TenTruyen { get; set; }

        [StringLength(256)]
        public string TenKhac { get; set; }

        public int NamPhatHanh { get; set; }

        [Required]
        [StringLength(256)]
        public string AnhBia { get; set; }

        [Required]
        [StringLength(256)]
        public string AnhDaiDien { get; set; }

        public string MoTa { get; set; }

        
        public DateTime NgayTao { get; set; }

        [ForeignKey("Id_LoaiTruyen")]
        public virtual LoaiTruyen LoaiTruyen { get; set; }

        [ForeignKey("Id_TrangThai")]
        public virtual TrangThaiTruyen TrangThaiTruyen { get; set; }

        [ForeignKey("Id_Nhom")]
        public virtual NhomDich NhomDich { get; set; }

        public virtual ChuKyPhatHanh ChuKyPhatHanh { get; set; }

        public virtual ICollection<Chuong> Chuongs { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<LuuLoaiTruyen> LuuLoaiTruyens { get; set; }
        public virtual ICollection<LuuTacGia> LuuTacGias { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<TheoDoiTruyen> TheoDoiTruyens { get; set; }
    }
}