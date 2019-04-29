using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema
{
    public class DanhSachNhom
    {
        public List<NhomDich> listNhomDich { set; get; }
        public Paging Paging { set; get; }

        public DanhSachNhom()
        {
            this.listNhomDich = new List<NhomDich>();
            this.Paging = new Paging();
        }
    }
}