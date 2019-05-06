using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblNhomDich = ReadComic.DataBase.Schema.NhomDich;
using TblTruyen = ReadComic.DataBase.Schema.Truyen;
using TblChuong = ReadComic.DataBase.Schema.Chuong;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;

namespace ReadComic.Areas.Admin.Models.QuanLyNhomDich
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến quản lý nhóm dịch
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyNhomDichModel
    {
        private DataContext context;
        public QuanLyNhomDichModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>Danh sách nhóm dịch. Exception nếu có lỗi</returns>
        public DanhSachNhom GetDanhSachNhomDich(int index)
        {
            try
            {
                DanhSachNhom danhSachNhom = new DanhSachNhom();
                danhSachNhom.Paging= new Paging(context.NhomDiches.Count(x => !x.DelFlag), index);

                danhSachNhom.listNhomDich = context.NhomDiches.Where(x => !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((danhSachNhom.Paging.CurrentPage - 1) * danhSachNhom.Paging.NumberOfRecord)
                    .Take(danhSachNhom.Paging.NumberOfRecord)
                    .Select(x => new NhomDich
                    {
                        Id = x.Id,
                        TenNhomDich = x.TenNhomDich,
                        MoTa= x.MoTa,
                        Logo=x.Logo
                    }).ToList();

                return danhSachNhom;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>lấy ra nhóm dịch theo id. Exception nếu có lỗi</returns>
        public NhomDich LoadNhomDich(int id)
        {
            try
            {
                NhomDich nhomDich = new NhomDich();
                TblNhomDich tblNhomDich = context.NhomDiches.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblNhomDich != null)
                {
                    nhomDich.Id = tblNhomDich.Id;
                    nhomDich.TenNhomDich = tblNhomDich.TenNhomDich;
                    nhomDich.MoTa = tblNhomDich.MoTa;
                    nhomDich.Logo = tblNhomDich.Logo;
                }
                return nhomDich;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các nhóm dịch trong DB.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các nhóm dịch sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn nhóm dịch được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteNhomDich(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.NhomDiches.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblNhomDich nhomDich = context.NhomDiches.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    nhomDich.DelFlag = true;
                    context.Truyens.Where(x => x.Id_Nhom == id && !x.DelFlag).Update(x => new TblTruyen
                    {
                        DelFlag = true
                    });
                    context.Chuongs.Where(x => x.Truyen.Id_Nhom == id && !x.DelFlag).Update(x => new TblChuong
                    {
                        DelFlag = true
                    });
                    context.TaiKhoans.Where(x=>x.Id_NhomDich==id && !x.DelFlag).Update(x => new TblTaiKhoan
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

        /// <summary>
        /// Cập nhật thông tin nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="nhomDich">thông tin về nhóm dịch muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật nhóm dịch, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateNhomDich(NhomDich nhomDich,int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.NhomDiches.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblNhomDich
                    {
                        TenNhomDich = nhomDich.TenNhomDich,
                        Logo=nhomDich.Logo,
                        MoTa=nhomDich.MoTa
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
        /// Thêm nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="nhomDich">nhóm dịch sẽ thêm</param>
        /// <returns>Trả về các thông tin khi cập nhật nhóm dịch, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemNhomDich(NhomDich nhomDich)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();
                int Id = Common.Common.GetAccount().Id;
                var TaiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == Id && !x.DelFlag);
                if(TaiKhoan.Id_PhanQuyen == 5 || TaiKhoan.Id_PhanQuyen == 4)
                {
                    TaiKhoan.Id_PhanQuyen = 3;
                }
                int id = context.NhomDiches.Count() == 0 ? 1 : context.NhomDiches.Max(x => x.Id) + 1;
                var nhom=context.NhomDiches.Add(new TblNhomDich
                {
                    TenNhomDich = nhomDich.TenNhomDich,
                    MoTa=nhomDich.MoTa,
                    Logo=nhomDich.Logo
                });
                TaiKhoan.Id_NhomDich = nhom.Id;
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

        /// <summary>
        /// Tìm kiếm nhóm dịch theo tên
        /// Author       :   HoangNM - 27/04/2019 - create
        /// </summary>
        /// <param name="query">tên nhóm dịch cần tìm kiếm</param>
        /// <returns>Danh sách các nhóm dịch đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachNhom GetListNhomSearch(string query, int index)
        {
            try
            {

                DanhSachNhom listNhomDich = new DanhSachNhom();

                // Lấy các thông tin dùng để phân trang
                listNhomDich.Paging = new Paging(context.NhomDiches.Count(x => x.TenNhomDich.Contains(query) && !x.DelFlag), index);
                // Tìm kiếm và lấy dữ liệu theo trang
                listNhomDich.listNhomDich = context.NhomDiches.Where(x => x.TenNhomDich.Contains(query) && !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listNhomDich.Paging.CurrentPage - 1) * listNhomDich.Paging.NumberOfRecord)
                    .Take(listNhomDich.Paging.NumberOfRecord).Select(x => new Schema.NhomDich
                    {
                        Id = x.Id,
                        TenNhomDich = x.TenNhomDich,
                        MoTa = x.MoTa,
                        Logo = x.Logo
                    }).ToList();

                return listNhomDich;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}