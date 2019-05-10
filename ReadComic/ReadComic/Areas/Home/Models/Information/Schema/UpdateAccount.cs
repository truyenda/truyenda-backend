using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Information.Schema
{
    public class UpdateAccount
    {
        public string Ten { get; set; }

        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        public string New_Password { get; set; }

        public string Old_Password { get; set; }
    }
}