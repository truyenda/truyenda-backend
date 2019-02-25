using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("ChuKyPhatHanh")]
    public class ChuKyPhatHanh
    {
        public ChuKyPhatHanh()
        {
            Truyens = new HashSet<Truyen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChuKy { get; set; }

        public ICollection<Truyen> Truyens { get; set; }
    }
}