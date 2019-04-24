using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using TblTheoDoiTruyen = ReadComic.Database.Schema.TheoDoiTruyen;
using ReadComic.Areas.Home.Models.TheoDoiTruyen.Schema;
using System.Data.Entity;
using EntityFramework.Extensions;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Enum;

namespace ReadComic.Areas.Home.Models.TheoDoiTruyen
{
    public class TheoDoiTruyenModel
    {
        private DataContext context;

        public TheoDoiTruyenModel()
        {
            context = new DataContext();
        }

        public void TheoDoiTruyen(AddBookMark data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                string Token = BaoMat.Base64Decode(token);
                TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == Token);
                int ID_TaiKhoan = TblToken.Id_TaiKhoan;
                TblTheoDoiTruyen tblTheoDoiTruyen = context.TheoDoiTruyens.FirstOrDefault(x => x.Id_NguoiDoc == ID_TaiKhoan && x.Id_Truyen == data.id_Truyen);
                if (tblTheoDoiTruyen == null)
                {
                    context.TheoDoiTruyens.Add(new TblTheoDoiTruyen
                    {
                        Id_NguoiDoc = ID_TaiKhoan,
                        Id_Truyen = data.id_Truyen
                    });
                    context.SaveChanges();
                }
                
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách theo dõi truyện
        /// Author       :   HoangNM - 22/04/2019 - create
        /// </summary>
        /// <returns>Danh sách theo dõi truyện của tài khoản. Exception nếu có lỗi</returns>
        public TheoDoi GetTheoDoiTruyen()
        {
            try
            {
                TheoDoi listBookMark = new TheoDoi();
                int Id_TaiKhoan = Common.Common.GetAccount().Id;
                listBookMark.Id_TaiKhoan = Id_TaiKhoan;

                listBookMark.ListBookmark = context.TheoDoiTruyens.Where(x =>x.Id_NguoiDoc==Id_TaiKhoan && !x.DelFlag)
                    .Select(x => new BookMark
                    {
                        Id_BookMark=x.Id,
                        Id_Truyen=x.Id_Truyen,
                        TenTruyen = x.Truyen.TenTruyen,
                        Id_NhomDich = x.Truyen.Id_Nhom,
                        TenNhom = x.Truyen.NhomDich.TenNhomDich,
                        Id_ChuongDanhDau = x.Id_ChuongDanhDau,
                        TenChuongDanhDau = x.Truyen.Chuongs.FirstOrDefault(y=>y.Id==x.Id_ChuongDanhDau).TenChuong ,
                        Id_ChuongMoiNhat = x.Truyen.Chuongs.OrderByDescending(y=>y.Id).FirstOrDefault().Id,
                        TenChuongMoiNhat = x.Truyen.Chuongs.OrderByDescending(y=>y.Id).FirstOrDefault().TenChuong
                    }).OrderBy(x=>x.Id_BookMark).ToList();
                
                return listBookMark;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Xóa theo dõi truyên trong db
        /// Author       :   HoangNM - 22/04/2019 - create
        /// </summary>
        /// <param name="id">id của theo dõi truyện sẽ xóa sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteTheoDoiTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.TheoDoiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    context.TheoDoiTruyens.Where(x => x.Id == id && !x.DelFlag).Delete();
                    context.SaveChanges();
                }
                else
                {
                    result = false;
                }
                transaction.Commit();
                return result;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        // Cập nhật thông tin theo dõi truyện
        
        public ResponseInfo UpadateTheoDoi(int id, UpdateTheoDoi data)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.TheoDoiTruyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTheoDoiTruyen
                    {
                        Id_ChuongDanhDau = data.IdChuongTheoDoi,
                    });
                context.SaveChanges();
                response.IsSuccess = true;
                transaction.Commit();
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.CapNhatDuLieuThanhCong);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                transaction.Rollback();
                throw e;
            }
        }

    }
}