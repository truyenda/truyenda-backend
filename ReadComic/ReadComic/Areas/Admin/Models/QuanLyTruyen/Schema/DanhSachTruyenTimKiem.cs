using ReadComic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class DanhSachTruyenTimKiem
    {
        public List<SearchTruyen> listTruyen { set; get; }
        public Paging Paging { set; get; }

        public DanhSachTruyenTimKiem()
        {
            this.listTruyen = new List<SearchTruyen>();
            this.Paging = new Paging();
        }
    }
}