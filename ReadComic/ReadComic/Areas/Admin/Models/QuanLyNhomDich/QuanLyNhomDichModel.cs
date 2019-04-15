using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblNhomDich = ReadComic.DataBase.Schema.NhomDich;

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
        public List<NhomDich> GetDanhSachNhomDich()
        {
            try
            {
                List<NhomDich> lishNhomDich = new List<NhomDich>();

                lishNhomDich = context.NhomDiches.Where(x => !x.DelFlag)
                    .Select(x => new NhomDich
                    {
                        Id = x.Id,
                        TenNhomDich = x.TenNhomDich,
                        MoTa= x.MoTa,
                        Logo=x.Logo
                    }).ToList();

                return lishNhomDich;
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

                int id = context.NhomDiches.Count() == 0 ? 1 : context.NhomDiches.Max(x => x.Id) + 1;
                context.NhomDiches.Add(new TblNhomDich
                {
                    TenNhomDich = nhomDich.TenNhomDich,
                    MoTa=nhomDich.MoTa,
                    Logo=nhomDich.Logo
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = id + "";
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