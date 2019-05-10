using ReadComic.Database.Schema;
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
            LuotXemNgay = new HashSet<LuotXemNgay>();
            LuotXemTuan = new HashSet<LuotXemTuan>();
            LuotXemThang = new HashSet<LuotXemThang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_Nhom { get; set; }

        public int Id_ChuKy { get; set; }

        public int Id_TrangThai { get; set; }

        [Required]
        [StringLength(256)]
        public string TenTruyen { get; set; }

        [StringLength(256)]
        public string TenKhac { get; set; }

        public int NamPhatHanh { get; set; }

       

        public string MoTa { get; set; }

        [Required]
        [StringLength(500)]
        public string anhBia { get; set; }

        [Required]
        [StringLength(500)]
        public string anhDaiDien { get; set; }


        public DateTime NgayTao { get; set; }


        [ForeignKey("Id_TrangThai")]
        public virtual TrangThaiTruyen TrangThaiTruyen { get; set; }

        [ForeignKey("Id_Nhom")]
        public virtual NhomDich NhomDich { get; set; }

        [ForeignKey("Id_ChuKy")]
        public virtual ChuKyPhatHanh ChuKyPhatHanh { get; set; }

        

        public virtual ICollection<Chuong> Chuongs { get; set; }
        public virtual ICollection<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual ICollection<LuuLoaiTruyen> LuuLoaiTruyens { get; set; }
        public virtual ICollection<LuuTacGia> LuuTacGias { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<TheoDoiTruyen> TheoDoiTruyens { get; set; }
        public virtual ICollection<LuotXemNgay> LuotXemNgay { get; set; }
        public virtual ICollection<LuotXemTuan> LuotXemTuan { get; set; }
        public virtual ICollection<LuotXemThang> LuotXemThang { get; set; }
    }
}