using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("LuuTacGia")]
    public class LuuTacGia
    {
        [Key]
        public int Id { get; set; }

        public int Id_Truyen { get; set; }

        public int Id_TacGia { get; set; }

        [ForeignKey("Id_Truyen")]
        public virtual Truyen Truyen { get; set; }
        [ForeignKey("Id_TacGia")]
        public virtual TacGia TacGia { get; set; }

    }
}