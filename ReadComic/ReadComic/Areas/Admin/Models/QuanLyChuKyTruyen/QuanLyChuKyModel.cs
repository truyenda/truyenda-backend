using EntityFramework.Extensions;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblChuKy = ReadComic.DataBase.Schema.ChuKyPhatHanh;

namespace ReadComic.Areas.Admin.Models.QuanLyChuKyTruyen.Schema
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến chu kỳ phát hành.
    /// Author       :   HoangNM - 12/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyChuKyModel
    {
        private DataContext context;
        public QuanLyChuKyModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách chu kỳ phát hành
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>Danh sách chu kỳ truyện. Exception nếu có lỗi</returns>
        public List<ChuKy> GetDanhSachChuKyTruyen()
        {
            try
            {
                List<ChuKy> listChuKy = new List<ChuKy>();

                listChuKy = context.ChuKyPhatHanhs.Where(x => !x.DelFlag)
                    .Select(x => new ChuKy
                    {
                        Id = x.Id,
                        TenChuKy = x.TenChuKy
                    }).ToList();

                return listChuKy;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 chu kỳ phát hành
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>lấy ra chu kỳ truyện theo id. Exception nếu có lỗi</returns>
        public ChuKy LoadChuKy(int id)
        {
            try
            {
                ChuKy chuKy = new ChuKy();
                TblChuKy tblChuKy = context.ChuKyPhatHanhs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblChuKy != null)
                {
                    chuKy.Id = tblChuKy.Id;
                    chuKy.TenChuKy = tblChuKy.TenChuKy;
                }
                return chuKy;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các chu kỳ truyện trong DB.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các chu kỳ phát hành sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteChuKyTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.ChuKyPhatHanhs.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblChuKy chuKy = context.ChuKyPhatHanhs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    chuKy.DelFlag = true;
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
        /// Cập nhật thông tin chu kỳ phát hành
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="chuKyTruyen">thông tin về chu kỳ truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật loại truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateChuKy(ChuKy chuKyTruyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.ChuKyPhatHanhs.Where(x => x.Id == chuKyTruyen.Id && !x.DelFlag)
                    .Update(x => new TblChuKy
                    {
                        TenChuKy = chuKyTruyen.TenChuKy,
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
        /// Thêm chu kỳ phát hành
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="chuKy">chu kỳ phát hành sẽ thêm</param>
        /// <returns>Trả về các thông tin khi cập nhật chu kỳ truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemChuKy(ChuKy chuKy)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                chuKy.Id = context.ChuKyPhatHanhs.Count() == 0 ? 1 : context.ChuKyPhatHanhs.Max(x => x.Id) + 1;
                context.ChuKyPhatHanhs.Add(new TblChuKy
                {
                    TenChuKy = chuKy.TenChuKy
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = chuKy.Id + "";
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