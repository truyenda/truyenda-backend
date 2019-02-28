using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("VaiTro")]
    public class VaiTro : TableHaveIdInt
    {
        public VaiTro()
        {
            PhanQuyens = new HashSet<PhanQuyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; }

        public decimal TongQuyen { get; set; }
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }

    }
}