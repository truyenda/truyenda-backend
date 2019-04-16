using EntityFramework.Extensions;
using ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema;
using ReadComic.Common;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TblTruyen = ReadComic.DataBase.Schema.Truyen;
using TblLuuTacGia = ReadComic.DataBase.Schema.LuuTacGia;
using TblLuuLoaiTruyen = ReadComic.DataBase.Schema.LuuLoaiTruyen;
using TblChuongTruyen= ReadComic.DataBase.Schema.Chuong;
using TblTacGia = ReadComic.DataBase.Schema.TacGia;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Enum;

namespace ReadComic.Areas.Admin.Models.QuanLyTruyen
{
    /// <summary>
    /// Class chứa các phương thức liên quan đến việc xử lý với truyện
    /// Author       :   HoangNM - 01/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlAdmin.Models
    /// Copyright    :   Team Hoangc#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTruyenModel
    {
        private DataContext context;

        public QuanLyTruyenModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Tìm kiếm các truyện phân trang theo phân trang và tìm kiếm
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <returns>Danh sách các truyện đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTruyen GetListTruyen(TruyenConditionSearch condition)
        {
            try
            {
                // Nếu không tồn tại điều kiện tìm kiếm thì khởi tạo giá trị tìm kiếm ban đầu
                if (condition == null)
                {
                    condition = new TruyenConditionSearch();
                }
                DanhSachTruyen listTruyen = new DanhSachTruyen();

                // Lấy các thông tin dùng để phân trang
                listTruyen.Paging = new Paging(context.Truyens.Count(x =>
                    (condition.TenTruyen == null || (condition.TenTruyen != null && (x.TenTruyen.Contains(condition.TenTruyen))))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.Id_TrangThai == condition.IdTrangThai))
                    && (condition.IdChuKy == 0 || (condition.IdChuKy != 0 && x.Id_ChuKy == condition.IdChuKy))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.Id_Nhom == condition.IdNhom)))
                    , condition.CurrentPage);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTruyen.listTruyen = context.Truyens.Where(x =>
                (condition.TenTruyen == null || (condition.TenTruyen != null && (x.TenTruyen.Contains(condition.TenTruyen))))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.Id_TrangThai == condition.IdTrangThai))
                    && (condition.IdChuKy == 0 || (condition.IdChuKy != 0 && x.Id_ChuKy == condition.IdChuKy))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.Id_Nhom == condition.IdNhom))
                    && !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTruyen.Paging.CurrentPage - 1) * listTruyen.Paging.NumberOfRecord)
                    .Take(listTruyen.Paging.NumberOfRecord).Select(x => new Truyen
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        Id_ChuKy = x.Id_ChuKy,
                        Id_TrangThai = x.Id_TrangThai,
                        TenNhom = x.NhomDich.TenNhomDich,
                        AnhDaiDien = x.AnhDaiDien

                    }).ToList();
                listTruyen.Condition = condition;

                return listTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin 1 truyện theo id
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <returns>lấy ra truyện theo id. Exception nếu có lỗi</returns>
        public Truyen LoadTruyen(int id)
        {
            try
            {
                Truyen truyen = new Truyen();

                TblTruyen tblTruyen = context.Truyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                if (tblTruyen != null)
                {
                    truyen.Id = tblTruyen.Id;
                    truyen.TenTruyen = tblTruyen.TenTruyen;
                    truyen.AnhDaiDien = tblTruyen.AnhDaiDien;
                    truyen.Id_TrangThai = tblTruyen.Id_TrangThai;
                    truyen.TenNhom = tblTruyen.NhomDich.TenNhomDich;
                    truyen.Id_ChuKy = tblTruyen.Id_ChuKy;
                }


                return truyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Xóa truyện trong DB.
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="id">id của truyện sẽ xóa</param>
        /// <returns>True nếu xóa thành công, False nếu không còn tài khoản được hiển thị trên trang chủ, Excetion nếu có lỗi</returns>
        public bool DeleteTruyen(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                bool result = true;
                if (context.Truyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                {
                    TblTruyen truyen = context.Truyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                    truyen.DelFlag = true;
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
        /// Cập nhật thông tin truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="truyen">thông tin về truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTruyen(NewComic truyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.Truyens.Where(x => x.Id == truyen.Id && !x.DelFlag)
                    .Update(x => new TblTruyen
                    {
                       // Id_ChuKy = truyen.Id_ChuKy,
                       // TenTruyen = truyen.TenTruyen,
                        //TenKhac = truyen.TenKhac,
                       // Id_TrangThai = truyen.Id_TrangThai,
                       // NamPhatHanh = truyen.NamPhatHanh,
                        // AnhBia = new Common.Common().SaveImage(truyen.AnhBia,truyen.Id,truyen.AnhBiaName),
                        AnhBia = truyen.AnhBia,
                        //AnhDaiDien = new Common.Common().SaveImage(truyen.AnhDaiDien, truyen.Id,truyen.AnhDaiDienName),
                        AnhDaiDien = truyen.AnhDaiDien,
                        //MoTa = truyen.MoTa
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
        /// Cập nhật trạng thái truyen
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="trangThaiTruyen">trạng thái truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật trạng thái truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTrangThaiTruyen(T_TrangThaiTruyen trangThaiTruyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.Truyens.Where(x => x.Id == trangThaiTruyen.IdTruyen && !x.DelFlag)
                    .Update(x => new TblTruyen
                    {
                        Id_TrangThai = trangThaiTruyen.IdTrangThai
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
        /// Cập nhật chu kỳ cho truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="chuky">thông tin về chu kỳ mà truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật chu kỳ, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateChuKyTruyen(ChuKyTruyen chuky)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                context.Truyens.Where(x => x.Id == chuky.IdTruyen && !x.DelFlag)
                    .Update(x => new TblTruyen
                    {
                        Id_ChuKy = chuky.IdChuKy
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
        /// Cập nhật thông tin truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="truyen">thông tin về truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo AddTruyen(NewComic truyen)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                int IdNhom = Common.Common.GetAccount(token).IdNhom;
                truyen.Id = context.Truyens.Count() == 0 ? 1 : context.Truyens.Max(x => x.Id) + 1;
                context.Truyens.Add(new TblTruyen
                {
                    Id_Nhom = IdNhom,
                    Id_ChuKy = truyen.Id_ChuKy,
                    TenTruyen = truyen.TenTruyen,
                    TenKhac = truyen.TenKhac,
                    Id_TrangThai = truyen.Id_TrangThai,
                    NamPhatHanh = truyen.NamPhatHanh,
                    AnhBia = truyen.AnhBia,
                    AnhDaiDien = truyen.AnhDaiDien,
                    MoTa = truyen.MoTa,
                    NgayTao=DateTime.Now
                });
                context.SaveChanges();
                response.ThongTinBoSung1 = truyen.Id + "";
                response.IsSuccess = true;
                transaction.Commit();
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThemDuLieuThanhCong);
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
        /// Tìm kiếm các truyện phân trang theo phân trang và tìm kiếm
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <returns>Danh sách các truyện đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTruyen GetListTruyenNhom(TruyenConditionSearch condition)
        {
            try
            {
                // Nếu không tồn tại điều kiện tìm kiếm thì khởi tạo giá trị tìm kiếm ban đầu
                if (condition == null)
                {
                    condition = new TruyenConditionSearch();
                }
                DanhSachTruyen listTruyen = new DanhSachTruyen();

                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                int IdNhom = Common.Common.GetAccount(token).IdNhom;
                    
                // Lấy các thông tin dùng để phân trang
                listTruyen.Paging = new Paging(context.Truyens.Count(x =>
                    (condition.TenTruyen == null || (condition.TenTruyen != null && (x.TenTruyen.Contains(condition.TenTruyen))))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.Id_TrangThai == condition.IdTrangThai))
                    && (condition.IdChuKy == 0 || (condition.IdChuKy != 0 && x.Id_ChuKy == condition.IdChuKy))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.Id_Nhom == condition.IdNhom))&& x.Id_Nhom == IdNhom)
                    , condition.CurrentPage);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTruyen.listTruyen = context.Truyens.Where(x =>
                (condition.TenTruyen == null || (condition.TenTruyen != null && (x.TenTruyen.Contains(condition.TenTruyen))))
                    && (condition.IdTrangThai == 0 || (condition.IdTrangThai != 0 && x.Id_TrangThai == condition.IdTrangThai))
                    && (condition.IdChuKy == 0 || (condition.IdChuKy != 0 && x.Id_ChuKy == condition.IdChuKy))
                    && (condition.IdNhom == 0 || (condition.IdNhom != 0 && x.Id_Nhom == condition.IdNhom))
                    && x.Id_Nhom==IdNhom && !x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTruyen.Paging.CurrentPage - 1) * listTruyen.Paging.NumberOfRecord)
                    .Take(listTruyen.Paging.NumberOfRecord).Select(x => new Truyen
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        Id_ChuKy = x.Id_ChuKy,
                        Id_TrangThai = x.Id_TrangThai,
                        TenNhom = x.NhomDich.TenNhomDich,
                        AnhDaiDien = x.AnhDaiDien

                    }).ToList();
                listTruyen.Condition = condition;

                return listTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Thêm tác giả cho truyện
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="data">dữ liệu chứa thông tin tác giả</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo AddTacGiaChoTruyen(ThemTacGiaChoTruyen data)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                foreach(int idTacGia in data.listIdTacGia)
                {
                    context.LuuTacGias.Add(new TblLuuTacGia
                    {
                        Id_Truyen = data.IdTruyen,
                        Id_TacGia=idTacGia
                    });
                }
                
                context.SaveChanges();
                response.ThongTinBoSung1 = data.IdTruyen + "";
                response.IsSuccess = true;
                transaction.Commit();
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThemDuLieuThanhCong);
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
        /// Thêm thể loại cho truyện
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="data">dữ liệu chứa thể loại truyện</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo AddTheLoaiChoTruyen(TheLoaiChoTruyen data)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                foreach (int idTheLoai in data.listTheLoai)
                {
                    context.LuuLoaiTruyens.Add(new TblLuuLoaiTruyen
                    {
                        IdTruyen = data.IdTruyen,
                        IdLoaiTruyen = idTheLoai
                    });
                }

                context.SaveChanges();
                response.ThongTinBoSung1 = data.IdTruyen + "";
                response.IsSuccess = true;
                transaction.Commit();
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ThemDuLieuThanhCong);
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
        /// Dùng để xử lý file json
        /// Author       :   HoangNM - 15/04/2019 - create
        /// </summary>
        /// <param name="path">file json cần xử lý</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo XulyJson(string path)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                //int[] t = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                //foreach(int i in t)
                //{
                //    TblTruyen truyen1 = context.Truyens.FirstOrDefault(x => x.Id == i && !x.DelFlag);
                //    truyen1.DelFlag = true;
                //    context.SaveChanges();
                //}


                //xử lý file json
                string[] lines = File.ReadAllLines(path);
                string json = string.Join("", lines);
                JsonTruyen Story = new JavaScriptSerializer().Deserialize<JsonTruyen>(json);
                List<Chapter> chapters = Story.chapters;
                //thêm truyên-------------------------------------------------------------------------------
                TblTruyen truyen = context.Truyens.Add(new TblTruyen
                {
                    Id_Nhom = 1,
                    Id_ChuKy = new Random().Next(1, 2),
                    TenTruyen = Story.name,
                    TenKhac = string.Join(",", Story.oname),
                    Id_TrangThai = new Random().Next(2, 4),
                    NamPhatHanh = new Random().Next(2004, 2018),
                    AnhBia = Story.thumb,
                    AnhDaiDien = Story.thumb,
                    MoTa = Story.description,
                    NgayTao = DateTime.Now
                });
                context.SaveChanges();
                int id_truyen = truyen.Id;
                response.ThongTinBoSung1 = "Thêm truyện thành công";

                //thêm chương truyên ----------------------------------------------------------------------

                int stt = 0;
                chapters.Reverse();
                foreach (Chapter chapter in chapters)
                {
                    string title = chapter.title;
                    string data = "{" + string.Join(",", chapter.data) + "}";
                    context.Chuongs.Add(new TblChuongTruyen
                    {
                        Id_Truyen = id_truyen,
                        TenChuong = chapter.title,
                        SoThuTu = ++stt,
                        LinkAnh = string.Join(",", chapter.data),
                        LuotXem = 0,
                        NgayTao = DateTime.Now
                    });
                    context.SaveChanges();
                }
                response.ThongTinBoSung2 = "Thêm chương thành công";

                //thêm thể loại cho truyện ----------------------------------------------------------------

                foreach (string idTheLoai in Story.cate)
                {
                    context.LuuLoaiTruyens.Add(new TblLuuLoaiTruyen
                    {
                        IdTruyen = id_truyen,
                        IdLoaiTruyen = Convert.ToInt32(idTheLoai)
                    });
                }

                context.SaveChanges();
                response.ThongTinBoSung3 = "Thêm thể loại thành công";

                //thêm tác giả cho truyên ----------------------------------------------------------------

                foreach (string tacgia in Story.authors)
                {
                    TblTacGia tblTacGia = context.TacGias.FirstOrDefault(x => string.Compare(x.TenTacGia, tacgia) == 0 && !x.DelFlag);
                    int Id_TacGia = 0;
                    if (tblTacGia != null)
                    {
                        Id_TacGia = tblTacGia.Id;
                    }
                    else
                    {
                        tblTacGia = context.TacGias.Add(new TblTacGia
                        {
                            TenTacGia = tacgia
                        });
                        context.SaveChanges();
                        Id_TacGia = tblTacGia.Id;
                    }
                    context.LuuTacGias.Add(new TblLuuTacGia
                    {
                        Id_Truyen = id_truyen,
                        Id_TacGia = Id_TacGia
                    });
                    context.SaveChanges();

                }
                response.ThongTinBoSung5 = "Thêm tác giả thành công";
                response.TypeMsgError = id_truyen;
                transaction.Commit();
                response.MsgError = "Thêm thành công";
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