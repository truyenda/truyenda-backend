using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("TheLoai")]
    public class TheLoai
    {
        public TheLoai()
        {
            LuuTheLoais = new HashSet<LuuTheLoai>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TenTheLoai { get; set; }

        public string Mota { get; set; }

        public virtual ICollection<LuuTheLoai> LuuTheLoais { get; set; }
    }
}