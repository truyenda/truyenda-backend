using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyQuyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblQuyen = ReadComic.DataBase.Schema.Quyen;

namespace ReadComic.Areas.Admin.Models.QuanLyQuyen
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến quyền
    /// Author       :   HoangNM - 16/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyQuyenModel
    {
        private DataContext context;
        public QuanLyQuyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách quyền ra
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>Danh sách quyền. Exception nếu có lỗi</returns>
        public List<Quyen> GetDanhSachQuyen()
        {
            try
            {
                List<Quyen> listQuyen = new List<Quyen>();

                listQuyen = context.Quyens.Where(x => !x.DelFlag)
                    .Select(x => new Quyen
                    {
                        Id = x.Id,
                        TenQuyen = x.TenQuyen,
                        BitQuyen=x.BitQuyen
                    }).OrderBy(x=>x.BitQuyen).ToList();

                return listQuyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>lấy ra quyền theo id. Exception nếu có lỗi</returns>
        public Quyen LoadQuyen(int id)
        {
            try
            {
                Quyen quyen = new Quyen();
                TblQuyen tblQuyen = context.Quyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblQuyen != null)
                {
                    quyen.Id = tblQuyen.Id;
                    quyen.TenQuyen = tblQuyen.TenQuyen;
                    quyen.BitQuyen = tblQuyen.BitQuyen;
                }
                return quyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa quyền trong DB.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="id">id của quyền sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteQuyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.Quyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblQuyen quyen = context.Quyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    quyen.DelFlag = true;
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
        /// Cập nhật thông tin quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="quyen">thông tin về quyền muốn thay đổi</param>
        /// <param name="id">là id của quyền muốn cập nhật</param>
        /// <returns>Trả về các thông tin khi cập nhật quyền, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateQuyen(NewQuyen quyen, int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.Quyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblQuyen
                    {
                        TenQuyen = quyen.TenQuyen,
                        BitQuyen=quyen.BitQuyen
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

        /// <summary>
        /// Thêm quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="quyen">quyền sẽ thêm</param>
        /// <returns>Trả về các thông tin khi thêm quyền vào db, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemQuyen(NewQuyen quyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                int id = context.Quyens.Count() == 0 ? 1 : context.PhanQuyens.Max(x => x.Id) + 1;
                context.Quyens.Add(new TblQuyen
                {
                    TenQuyen = quyen.TenQuyen,
                    BitQuyen = quyen.BitQuyen
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = id + "";
                transaction.Commit();
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThemDuLieuThanhCong);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
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