using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    public class Profile
    {
        [Required(ErrorMessage = "1")]
        public int Id { get; set; }

        [Required(ErrorMessage = "1")]
        [MaxLength(50, ErrorMessage = "2")]
        public string Ten { set; get; }

        

        public bool GioiTinh { set; get; }

        public DateTime NgaySinh { set; get; }

        [Required(ErrorMessage = "1")]
        [MaxLength(255, ErrorMessage = "2")]
        [EmailAddress(ErrorMessage = "5")]
        public string Email { set; get; }

    }
}