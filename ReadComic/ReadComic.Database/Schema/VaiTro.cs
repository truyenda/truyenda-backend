using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("VaiTro")]
    public class VaiTro
    {
        public VaiTro()
        {
            PhanQuyens = new HashSet<PhanQuyen>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string TenVaiTro { get; set; }

        public int TongQuyen { get; set; }
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }

    }
}