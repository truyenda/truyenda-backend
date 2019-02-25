using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("Quyen")]
    public class Quyen
    {
        [Key]
        public int Id { get; set; }
        
        public int BitQuyen { get; set; }

        [StringLength(50)]
        public string TenQuyen { get; set; }
    }
}