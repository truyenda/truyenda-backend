﻿using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Linq;
using System.Data.Entity;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using TblTokenResset = ReadComic.Database.Schema.ResetPassWord;
using TblUser = ReadComic.DataBase.Schema.ThongTinNguoiDung;
using ReadComic.Areas.Home.Models.Schema;
using ReadComic.Common.Enum;
using System.Net.Http;
using Newtonsoft.Json;
using EntityFramework.Extensions;
using ReadComic.Common.ErrorMsg;

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
        /// Author       :   HoangNM - 28/02/2019 - create
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
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.KhongCoTaiKhoan);
                    result.Code = 400;
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                else if ((taiKhoan.hash_Pass) != BaoMat.GetMD5(BaoMat.GetSimpleMD5(account.Password), taiKhoan.salt_Pass))
                {
                    result.Code = 400;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.MatKhauSai);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                else
                {
                    //Chứa thông tin chuỗi token
                    string token = Common.Common.GetToken(taiKhoan.Id);
                    TblToken tokenLG = new TblToken
                    {
                        Id_TaiKhoan = taiKhoan.Id,
                        TokenTaiKhoan = token,
                        ThoiGianHetHan = DateTime.Now.AddHours(24)
                    };
                    context.Tokens.Add(tokenLG);
                    context.SaveChanges();
                    result.IsSuccess = true;
                    result.Data = new
                    {
                        Profile = new Profile()
                        {
                            Id_TrangThai = taiKhoan.Id_TrangThai,
                            TenTrangThai=taiKhoan.TrangThaiTaiKhoan.TenTrangThai,
                            Id_NhomDich=taiKhoan.Id_NhomDich,
                            TenNhomDich=taiKhoan.NhomDich.TenNhomDich,
                            Username=taiKhoan.Username,
                            Email = taiKhoan.Email,
                            Id_Face=taiKhoan.Id_Face,
                            Id_Google=taiKhoan.Id_Google,
                            GioiTinh = taiKhoan.ThongTinNguoiDung.GioiTinh,
                            Ten = taiKhoan.ThongTinNguoiDung.Ten,
                            NgaySinh = (DateTime)taiKhoan.ThongTinNguoiDung.NgaySinh,
                            NgayHetHan= tokenLG.ThoiGianHetHan,
                            Permissions=new QuyenProFile
                            {
                                TenvaiTro=taiKhoan.PhanQuyen.TenVaiTro,
                                Id_VaiTro=taiKhoan.PhanQuyen.Id

                            }
                        },
                        Token = BaoMat.Base64Encode(token)
                    };
                    result.ThongTinBoSung1 = BaoMat.Base64Encode(token);
                    

                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.DangNhapThanhCong);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Xóa token login của user khi user logout
        /// Author       :   HoangNM - 03/03/2019 - create
        /// </summary>
        /// <returns>true nếu xóa thành công</returns>
        public ResponseInfo RemoveToken(string token)
        {
            ResponseInfo result = new ResponseInfo();
            try
            {
                token = BaoMat.Base64Decode(token);
                context.Tokens.Where(x => x.TokenTaiKhoan == token).Delete();
                context.Tokens.Where(x => x.ThoiGianHetHan < DateTime.Now).Delete();
                
                
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.DangXuatThanhCong);
                result.TypeMsgError = errorMsg.Type;
                result.MsgError = errorMsg.Msg;

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Lấy thông tin cá nhân của người dùng từ facebook.
        /// Author       :   HoangNM - 03/03/2019 - create
        /// </summary>
        /// <param name="accessToken">Token được facebook cung cấp để truy cập</param>
        /// <returns>Đối tượng chưa các thông tin đã lấy được từ facebook</returns>
        public SocialAccount LayThongTinFB(string accessToken)
        {
            try
            {
                //Gọi api của facebook để lấy thông tin
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://graph.facebook.com/v3.0");
                HttpResponseMessage response = client.GetAsync($"me?fields=id,first_name,last_name,name,birthday,gender,email&access_token={accessToken}").Result;
                response.EnsureSuccessStatusCode();
                string fbInfo = response.Content.ReadAsStringAsync().Result;
                var jsonRes = JsonConvert.DeserializeObject<dynamic>(fbInfo);

                // Chuyển dữ liệu động lấy ddowwcj từ facebook sang đối tượng cần thiết của hệ thống
                SocialAccount facebookAccount = new SocialAccount();
                var a = jsonRes["birthday"];
                facebookAccount.Id = jsonRes["id"] != null ? jsonRes["id"].ToString() : "";
                facebookAccount.FirstName = jsonRes["first_name"] != null ? jsonRes["first_name"].ToString() : "";
                facebookAccount.LastName = jsonRes["last_name"] != null ? jsonRes["last_name"].ToString() : "";
                facebookAccount.Name = jsonRes["name"] != null ? jsonRes["name"].ToString() : "";
                string birthday = jsonRes["birthday"] != null ? jsonRes["birthday"].ToString() : "";
                DateTime dateOfBirth = DateTime.Today;
                if (birthday.Length > 0 && birthday.Length == 4)
                {
                    dateOfBirth = new DateTime(Convert.ToInt32(birthday), 1, 1);
                }
                if (birthday.Length > 0 && birthday.Length == 10)
                {
                    dateOfBirth = new DateTime(Convert.ToInt32(birthday.Substring(6, 10)),
                        Convert.ToInt32(birthday.Substring(0, 2)),
                        Convert.ToInt32(birthday.Substring(3, 5)));
                }
                facebookAccount.Birthday = dateOfBirth;
                facebookAccount.Email = jsonRes["email"] != null ? jsonRes["email"].ToString() : "";
                facebookAccount.Gender = jsonRes["gender"] != null ? jsonRes["gender"].ToString() == "female" ? false : true : true;
                facebookAccount.PhoneNumber = jsonRes["phone_number"] != null ? jsonRes["phone_number"].ToString() : "";
                return facebookAccount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// So khớp thông tin đăng nhập có thừ FB hoặc GG với thông tin tài khoản của hệ thống.
        /// Author       :   Hoang - 30/04/2019 - create
        /// </summary>
        /// <param name="socialAccount">Thông tin cá nhân lấy được từ FB </param>
        /// <returns>Đối tượng chứ token login của tài khoản trong hệ thống</returns>
        public TblToken CheckSocialAccount(SocialAccount socialAccount)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                if (socialAccount.Id != "")
                {
                    TblTaiKhoan account = context.TaiKhoans.FirstOrDefault(x => x.Email == socialAccount.Email && !x.DelFlag);
                    if (account == null)
                    {
                        account = context.TaiKhoans.FirstOrDefault(x => (
                            x.Id_Face == socialAccount.Id ) && !x.DelFlag);
                        if (account != null && socialAccount.Email != "")
                        {
                            account.Email = socialAccount.Email;
                        }
                    }
                    else
                    {
                        
                            account.Id_Face = socialAccount.Id;
                        
                    }
                    if (account == null)
                    {
                        TblUser user = new TblUser
                        {
                            Ten = socialAccount.FirstName+" "+socialAccount.LastName,
                            GioiTinh = socialAccount.Gender,
                            NgaySinh = socialAccount.Birthday,
                        };
                        context.ThongTinNguoiDungs.Add(user);
                        context.SaveChanges();
                        account = new TblTaiKhoan
                        {
                            Username = "",
                            hash_Pass = "",
                            salt_Pass="",
                            Email = socialAccount.Email,
                            Id_Face= socialAccount.Id
                    };
                        
                            
                        
                        context.TaiKhoans.Add(account);
                    }
                    TblToken tokenLogin = new TblToken
                    {
                        TokenTaiKhoan = Common.Common.GetToken(account.Id),
                        ThoiGianHetHan = DateTime.Now.AddHours(12)
                    };
                    account.Tokens.Add(tokenLogin);
                    context.SaveChanges();
                    transaction.Commit();
                    return tokenLogin;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        //Lấy lại mật khẩu
        public ResponseInfo ForgotPassword(SendEMail email)
        {
            ResponseInfo result = new ResponseInfo();
            try
            {
                var taikhoan = context.TaiKhoans.FirstOrDefault(x => x.Email == email.email && !x.DelFlag);
                string Username = taikhoan.Username;

                string token = Common.Common.GetToken(taikhoan.Id);
                TblTokenResset tokenLG = new TblTokenResset
                {
                    Id_TaiKhoan = taikhoan.Id,
                    TokenReset = token,
                    ThoiGianHetHan = DateTime.Now.AddHours(12)
                };
                context.ResetPassWords.Add(tokenLG);
                context.SaveChanges();
                token = BaoMat.Base64Encode(token);
                string Subject = "Password Reset Confirmation for "+ Username;
                string body = "<p>There was recently a request to change the password for your account. </p>" +
                    "<p>If you requested this password change, please reset your password here: </p>" +
                    "<p> https://truyenda.tk/forgot?token=" + token+ "</p>"+
                    "<p>If you did not make this request, you can ignore this message and your password will remain the same.</p>";
                SendMail.SendGird(email.email,body, Subject);
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.GuiEmailThanhCong);
                result.TypeMsgError = errorMsg.Type;
                result.MsgError = errorMsg.Msg;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Lấy lại mật khẩu
        public ResponseInfo ChangePassword(ChangePass data)
        {
            ResponseInfo result = new ResponseInfo();
            try
            {
                string Token = BaoMat.Base64Decode(data.tokenReset);
                TblTokenResset resetPassWord = context.ResetPassWords.FirstOrDefault(x => x.TokenReset == Token  && !x.DelFlag);
                if (resetPassWord == null)
                {
                    result.Code = 400;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThayDoiMatKhauThatBai);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                else if (resetPassWord.ThoiGianHetHan < DateTime.Now)
                {
                    result.Code = 400;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.TokenResetHetHan);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                else
                {
                    string Hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(data.NewPass), context.TaiKhoans.Where(x => x.Id == resetPassWord.Id_TaiKhoan && !x.DelFlag).FirstOrDefault().salt_Pass);
                    //cập nhật mật khẩu
                    context.TaiKhoans.Where(x => x.Id == resetPassWord.Id_TaiKhoan && !x.DelFlag).Update(y => new TblTaiKhoan
                    {
                        hash_Pass = Hash_Pass

                    });
                    context.ResetPassWords.Where(x => x.TokenReset == Token).Delete();
                    context.ResetPassWords.Where(x => x.ThoiGianHetHan < DateTime.Now).Delete();
                    context.SaveChanges();
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThayDoiMatKhauThanhCong);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                    return result;
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