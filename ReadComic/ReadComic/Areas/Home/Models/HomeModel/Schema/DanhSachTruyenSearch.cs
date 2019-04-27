using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.HomeModel.Schema
{
    public class DanhSachTruyenSearch
    {
        public List<Comic> listComic { set; get; }
        public Paging Paging { set; get; }

        public DanhSachTruyenSearch()
        {
            this.listComic = new List<Comic>();
            this.Paging = new Paging();
        }
    }
}