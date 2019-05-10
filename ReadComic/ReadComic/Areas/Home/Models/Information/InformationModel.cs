using EntityFramework.Extensions;
using ReadComic.Areas.Home.Models.Information.Schema;
using ReadComic.Areas.Home.Models.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using TblUser = ReadComic.DataBase.Schema.ThongTinNguoiDung;

namespace ReadComic.Areas.Home.Models.Information
{
    public class InformationModel
    {
        private DataContext context;

        public InformationModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy thông tin tài khoản đang đăng nhập
        /// Author       :   HoangNM - 29/03/2019 - create
        /// </summary>
        /// <param name="token">
        /// token của account đăng nhập.
        /// </param>
        /// <returns>
        /// Thông tin tài khoản
        /// </returns>

        public GetAccount GetAccount(string token)
        {
            string Token = BaoMat.Base64Decode(token);
            TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == Token);
            GetAccount getAccount = context.TaiKhoans.Where(x => x.Id == TblToken.Id_TaiKhoan && !x.DelFlag).Select(x => new GetAccount
            {
                Id_TaiKhoan=x.Id,
                Email = x.Email,
                GioiTinh = x.ThongTinNguoiDung.GioiTinh,
                Id_Face = x.Id_Face,
                Id_google = x.Id_Google,
                Id_TrangThai = x.Id_TrangThai,
                TenTrangThai = x.TrangThaiTaiKhoan.TenTrangThai,
                Id_NhomDich = x.Id_NhomDich,
                TenNhom = x.NhomDich.TenNhomDich,
                Username = x.Username,
                Ten = x.ThongTinNguoiDung.Ten,
                NgaySinh = x.ThongTinNguoiDung.NgaySinh,
                NgayHetHan= TblToken.ThoiGianHetHan,
                Token= token
            }).FirstOrDefault();
            long TongQuyen = (long)context.TaiKhoans.Where(x => x.Id == TblToken.Id_TaiKhoan && !x.DelFlag).FirstOrDefault().PhanQuyen.TongQuyen;
            getAccount.Permissions = context.Quyens.Where(x => !x.DelFlag && ( (long)x.BitQuyen & TongQuyen)!=0).Select(x => new AllPermission
            {
                TenQuyen=x.TenQuyen,
                Id_Quyen=x.Id
            }).ToList();

            return getAccount;
        }

        /// <summary>
        /// Update thông tin cá nhân
        /// Author       :   HoangNM - 29/03/2019 - create
        /// </summary>
        /// <param name="account">
        /// thông tin mà người dùng muốn thay đổi
        /// </param>
        /// <returns>
        /// Thông báo
        /// </returns>

        public ResponseInfo UpdateAccount(UpdateAccount account)
        {

            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                token = BaoMat.Base64Decode(token);
                TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == token);
                context.ThongTinNguoiDungs.Where(x => x.Id == TblToken.TaiKhoan.ThongTinNguoiDung.Id && !x.DelFlag).Update(x => new TblUser
                {
                    Ten = account.Ten,
                    NgaySinh = account.NgaySinh,
                    GioiTinh = account.GioiTinh
                });
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.CapNhatThongTinThanhCong);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;

                if (account.New_Password != "" && account.Old_Password != "")
                {
                    string Hash_Pass_Old = BaoMat.GetMD5(BaoMat.GetSimpleMD5(account.Old_Password), context.TaiKhoans.Where(x => x.Id == TblToken.TaiKhoan.Id && !x.DelFlag).FirstOrDefault().salt_Pass);
                    string HashPassAccount = context.TaiKhoans.Where(x => x.Id == TblToken.TaiKhoan.Id && !x.DelFlag).FirstOrDefault().hash_Pass;
                    if (string.Compare(HashPassAccount, Hash_Pass_Old) == 0)
                    {
                        string Hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(account.New_Password), context.TaiKhoans.Where(x => x.Id == TblToken.TaiKhoan.Id && !x.DelFlag).FirstOrDefault().salt_Pass);

                        //cập nhật mật khẩu
                        context.TaiKhoans.Where(x => x.Id == TblToken.TaiKhoan.Id && !x.DelFlag).Update(y => new TblTaiKhoan
                        {
                            hash_Pass = Hash_Pass

                        });
                    }
                    else
                    {
                        errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.MatKhauCuKhongDung);
                        response.TypeMsgError = errorMsg.Type;
                        response.MsgError = errorMsg.Msg;
                    }

                    
                }


                
                context.SaveChanges();
                response.IsSuccess = true;
                transaction.Commit();

                
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                transaction.Rollback();
                throw e;
            }

            return response;
        }



    }
}