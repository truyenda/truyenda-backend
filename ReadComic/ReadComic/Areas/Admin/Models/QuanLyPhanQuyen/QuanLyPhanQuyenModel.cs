using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyPhanQuyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblPhanQuyen = ReadComic.DataBase.Schema.PhanQuyen;

namespace ReadComic.Areas.Admin.Models.QuanLyPhanQuyen
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến phân quyền
    /// Author       :   HoangNM - 16/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyPhanQuyenModel
    {
        private DataContext context;
        public QuanLyPhanQuyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách phân quyền ra
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>Danh sách quyền. Exception nếu có lỗi</returns>
        public List<PhanQuyen> GetDanhSachPhanQuyen()
        {
            try
            {
                List<PhanQuyen> listPhanQuyen = new List<PhanQuyen>();

                listPhanQuyen = context.PhanQuyens.Where(x => !x.DelFlag)
                    .Select(x => new PhanQuyen
                    {
                        Id = x.Id,
                        TenVaiTro = x.TenVaiTro,
                        TongQuyen = x.TongQuyen
                    }).OrderBy(x => x.Id).ToList();

                return listPhanQuyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 phân quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>lấy ra quyền theo id. Exception nếu có lỗi</returns>
        public PhanQuyen LoadPhanQuyen(int id)
        {
            try
            {
                PhanQuyen phanQuyen = new PhanQuyen();
                TblPhanQuyen tblPhanQuyen = context.PhanQuyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblPhanQuyen != null)
                {
                    phanQuyen.Id = tblPhanQuyen.Id;
                    phanQuyen.TenVaiTro = tblPhanQuyen.TenVaiTro;
                    phanQuyen.TongQuyen = tblPhanQuyen.TongQuyen;
                }
                return phanQuyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa phân quyền trong DB.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="id">id của phân quyền sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeletePhanQuyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.PhanQuyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblPhanQuyen PhanQuyen = context.PhanQuyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    PhanQuyen.DelFlag = true;
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
        /// Cập nhật thông tin phân quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="phanQuyen">thông tin về phân quyền muốn thay đổi</param>
        /// <param name="id">là id của quyền muốn cập nhật</param>
        /// <returns>Trả về các thông tin khi cập nhật quyền, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadatePhanQuyen(NewPhanQuyen phanQuyen, int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.PhanQuyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblPhanQuyen
                    {
                        TenVaiTro = phanQuyen.TenVaiTro,
                        TongQuyen = phanQuyen.TongQuyen
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
        /// Thêm phân quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="phanQuyen">phân quyền sẽ thêm</param>
        /// <returns>Trả về các thông tin khi thêm quyền vào db, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemPhanQuyen(NewPhanQuyen phanQuyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                int id = context.PhanQuyens.Count() == 0 ? 1 : context.PhanQuyens.Max(x => x.Id) + 1;
                context.PhanQuyens.Add(new TblPhanQuyen
                {
                    TenVaiTro = phanQuyen.TenVaiTro,
                    TongQuyen = phanQuyen.TongQuyen
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