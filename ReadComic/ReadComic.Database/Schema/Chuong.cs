using ReadComic.Database.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("Chuong")]
    public class Chuong: TableHaveIdInt
    {
        public Chuong()
        {
            TheoDoiTruyens = new HashSet<TheoDoiTruyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_Truyen { get; set; }

        [Required]
        [StringLength(256)]
        public string TenChuong { get; set; }

        public float SoThuTu { get; set; }

        [Required]
        public string LinkAnh { get; set; }

        public long LuotXem { get; set; }

        public DateTime NgayTao { get; set; }

        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }

        public virtual ICollection<TheoDoiTruyen> TheoDoiTruyens { get; set; }
    }
}