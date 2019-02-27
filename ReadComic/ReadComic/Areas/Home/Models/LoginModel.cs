﻿using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using ReadComic.Areas.Home.Models.Schema;

namespace ReadComic.Areas.Home.Models
{
    public class LoginModel
    {
        private DataContext context;

        public LoginModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Kiểm tra thông tin tài khoản người dùng nhập vào có đúng hay không
        /// Author       :   AnTM - 17/10/2018 - create
        /// </summary>
        /// <param name="account">Đối tượng chưa thông tin tài khoản</param>
        /// <returns>Đối tượng ResponseInfo chứa thông tin của việc kiểm tra</returns>
        public ResponseInfo CheckAccount(TaiKhoan account)
        {
            try
            {
                ResponseInfo result = new ResponseInfo();
                TblTaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Username == account.Username );
                
                if (taiKhoan == null)
                {
                    result.Code = 202;
                    //result.MsgNo = (int)MessageEnum.MsgNO.KhongCoTaiKhoan;
                    //result.MsgError = new Common.Common().GetErrorMessageById(result.MsgNo.ToString());
                }
                else if ((taiKhoan.hash_Pass) != BaoMat.GetMD5(account.Password+taiKhoan.salt_Pass))
                {
                   // result.MsgNo = (int)MessageEnum.MsgNO.MatKhauKhongDung;
                   // result.MsgError = new Common.Common().GetErrorMessageById(result.MsgNo.ToString());
                    
                    result.Code = 205;
                }
                else
                {
                    //Chứa thông tin chuỗi token
                    string token = Common.Common.GetToken(taiKhoan.Id);
                    TblToken tokenLG = new TblToken
                    {
                        Id_TaiKhoan = taiKhoan.Id,
                        TokenTaiKhoan = token,
                        ThoiGianHetHan = DateTime.Now.AddHours(12)
                    };
                    context.Tokens.Add(tokenLG);
                    result.IsSuccess = true;
                    //result.Data = new
                    //{
                    //    Profile = new Profile()
                    //    {
                    //        Id = taiKhoan.Id,
                    //        CMND = taiKhoan.User.CMND,
                    //        DiaChi = taiKhoan.User.DiaChi,
                    //        Email = taiKhoan.Email,
                    //        GioiTinh = taiKhoan.User.GioiTinh,
                    //        Ho = taiKhoan.User.Ho,
                    //        NgaySinh = (DateTime)taiKhoan.User.NgaySinh,
                    //        SoDienThoai = taiKhoan.User.SoDienThoai,
                    //        Ten = taiKhoan.User.Ten,
                    //        Avatar = taiKhoan.User.Avatar
                    //    },
                    //    Token = BaoMat.Base64Encode(token)
                    //};
                    context.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}