using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Common.Schema
{
    public class GetAccount
    {
        public int Id { get; set; }
        public int IdNhom { get; set; }
        public decimal TongQuyen { get; set; }
        public int IdQuyen { get; set; }
    }
}