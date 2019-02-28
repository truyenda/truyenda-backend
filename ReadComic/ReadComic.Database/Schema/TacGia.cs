using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("TacGia")]
    public class TacGia : TableHaveIdInt
    {
        public TacGia()
        {
            LuuTacGias = new HashSet<LuuTacGia>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTacGia { get; set; }

        public virtual ICollection<LuuTacGia> LuuTacGias { get; set; }
    }
}