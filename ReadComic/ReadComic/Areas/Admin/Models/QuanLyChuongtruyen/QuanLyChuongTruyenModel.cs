using ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblChuongTruyen = ReadComic.DataBase.Schema.Chuong;

namespace ReadComic.Areas.Admin.Models.QuanLyChuongtruyen
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến quản lý chương truyện
    /// Author       :   HoangNM - 04/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyChuongTruyenModel
    {
        private DataContext context;
        public QuanLyChuongTruyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy thông tin 1 loại truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <returns>Danh sách các loại truyện. Exception nếu có lỗi</returns>
        public ChuongCuaTruyen LoadChuongTruyen(int id)
        {
            try
            {
                ChuongCuaTruyen ChuongTruyen = new ChuongCuaTruyen();
                TblChuongTruyen tbChuongTruyen = context.Chuongs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tbChuongTruyen != null)
                {
                    ChuongTruyen.Id = tbChuongTruyen.Id;
                    ChuongTruyen.IdTruyen = tbChuongTruyen.Id_Truyen;
                    ChuongTruyen.TenChuong = tbChuongTruyen.TenChuong;
                    ChuongTruyen.SoThuTu = tbChuongTruyen.SoThuTu;
                    ChuongTruyen.LinkAnh = tbChuongTruyen.LinkAnh;
                    ChuongTruyen.LuotXem = tbChuongTruyen.LuotXem;
                }
                return ChuongTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Thêm chương truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <param name="data">Dữ liệu của truyện sẽ thêm</param>
        /// <returns>Trả về các thông tin khi thêm chương truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemChuongTruyen(ChuongCuaTruyen chuongTruyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                chuongTruyen.Id = context.Chuongs.Count() == 0 ? 1 : context.Chuongs.Max(x => x.Id) + 1;
                context.Chuongs.Add(new TblChuongTruyen
                {
                    Id = chuongTruyen.Id,
                    Id_Truyen = chuongTruyen.IdTruyen,
                    TenChuong = chuongTruyen.TenChuong,
                    SoThuTu = chuongTruyen.SoThuTu,
                    LinkAnh = chuongTruyen.LinkAnh,
                    LuotXem = chuongTruyen.LuotXem,
                    NgayTao=DateTime.Now
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = chuongTruyen.Id + "";
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