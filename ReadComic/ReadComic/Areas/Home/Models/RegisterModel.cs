using ReadComic.Areas.Home.Models.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblThongTinNguoiDung = ReadComic.DataBase.Schema.ThongTinNguoiDung;
using TblPhanQuyens = ReadComic.DataBase.Schema.PhanQuyen;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Enum;

namespace ReadComic.Areas.Home.Models
{
    /// <summary>
    /// Class chứa các phương thức liên quan đến việc đăng ký tài khoản
    /// Author       :   HoangNM - 28/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class RegisterModel
    {
        private DataContext context;

        public RegisterModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Tạo tài khoản cho người dùng dựa vào thông tin đã cung cấp.
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="newAccount">Thông tin tạo tài khoản của người dùng</param>
        /// <returns>Thông tin về việc tạo tài khoản thành công hay thất bại</returns>
        public ResponseInfo TaoAccount(NewAccount newAccount)
        {
            //DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo result = new ResponseInfo();
                // Kiểm tra xem username đã tồn tại hay chưa
                TblTaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Username == newAccount.Username && !x.DelFlag);
                if (taiKhoan == null)
                {
                    // Kiểm tra xem email đã tồn tại hay chưa
                    taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Email == newAccount.Email && !x.DelFlag);
                    if (taiKhoan == null)
                    {
                        // Tạo user mới
                        TblThongTinNguoiDung user = new TblThongTinNguoiDung
                        {
                            Ten = newAccount.Ten,
                            GioiTinh = newAccount.GioiTinh,
                            NgaySinh = newAccount.NgaySinh,
                        };
                        context.ThongTinNguoiDungs.Add(user);
                        context.SaveChanges();



                        string salt = BaoMat.GetSalt();

                        //// Tạo tài khoản đăng nhập cho user
                        taiKhoan = new TblTaiKhoan
                        {
                            Id_User=user.Id,
                            Username = newAccount.Username,
                            salt_Pass = salt,
                            hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(newAccount.Password), salt),
                            Email = newAccount.Email,
                            Id_TrangThai = 1,
                            Id_NhomDich = 1,
                            Id_Face = "",
                            Id_Google = "",
                            Id_PhanQuyen = 5
                        };



                        context.TaiKhoans.Add(taiKhoan);
                        // Lưu vào CSDL
                        context.SaveChanges();


                        

                        result.Code = 200;
                        result.IsSuccess = true;
                        result.IsValid = true;

                        var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.TaoTaiKhoanThanhCong);
                        result.TypeMsgError = errorMsg.Type;
                        result.MsgError = errorMsg.Msg;
                    }
                    else
                    {
                        result.Code = 203;
                        var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.EmailDaTonTai);
                        result.TypeMsgError = errorMsg.Type;
                        result.MsgError = errorMsg.Msg;
                    }
                }
                else
                {
                    result.Code = 203;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.UserNameDaDung);
                    result.TypeMsgError = errorMsg.Type;
                    result.MsgError = errorMsg.Msg;
                }
                //transaction.Commit();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Kiểm tra email hoặc username đã tồn tại hay chưa.
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="value">giá trị của email hoặc username cần kiểm tra</param>
        /// <param name="type">type = 1: kiểm tra usernme; type = 2: kiểm tra email</param>
        /// <returns>Nếu có tồn tại trả về true, ngược lại trả về false</returns>
        public bool CheckExistAccount(string value)
        {
            try
            {
                TblTaiKhoan acount = context.TaiKhoans.FirstOrDefault(x => x.Email == value || x.Username == value && !x.DelFlag);
                if (acount != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}