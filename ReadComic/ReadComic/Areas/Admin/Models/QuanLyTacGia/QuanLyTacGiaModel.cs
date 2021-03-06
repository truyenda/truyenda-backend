﻿using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema;
using ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTacGia = ReadComic.DataBase.Schema.TacGia;
using TblLuuTacGia = ReadComic.DataBase.Schema.LuuTacGia;

namespace ReadComic.Areas.Admin.Models.QuanLyTacGia
{
    /// <summary>
    /// Class chứa các phương thức liên quan đến việc xử lý với tác giả truyện
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoangc#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTacGiaModel
    {
        private DataContext context;

        public QuanLyTacGiaModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Tìm kiếm các tác giả theo phân trang.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <returns>Danh sách các tác giả đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTacGia GetListTacGia(int page)
        {
            try
            {

                DanhSachTacGia listTacGia = new DanhSachTacGia();

                // Lấy các thông tin dùng để phân trang
                listTacGia.Paging = new Paging(context.TacGias.Count(x => !x.DelFlag), page);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTacGia.listTacGia = context.TacGias.Where(x => !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTacGia.Paging.CurrentPage - 1) * listTacGia.Paging.NumberOfRecord)
                    .Take(listTacGia.Paging.NumberOfRecord).Select(x => new Schema.TacGia
                    {
                        Id = x.Id,
                        TenTacGia = x.TenTacGia
                    }).ToList();

                return listTacGia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <returns>lấy ra tác giả theo id. Exception nếu có lỗi</returns>
        public TacGia LoadTacGia(int id)
        {
            try
            {
                TacGia tacGia = new TacGia();
                TblTacGia tblTacGia = context.TacGias.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblTacGia != null)
                {
                    tacGia.Id = tblTacGia.Id;
                    tacGia.TenTacGia = tblTacGia.TenTacGia;
                }
                return tacGia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các tác giả truyện trong DB.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các tác giả sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteTacGia(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.TacGias.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblTacGia tacGia = context.TacGias.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    tacGia.DelFlag = true;
                    context.LuuTacGias.Where(x => x.Id_TacGia == id && !x.DelFlag).Update(x => new TblLuuTacGia
                    {
                        DelFlag = true
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
        /// Cập nhật thông tin chu kỳ phát hành
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="tacGia">thông tin về tác giả muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật tác giả, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTacGia(TacGia tacGia, int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.TacGias.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTacGia
                    {
                        TenTacGia = tacGia.TenTacGia,
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
        /// Thêm tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="tacGia">tác giả sẽ thêm</param>
        /// <returns>Trả về các thông tin khi thêm tác giả, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemTacGia(TacGia tacGia)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                int id = context.ChuKyPhatHanhs.Count() == 0 ? 1 : context.ChuKyPhatHanhs.Max(x => x.Id) + 1;
                context.TacGias.Add(new TblTacGia
                {
                    TenTacGia = tacGia.TenTacGia
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

        /// <summary>
        /// Tìm kiếm tất cả truyện của tác giả 
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="Id_TacGia">Id của tác giả của truyện muốn tìm</param>
        /// <returns>Danh sách các tác giả đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTruyenTheoTacGia GetListTruyenWithAuthor(int Id_TacGia)
        {
            try
            {

                DanhSachTruyenTheoTacGia listTruyen = new DanhSachTruyenTheoTacGia();
                listTruyen = context.TacGias.Where(x => x.Id == Id_TacGia).Select(x => new DanhSachTruyenTheoTacGia
                {
                    Id_TacGia = x.Id,
                    TenTacGia = x.TenTacGia
                }).FirstOrDefault();


                listTruyen.listTruyen = context.LuuTacGias.Where(x => !x.DelFlag && x.Id_TacGia == Id_TacGia)
                    .Select(x => new Truyen_TacGia
                    {
                        Id_Truyen = x.Id_Truyen,
                        TenTruyen = x.Truyen.TenTruyen,
                        AnhDaiDien = x.Truyen.AnhDaiDien,
                        MoTa = x.Truyen.MoTa
                    }).ToList();

                return listTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Tìm kiếm các tác giả theo tên tác giả
        /// Author       :   HoangNM - 24/04/2019 - create
        /// </summary>
        /// <param name="query">tên tác giả cần tìm kiếm</param>
        /// <returns>Danh sách các tác giả đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTacGia GetListTacGiaSearch(string query,int index)
        {
            try
            {

                DanhSachTacGia listTacGia = new DanhSachTacGia();

                // Lấy các thông tin dùng để phân trang
                listTacGia.Paging = new Paging(context.TacGias.Count(x => x.TenTacGia.Contains(query) && !x.DelFlag), index);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTacGia.listTacGia = context.TacGias.Where(x =>x.TenTacGia.Contains(query) && !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTacGia.Paging.CurrentPage - 1) * listTacGia.Paging.NumberOfRecord)
                    .Take(listTacGia.Paging.NumberOfRecord).Select(x => new Schema.TacGia
                    {
                        Id = x.Id,
                        TenTacGia = x.TenTacGia
                    }).ToList();

                return listTacGia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}