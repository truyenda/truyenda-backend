using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("NhomDich")]
    public class NhomDich
    {
        public NhomDich()
        {
            Truyens = new HashSet<Truyen>();
            ThongTinNguoiDungs = new HashSet<ThongTinNguoiDung>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_NhomTruong { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNhomDich { get; set; }

        public string MoTa { get; set; }

        [StringLength(256)]
        public string Website { get; set; }

        [StringLength(256)]
        public string Logo { get; set; }

        public virtual ICollection<Truyen> Truyens { get; set; }

        public virtual ICollection<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
    }
}