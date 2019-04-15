using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyTrangThaiTruyen.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTrangThaiTruyen = ReadComic.DataBase.Schema.TrangThaiTruyen;

namespace ReadComic.Areas.Admin.Models.QuanLyTrangThaiTruyen
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến quản lý trạng thái truyện.
    /// Author       :   HoangNM - 13/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTrangThaiTruyenModel
    {
        private DataContext context;
        public QuanLyTrangThaiTruyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách trạng thái truyện
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>Danh sách trạng thái truyện. Exception nếu có lỗi</returns>
        public List<TrangThaiTruyen> GetDanhSachTrangThaiTruyen()
        {
            try
            {
                List<TrangThaiTruyen> listTrangThaiTruyen = new List<TrangThaiTruyen>();

                listTrangThaiTruyen = context.ThaiTruyens.Where(x => !x.DelFlag)
                    .Select(x => new TrangThaiTruyen
                    {
                        Id = x.Id,
                        TentrangThai = x.TenTrangThai
                    }).ToList();

                return listTrangThaiTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 trạng thái truyện
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>lấy ra trạng thái truyện theo id. Exception nếu có lỗi</returns>
        public TrangThaiTruyen LoadTrangThaiTruyen(int id)
        {
            try
            {
                TrangThaiTruyen trangThaiTruyen = new TrangThaiTruyen();
                TblTrangThaiTruyen tblTrangThaiTruyen = context.ThaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblTrangThaiTruyen != null)
                {
                    trangThaiTruyen.Id = tblTrangThaiTruyen.Id;
                    trangThaiTruyen.TentrangThai = tblTrangThaiTruyen.TenTrangThai;
                }
                return trangThaiTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các trạng thái truyện trong DB.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các trạng thái truyện sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteTrangThaiTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.ThaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblTrangThaiTruyen tblTrangThaiTruyen = context.ThaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    tblTrangThaiTruyen.DelFlag = true;
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

        /// <summary>
        /// Cập nhật thông tin trạng thái truyện
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="trangThaiTruyen">thông tin về trạng thái truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật trạng thái truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTrangThaiTruyen(TrangThaiTruyen trangThaiTruyen,int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.ThaiTruyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTrangThaiTruyen
                    {
                        TenTrangThai = trangThaiTruyen.TentrangThai,
                    });
                context.SaveChanges();
                response.IsSuccess = true;
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// Thêm trạng thái truyện
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="trangThaiTruyen">trạng thái truyện sẽ thêm</param>
        /// <returns>Trả về các thông tin khi thêm trạng thái truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemTrangThai(TrangThaiTruyen trangThaiTruyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                trangThaiTruyen.Id = context.ThaiTruyens.Count() == 0 ? 1 : context.ThaiTruyens.Max(x => x.Id) + 1;
                context.ThaiTruyens.Add(new TblTrangThaiTruyen
                {
                    TenTrangThai = trangThaiTruyen.TentrangThai
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = trangThaiTruyen.Id + "";
                transaction.Commit();
                return response;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }
    }
}