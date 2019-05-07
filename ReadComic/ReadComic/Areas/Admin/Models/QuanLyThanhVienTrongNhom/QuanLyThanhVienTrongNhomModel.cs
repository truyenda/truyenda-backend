using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom.Schema;
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

namespace ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom
{
    public class QuanLyThanhVienTrongNhomModel
    {
        private DataContext context;
        public QuanLyThanhVienTrongNhomModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách thành viên nhóm dịch
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <returns>Danh sách nhóm dịch. Exception nếu có lỗi</returns>
        public DanhSachThanhVien GetDanhSachNhomDichCuaTaiKhoan()
        {
            try
            {
                DanhSachThanhVien danhSachThanhVien = new DanhSachThanhVien();
                danhSachThanhVien.Id_NhomDich = Common.Common.GetAccount().IdNhom;

                danhSachThanhVien.ThanhVienList = context.TaiKhoans.Where(x => !x.DelFlag && x.Id_NhomDich == danhSachThanhVien.Id_NhomDich).OrderBy(x => x.Id)
                    .Select(x => new ThanhVien
                    {
                        Id_TaiKhoanThanhVien = x.Id,
                        Username = x.Username,
                        Id_Role = x.Id_PhanQuyen
                    }).ToList();

                return danhSachThanhVien;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 thanh vien
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <returns>lấy ra nhóm dịch theo id. Exception nếu có lỗi</returns>
        public ThanhVien LoadThanhVien(int id)
        {
            try
            {
                ThanhVien ThanhVien = new ThanhVien();
                TblTaiKhoan tblTaiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblTaiKhoan != null)
                {
                    ThanhVien.Id_TaiKhoanThanhVien = tblTaiKhoan.Id;
                    ThanhVien.Username = tblTaiKhoan.Username;
                    ThanhVien.Id_Role = tblTaiKhoan.Id_PhanQuyen;
                }
                return ThanhVien;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Thêm nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="nhomDich">nhóm dịch sẽ thêm</param>
        /// <returns>Trả về các thông tin khi cập nhật nhóm dịch, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemThanhVienVaoNhom(AddThanhVien data)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();
                int Id = Common.Common.GetAccount().IdNhom;
                var TaiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Username == data.Username && !x.DelFlag);
                if (TaiKhoan == null)
                {
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.UserNameKhongTonTai);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    response.Code = 400;
                    return response;
                }
                else
                {

                    TaiKhoan.Id_NhomDich = Id;
                    context.SaveChanges();
                    transaction.Commit();
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThemThanhvienThanhCong);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    return response;
                }

            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        /// <summary>
        /// Cập nhật quyền cho thành viên
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <param name="data">thông tin về nhóm dịch muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật nhóm dịch, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateThanhVienRole(UpdateThanhVien data, int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {

                context.TaiKhoans.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Id_PhanQuyen = data.Id_Role
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
        /// Xóa thành viên khỏi nhóm dịch
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các nhóm dịch sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn nhóm dịch được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteThanhVien(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.TaiKhoans.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    context.TaiKhoans.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Id_NhomDich = 1
                    });
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
    }
}