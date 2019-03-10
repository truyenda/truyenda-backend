using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("ErrorMsg")]
    public class ErrorMsg : TableHaveIdInt
    {
        [Key]
        public int Id { get; set; }

        public int Type { get; set; }

        [Required]
        [StringLength(256)]
        public string mgs { get; set; }
    }
}