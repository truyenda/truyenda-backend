using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("LoaiTruyen")]
    public class LoaiTruyen : TableHaveIdInt
    {
        public LoaiTruyen()
        {
            LuuLoaiTruyen = new HashSet<LuuLoaiTruyen>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TenTheLoai { get; set; }

        public string Mota { get; set; }

        public virtual ICollection<LuuLoaiTruyen> LuuLoaiTruyen { get; set; }
    }
}