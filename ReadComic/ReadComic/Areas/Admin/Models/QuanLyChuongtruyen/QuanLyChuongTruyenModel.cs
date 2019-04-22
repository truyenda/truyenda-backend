using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Permission;
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
        /// Tìm kiếm các truyện phân trang theo phân trang và tìm kiếm
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <returns>Danh sách các truyện đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachChuongTruyen GetListChuongTruyen(ChuongConditionSearch condition)
        {
            try
            {
                // Nếu không tồn tại điều kiện tìm kiếm thì khởi tạo giá trị tìm kiếm ban đầu
                if (condition == null)
                {
                    condition = new ChuongConditionSearch();
                }
                if (condition.CurrentPage < 1)
                    condition.CurrentPage = 1;

                DanhSachChuongTruyen listChuongTruyen = new DanhSachChuongTruyen();

                // Lấy các thông tin dùng để phân trang
                listChuongTruyen.Paging = new Paging(context.Chuongs.Count(x =>
                    (condition.TenChuong == null || (condition.TenChuong != null && (x.TenChuong.Contains(condition.TenChuong))))
                    && (condition.SoThuTu == 0 || (condition.SoThuTu != 0 && x.SoThuTu == condition.SoThuTu)))
                    , condition.CurrentPage);
                // Tìm kiếm và lấy dữ liệu theo trang
                listChuongTruyen.listChuongTruyen = context.Chuongs.Where(x =>
                (condition.TenChuong == null || (condition.TenChuong != null && (x.TenChuong.Contains(condition.TenChuong))))
                    && (condition.SoThuTu == 0 || (condition.SoThuTu != 0 && x.SoThuTu == condition.SoThuTu))
                    && !x.DelFlag).OrderBy(x => x.SoThuTu)
                    .Skip((listChuongTruyen.Paging.CurrentPage - 1) * listChuongTruyen.Paging.NumberOfRecord)
                    .Take(listChuongTruyen.Paging.NumberOfRecord).Select(x => new GetChuongTruyen
                    {
                        Id = x.Id,
                        TenChuong = x.TenChuong,
                        SoThuTu = x.SoThuTu

                    }).ToList();
                listChuongTruyen.Condition = condition;

                return listChuongTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 loại truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <returns>Danh sách các loại truyện. Exception nếu có lỗi</returns>
        public GetChuongTruyen LoadChuongTruyen(int id)
        {
            try
            {
                GetChuongTruyen ChuongTruyen = new GetChuongTruyen();
                TblChuongTruyen tbChuongTruyen = context.Chuongs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tbChuongTruyen != null)
                {
                    ChuongTruyen.Id = tbChuongTruyen.Id;
                    ChuongTruyen.TenChuong = tbChuongTruyen.TenChuong;
                    ChuongTruyen.IdTruyen = tbChuongTruyen.Id_Truyen;
                    ChuongTruyen.TenTruyen = tbChuongTruyen.Truyen.TenTruyen;
                    ChuongTruyen.IdNhomDich = tbChuongTruyen.Truyen.Id_Nhom;
                    ChuongTruyen.TenNhomDich = tbChuongTruyen.Truyen.NhomDich.TenNhomDich;
                    ChuongTruyen.SoThuTu = tbChuongTruyen.SoThuTu;
                    ChuongTruyen.LinkAnh = tbChuongTruyen.LinkAnh;
                    ChuongTruyen.LuotXem = tbChuongTruyen.LuotXem;
                    ChuongTruyen.NgayTao = tbChuongTruyen.NgayTao;
                }
                return ChuongTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa chương truyện trong DB.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="id">id của truyện sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn tài khoản được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteChuongTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("CHAPTER_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    if (context.Chuongs.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                    {
                        TblChuongTruyen chuong = context.Chuongs.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                        chuong.DelFlag = true;
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    if (context.Chuongs.FirstOrDefault(x => x.Id == id && x.Truyen.Id_Nhom==Common.Common.GetAccount().IdNhom && !x.DelFlag) != null)
                    {
                        TblChuongTruyen chuong = context.Chuongs.FirstOrDefault(x => x.Id == id && x.Truyen.Id_Nhom == Common.Common.GetAccount().IdNhom && !x.DelFlag);
                        chuong.DelFlag = true;
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
        /// Cập nhật thông tin chương truyện
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="chuong">thông tin về chương truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật chương truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateChuongTruyen(ChuongCuaTruyen chuong,int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("CHAPTER_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    context.Chuongs.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblChuongTruyen
                    {
                        Id_Truyen = chuong.IdTruyen,
                        TenChuong = chuong.TenChuong,
                        SoThuTu = chuong.SoThuTu,
                        LinkAnh = chuong.LinkAnh
                    });
                }
                else
                {
                    context.Chuongs.Where(x => x.Id == id && x.Truyen.Id_Nhom==Common.Common.GetAccount().IdNhom && !x.DelFlag)
                    .Update(x => new TblChuongTruyen
                    {
                        Id_Truyen = chuong.IdTruyen,
                        TenChuong = chuong.TenChuong,
                        SoThuTu = chuong.SoThuTu,
                        LinkAnh = chuong.LinkAnh
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
                    LuotXem = 0,
                    NgayTao = DateTime.Now
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = chuongTruyen.Id + "";
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