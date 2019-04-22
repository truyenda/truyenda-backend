﻿using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTaiKhoan = ReadComic.DataBase.Schema.TaiKhoan;
using TblToken = ReadComic.DataBase.Schema.Token;
using TblPhanQuyen = ReadComic.DataBase.Schema.PhanQuyen;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Enum;
using ReadComic.Common.Permission;

namespace ReadComic.Areas.Admin.Models.QuanLyTaiKhoan
{
    /// <summary>
    /// Class chứa các phương thức liên quan đến việc xử lý với tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoangc#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTaiKhoanModel
    {
        private DataContext context;

        public QuanLyTaiKhoanModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Tìm kiếm các tài khoản theo phân trang và tìm kiếm
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <param name="type">loại quyền của tài khoản</param>
        /// <returns>Danh sách các tác giả đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTaiKhoan GetListTaiKhoan(TaiKhoanConditionSearch condition)
        {
            try
            {
                // Nếu không tồn tại điều kiện tìm kiếm thì khởi tạo giá trị tìm kiếm ban đầu
                if (condition == null)
                {
                    condition = new TaiKhoanConditionSearch();
                }
                DanhSachTaiKhoan listTaiKhoan = new DanhSachTaiKhoan();
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("ACCOUNT_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());

                if (kt == 0)
                {
                    string Token = BaoMat.Base64Decode(condition.Token);
                    TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == Token);
                    condition.IdNhom = context.TaiKhoans.FirstOrDefault(x => x.Id == TblToken.Id_TaiKhoan).Id_NhomDich;
                }

                // Lấy các thông tin dùng để phân trang
                listTaiKhoan.Paging = new Paging(context.TaiKhoans.Count(x =>
                    (condition.Email == null || (condition.Email != null && (x.Email.Contains(condition.Email) )))
                    && (condition.Username == null || (condition.Username != null &&  x.Username.Contains(condition.Username) ))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.TrangThaiTaiKhoan.Id==condition.IdTrangThai))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.NhomDich.Id==condition.IdNhom)))
                    , condition.CurrentPage);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTaiKhoan.listTaiKhoan = context.TaiKhoans.Where(x =>
                (condition.Email == null || (condition.Email != null && (x.Email.Contains(condition.Email))))
                    && (condition.Username == null || (condition.Username != null && x.Username.Contains(condition.Username)))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.Id_TrangThai == condition.IdTrangThai))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.Id_NhomDich == condition.IdNhom))
                    && !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTaiKhoan.Paging.CurrentPage - 1) * listTaiKhoan.Paging.NumberOfRecord)
                    .Take(listTaiKhoan.Paging.NumberOfRecord).Select(x => new QL_TaiKhoan
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email=x.Email,
                        IdNhom=x.Id_NhomDich,
                        IdTrangThai=x.Id_TrangThai

                    }).ToList();
                listTaiKhoan.Condition = condition;

                return listTaiKhoan;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 tài khoản theo id
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>lấy ra tài khoản theo id. Exception nếu có lỗi</returns>
        public QL_TaiKhoan LoadTaiKhoan(int id)
        {
            try
            {
                QL_TaiKhoan taiKhoan = new QL_TaiKhoan();
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("ACCOUNT_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt!=0)
                {
                    TblTaiKhoan tblTaiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    if (tblTaiKhoan != null)
                    {
                        taiKhoan.Id = tblTaiKhoan.Id;
                        taiKhoan.Username = tblTaiKhoan.Username;
                        taiKhoan.Email = tblTaiKhoan.Email;
                        taiKhoan.IdTrangThai = tblTaiKhoan.Id_TrangThai;
                        taiKhoan.IdNhom = tblTaiKhoan.Id_NhomDich;
                    }
                }
                else 
                {
                    TblTaiKhoan tblTaiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == id &&x.Id_NhomDich== Common.Common.GetAccount().IdNhom && !x.DelFlag);
                    if (tblTaiKhoan != null)
                    {
                        taiKhoan.Id = tblTaiKhoan.Id;
                        taiKhoan.Username = tblTaiKhoan.Username;
                        taiKhoan.Email = tblTaiKhoan.Email;
                        taiKhoan.IdTrangThai = tblTaiKhoan.Id_TrangThai;
                        taiKhoan.IdNhom = tblTaiKhoan.Id_NhomDich;
                    }
                }
                
                return taiKhoan;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các tài khoản trong DB.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các tài khoản sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn tài khoản được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteTaiKhoan(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("ACCOUNT_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                bool result = true;
                if (kt != 0)
                {
                    if (context.TaiKhoans.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                    {
                        TblTaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                        taiKhoan.DelFlag = true;
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    int id_nhomDich = Common.Common.GetAccount().IdNhom;
                    if (context.TaiKhoans.FirstOrDefault(x => x.Id == id &&x.Id_NhomDich==id_nhomDich && !x.DelFlag) != null)
                    {
                        TblTaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(x => x.Id == id && x.Id_NhomDich == id_nhomDich && !x.DelFlag);
                        taiKhoan.DelFlag = true;
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
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
        /// Cập nhật thông tin tài khoản
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="taiKhoan">thông tin về tài khoản muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật tài khoản, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTaiKhoan(QL_TaiKhoan taiKhoan)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("ACCOUNT_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    context.TaiKhoans.Where(x => x.Id == taiKhoan.Id && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Username = taiKhoan.Username,
                        Email = taiKhoan.Email,
                        Id_NhomDich = taiKhoan.IdNhom,
                        Id_TrangThai = taiKhoan.IdTrangThai
                    });
                }
                else
                {
                    int id_nhomDich = Common.Common.GetAccount().IdNhom;
                    context.TaiKhoans.Where(x => x.Id == taiKhoan.Id &&x.Id_NhomDich==id_nhomDich && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Username = taiKhoan.Username,
                        Email = taiKhoan.Email,
                        Id_TrangThai = taiKhoan.IdTrangThai
                    });
                }
                
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
        /// Cập nhật trạng thái tài khoản
        /// Author       :   HoangNM - 19/03/2019 - create
        /// </summary>
        /// <param name="TrangThaiTaiKhoan">thông tin về tài khoản muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật tài khoản, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTrangThaiTaiKhoan(TrangThaiTaiKhoan TrangThaiTaiKhoan)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.TaiKhoans.Where(x => x.Id == TrangThaiTaiKhoan.Id && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Id_TrangThai = TrangThaiTaiKhoan.IdTrangThai
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
        /// Cập nhật nhóm cho tài khoản
        /// Author       :   HoangNM - 19/03/2019 - create
        /// </summary>
        /// <param name="nhom">thông tin về nhóm mà tài khoản muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật tài khoản, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateNhomTaiKhoan(Nhom nhom)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.TaiKhoans.Where(x => x.Id == nhom.Id && !x.DelFlag)
                    .Update(x => new TblTaiKhoan
                    {
                        Id_NhomDich = nhom.IdNhom
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

        

    }
}