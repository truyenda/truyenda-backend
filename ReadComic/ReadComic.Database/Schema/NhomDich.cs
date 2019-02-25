using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("NhomDich")]
    public class NhomDich
    {
        public NhomDich()
        {
            Truyens = new HashSet<Truyen>();
        }
        [Key]
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

        [ForeignKey("Id_NhomTruong")]
        public virtual ThongTinNguoiDung ThongTinNguoiDung { get; set; }
    }
}