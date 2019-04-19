using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    public class QuyenProFile
    {
        public int Id_Quyen { get; set; }
        public string TenQuyen { get; set; }
    }

    public class Profile
    {
        public int Id_TrangThai { get; set; }
        public string TenTrangThai { get; set; }

        public int Id_NhomDich { get; set; }
        public string TenNhomDich { get; set; }

        public string Username { get; set; }
        public string Email { set; get; }

        public string Id_Face { get; set; }
        public string Id_Google { get; set; }

        public string Ten { set; get; }
        public DateTime NgaySinh { set; get; }
        public bool GioiTinh { set; get; }
        public DateTime NgayHetHan { get; set; }

        public QuyenProFile Permissions { get; set; }






    }
}