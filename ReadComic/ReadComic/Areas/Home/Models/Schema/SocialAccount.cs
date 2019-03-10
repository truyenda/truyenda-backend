using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Areas.Home.Models.Schema
{
    /// <summary>
    /// Class chứa các thuộc tính để convert từ thông tin lấy được của FB và GG
    /// Author       :   HoangNM - 03/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class SocialAccount
    {
        public string Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public DateTime? Birthday { set; get; }
        public bool Gender { set; get; }
        public string PhoneNumber { set; get; }
    }
}