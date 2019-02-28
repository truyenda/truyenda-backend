using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReadComic.DataBase.Schema
{
    [Table("TrangThaiTaiKhoan")]
    public class TrangThaiTaiKhoan : TableHaveIdInt
    {
        public TrangThaiTaiKhoan()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTrangThai { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}