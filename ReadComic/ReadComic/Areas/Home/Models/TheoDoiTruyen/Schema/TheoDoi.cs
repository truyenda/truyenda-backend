using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.TheoDoiTruyen.Schema
{
    public class TheoDoi
    {
        public int Id_TaiKhoan { get; set; }

        public List<BookMark> ListBookmark { get; set; }
    }
}