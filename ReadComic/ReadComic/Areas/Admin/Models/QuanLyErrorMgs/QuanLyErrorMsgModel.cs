using ReadComic.Areas.Admin.Models.QuanLyErrorMgs.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using System.Web;
using TblErrorMgs = ReadComic.DataBase.Schema.ErrorMsg;

namespace ReadComic.Areas.Admin.Models.QuanLyErrorMgs
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến ErrorMsg
    /// Author       :   HoangNM - 14/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyErrorMsgModel
    {
        private DataContext context;
        public QuanLyErrorMsgModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách ErrorMgs
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <returns>Danh sách chu kỳ truyện. Exception nếu có lỗi</returns>
        public List<ErrorMgs> GetDanhSachErrorMsg()
        {
            try
            {
                List<ErrorMgs> listErrorMgs = new List<ErrorMgs>();

                listErrorMgs = context.ErrorMsgs.Where(x => !x.DelFlag)
                    .Select(x => new ErrorMgs
                    {
                        Id = x.Id,
                        Type=x.Type,
                        Msg = x.mgs
                    }).ToList();

                return listErrorMgs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Xóa ErrorMgs trong DB.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="id">id của các errorMsg sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteErrorMgs(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.ChuKyPhatHanhs.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblErrorMgs errorMgs = context.ErrorMsgs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    errorMgs.DelFlag = true;
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
        /// Cập nhật thông tin ErrorMgs
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="errorMgs">thông tin về ErrorMgs muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật loại truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateErrorMgs(ErrorMgs errorMgs)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.ErrorMsgs.Where(x => x.Id == errorMgs.Id && !x.DelFlag)
                    .Update(x => new TblErrorMgs
                    {
                        Type = errorMgs.Type,
                        mgs= errorMgs.Msg
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
        /// Thêm ErrorMgs
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="errorMgs">errorMgs cần thêm</param>
        /// <returns>Trả về các thông tin khi cập nhật chu kỳ truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemErrorMgs(ErrorMgs errorMgs)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                errorMgs.Id = context.ErrorMsgs.Count() == 0 ? 1 : context.ErrorMsgs.Max(x => x.Id) + 1;
                context.ErrorMsgs.Add(new TblErrorMgs
                {
                    Type = errorMgs.Type,
                    mgs=errorMgs.Msg
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = errorMgs.Id + "";
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