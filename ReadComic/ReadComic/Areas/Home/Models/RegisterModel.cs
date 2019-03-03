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
                        // Tạo tài khoản đăng nhập cho user
                        taiKhoan = new TblTaiKhoan
                        {
                            Username = newAccount.Username,
                            salt_Pass=salt,
                            hash_Pass = BaoMat.GetMD5(BaoMat.GetSimpleMD5(newAccount.Password), salt),
                            Email = newAccount.Email,
                            Id_TrangThai=2,
                            Id_User=user.Id
                        };
                        // Cho tài khoản thuộc vào 1 group
                        //taiKhoan.PhanQuyens.Add(new TblPhanQuyens
                        //{
                        //    Id_VaiTro = 1
                        //});
                        
                        
                       
                        context.TaiKhoans.Add(taiKhoan);
                        // Lưu vào CSDL
                        context.SaveChanges();

                        //context.PhanQuyens.Add(new TblPhanQuyens
                        //{
                        //    Id_TaiKhoan=taiKhoan.Id,
                        //    Id_VaiTro=1
                        //});
                        //context.SaveChanges();
                        result.IsSuccess = true;
                        result.IsValid = true;
                        result.MsgNo = 39;
                        result.MsgError = new Common.Common().GetErrorMessageById(result.MsgNo.ToString());
                        //result.ThongTinBoSung1 = BaoMat.Base64Encode(taiKhoan.Tokens.);
                    }
                    else
                    {
                        result.Code = 202;
                        result.MsgNo = 37;
                        result.MsgError = new Common.Common().GetErrorMessageById(result.MsgNo.ToString());
                    }
                }
                else
                {
                    result.Code = 202;
                    result.MsgNo = 36;
                    result.MsgError = new Common.Common().GetErrorMessageById(result.MsgNo.ToString());
                }
                //transaction.Commit();
                return result;
            }
            catch (Exception e)
            {
                //transaction.Rollback();
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