using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema
{
    public class JsonTruyen
    {
        //tên truyện
        public string name { get; set; }
        //tên khác
        public List<string> oname { get; set; }

        //ảnh đại diện
        public string thumb { get; set; }

        //mô tả
        public string description { get; set; }

        //trạng thái truyện
        public string state { get; set; }

        //thể loại của truyện
        public List<string> cate { get; set; }

        //tác giả của truyện
        public List<string> authors { get; set; }

        //chương truyện
        public List<Chapter> chapters { get; set; }


    }
}