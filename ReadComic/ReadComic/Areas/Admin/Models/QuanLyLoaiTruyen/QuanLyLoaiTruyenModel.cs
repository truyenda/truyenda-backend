using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyLoaiTruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TbLoaiTruyen = ReadComic.DataBase.Schema.LoaiTruyen;
namespace ReadComic.Areas.Admin.Models.QuanLyLoaiTruyen
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến loại truyện.
    /// Author       :   HoangNM - 10/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyLoaiTruyenModel
    {
        private DataContext context;
        public QuanLyLoaiTruyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách các loại truyện
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <returns>Danh sách các loại truyện. Exception nếu có lỗi</returns>
        public List<LoaiTruyen> GetDanhSachLoaiTruyen()
        {
            try
            {
                List<LoaiTruyen> listLoaiTruyen = new List<LoaiTruyen>();

                listLoaiTruyen = context.LoaiTruyens.Where(x => !x.DelFlag)
                    .Select(x => new LoaiTruyen
                    {
                        Id = x.Id,
                        TenLoaiTruyen = x.TenTheLoai,
                        MoTa = x.Mota
                    }).ToList();

                return listLoaiTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 loại truyện
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <returns>Danh sách các loại truyện. Exception nếu có lỗi</returns>
        public LoaiTruyen LoadLoaiTruyen(int id)
        {
            try
            {
                LoaiTruyen loaiTruyen = new LoaiTruyen();
                TbLoaiTruyen tbLoaiTruyen = context.LoaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tbLoaiTruyen != null)
                {
                    loaiTruyen.Id = tbLoaiTruyen.Id;
                    loaiTruyen.TenLoaiTruyen = tbLoaiTruyen.TenTheLoai;
                    loaiTruyen.MoTa = tbLoaiTruyen.Mota;
                }
                return loaiTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa các loại truyện trong DB.
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các loại truyện sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn Loại truyện được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteLoaiTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.LoaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TbLoaiTruyen loaiTruyen = context.LoaiTruyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    loaiTruyen.DelFlag = true;
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
        /// Cập nhật thông tin loại truyện
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="loaiTruyen">thông tin về loại truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật loại truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateLoaiTruyen(LoaiTruyen loaiTruyen,int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.LoaiTruyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TbLoaiTruyen
                    {
                        TenTheLoai = loaiTruyen.TenLoaiTruyen,
                        Mota = loaiTruyen.MoTa
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
        /// Thêm loại truyện
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id của các loại truyện sẽ xóa</param>
        /// <returns>Trả về các thông tin khi cập nhật loại truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo ThemLoaiTruyen(LoaiTruyen loaiTruyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                ResponseInfo response = new ResponseInfo();

                int id = context.LoaiTruyens.Count() == 0 ? 1 : context.LoaiTruyens.Max(x => x.Id) + 1;
                context.LoaiTruyens.Add(new TbLoaiTruyen
                {
                    TenTheLoai = loaiTruyen.TenLoaiTruyen,
                    Mota = loaiTruyen.MoTa
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