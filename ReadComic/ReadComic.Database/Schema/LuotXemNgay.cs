using ReadComic.DataBase.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.Database.Schema
{
    [Table("LuotXemNgay")]
    public class LuotXemNgay
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        public int Id_Truyen { get; set; }

        public int  View { get; set; }

        public virtual Truyen Truyen { get; set; }
    }
}