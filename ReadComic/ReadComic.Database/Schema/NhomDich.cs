using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("NhomDich")]
    public class NhomDich : TableHaveIdInt
    {
        public NhomDich()
        {
            Truyens = new HashSet<Truyen>();
            TaiKhoans = new HashSet<TaiKhoan>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNhomDich { get; set; }

        public string MoTa { get; set; }

        [StringLength(256)]
        public string Logo { get; set; }

        public virtual ICollection<Truyen> Truyens { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}