using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("LuuLoaiTruyen")]
    public class LuuLoaiTruyen : TableHaveIdInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdTruyen { get; set; }

        public int IdLoaiTruyen { get; set; }

        [ForeignKey("IdTruyen")]
        public virtual Truyen Truyen { get; set; }

        [ForeignKey("IdTheLoai")]
        public virtual LoaiTruyen LoaiTruyen { get; set; }
    }
}