using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using TblTheoDoiTruyen = ReadComic.Database.Schema.TheoDoiTruyen;

namespace ReadComic.Areas.Home.Models.TheoDoiTruyen
{
    public class TheoDoiTruyenModel
    {
        private DataContext context;

        public TheoDoiTruyenModel()
        {
            context = new DataContext();
        }

        public void TheoDoiTruyen(int id_Truyen)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                
                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                string Token = BaoMat.Base64Decode(token);
                TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == Token);
                int ID_TaiKhoan = TblToken.Id_TaiKhoan;
                context.TheoDoiTruyens.Add(new TblTheoDoiTruyen
                {
                    Id_NguoiDoc = ID_TaiKhoan,
                    Id_Truyen = id_Truyen
                });
                context.SaveChanges();

            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}