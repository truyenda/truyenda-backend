using ReadComic.Areas.Admin.Models.HomeModel.Schema;
using ReadComic.Areas.Home.Models.HomeModel.Schema;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TblChuong = ReadComic.DataBase.Schema.Chuong;
using TblluotXemNgay = ReadComic.Database.Schema.LuotXemNgay;
using TblLuotXemTuan = ReadComic.Database.Schema.LuotXemTuan;
using TblLuotXemThang = ReadComic.Database.Schema.LuotXemThang;
using EntityFramework.Extensions;
using System.Globalization;
using ReadComic.Common;

namespace ReadComic.Areas.Home.Models.HomeModel
{
    /// <summary>
    /// Class dùng để xử lý các hoạt động liên quan đến trang Home
    /// Author       :   HoangNM - 26/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   HomeModel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class HomeModel
    {
        private DataContext context;
        public HomeModel()
        {
            context = new DataContext();
        }

        /// <summary>
        /// Lấy danh sách truyện
        /// Author       :   HoangNM - 26/03/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public List<Comic> GetDanhSachTruyenMoi()
        {
            try
            {
                List<Comic> NewComicList = new List<Comic>();
                NewComicList = context.Truyens.Where(x => !x.DelFlag)
                    .Select(x => new Comic
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        TenKhac = x.TenKhac,
                        NamPhatHanh = x.NamPhatHanh,
                        AnhBia = x.AnhBia,
                        AnhDaiDien = x.AnhDaiDien,
                        MoTa = x.MoTa,
                        NgayTao = x.NgayTao,
                        TrangThai = x.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList()

                    }).OrderByDescending(x => x.NgayTao).Take(5).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy thông tin truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public ChuongTruyen GetThongTinTruyen(int IdTruyen)
        {
            try
            {
                ChuongTruyen chuongTruyen = new ChuongTruyen();
                chuongTruyen = context.Truyens.Where(x => x.Id == IdTruyen && !x.DelFlag).Select(x => new ChuongTruyen
                {
                    IdTruyen = x.Id,
                    TenTruyen = x.TenTruyen,
                    AnhBia = x.AnhBia,
                    listTheLoai = x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                    {
                        Id = y.LoaiTruyen.Id,
                        TenTheLoai = y.LoaiTruyen.TenTheLoai,
                        MoTa = y.LoaiTruyen.Mota
                    }).ToList(),
                    listChuong = x.Chuongs.Select(y => new Chuong
                    {
                        IdChuong = y.Id,
                        TenChuong = y.TenChuong,
                        soThuTu = y.SoThuTu,
                        linkAnh = y.LinkAnh,
                        luotXem = y.LuotXem,
                        ngayTao = y.NgayTao

                    }).ToList()

                }).FirstOrDefault();


                return chuongTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả các truyện
        /// Author       :   HoangNM - 05/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện. Exception nếu có lỗi</returns>
        public List<Comic> GetDanhSachTruyen()
        {
            try
            {
                List<Comic> NewComicList = new List<Comic>();
                NewComicList = context.Truyens.Where(x => !x.DelFlag)
                    .Select(x => new Comic
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        TenKhac = x.TenKhac,
                        NamPhatHanh = x.NamPhatHanh,
                        AnhBia = x.AnhBia,
                        AnhDaiDien = x.AnhDaiDien,
                        MoTa = x.MoTa,
                        NgayTao = x.NgayTao,
                        TrangThai = x.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList()

                    }).OrderByDescending(x => x.NgayTao).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả các truyện theo thể loại
        /// Author       :   HoangNM - 05/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện. Exception nếu có lỗi</returns>
        public List<Comic> GetDanhSachTruyenTheoTheLoai(int IdTheLoai)
        {
            try
            {
                List<Comic> NewComicList = new List<Comic>();
                NewComicList = context.LuuLoaiTruyens.Where(x => x.IdLoaiTruyen == IdTheLoai && !x.DelFlag)
                    .Select(x => new Comic
                    {
                        Id = x.Truyen.Id,
                        TenTruyen = x.Truyen.TenTruyen,
                        TenKhac = x.Truyen.TenKhac,
                        NamPhatHanh = x.Truyen.NamPhatHanh,
                        AnhBia = x.Truyen.AnhBia,
                        AnhDaiDien = x.Truyen.AnhDaiDien,
                        MoTa = x.Truyen.MoTa,
                        NgayTao = x.Truyen.NgayTao,
                        TrangThai = x.Truyen.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.Truyen.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.Truyen.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.Truyen.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList()

                    }).OrderByDescending(x => x.NgayTao).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy các truyện phù hợp với mục tìm kiếm
        /// Author       :   HoangNM - 13/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public DanhSachTruyenSearch SearchComic(string query, int index)
        {
            try
            {
                DanhSachTruyenSearch listComicSearch = new DanhSachTruyenSearch();
                // Lấy các thông tin dùng để phân trang
                listComicSearch.Paging = new Paging(context.Truyens.Count(x => x.TenTruyen.Contains(query) && !x.DelFlag), index);


                listComicSearch.listComic = context.Truyens.Where(x => !x.DelFlag && x.TenTruyen.Contains(query))
                    .Select(x => new Comic
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        TenKhac = x.TenKhac,
                        NamPhatHanh = x.NamPhatHanh,
                        AnhBia = x.AnhBia,
                        AnhDaiDien = x.AnhDaiDien,
                        MoTa = x.MoTa,
                        NgayTao = x.NgayTao,
                        TrangThai = x.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList()

                    }).OrderByDescending(x => x.NgayTao).Take(5).ToList();
                return listComicSearch;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách truyện ngẫu nhiên
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public List<Comic> GetDanhSachTruyenRandom()
        {
            try
            {
                List<Comic> NewComicList = new List<Comic>();

                NewComicList = context.Truyens.Where(x => !x.DelFlag)
                    .Select(x => new Comic
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        TenKhac = x.TenKhac,
                        NamPhatHanh = x.NamPhatHanh,
                        AnhBia = x.AnhBia,
                        AnhDaiDien = x.AnhDaiDien,
                        MoTa = x.MoTa,
                        NgayTao = x.NgayTao,
                        TrangThai = x.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.ChuKyPhatHanh.TenChuKy

                    }).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách truyện theo ngày
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public List<getComic> GetComicOfDay()
        {
            try
            {
                string time = DateTime.Now.ToString("dd-MM-yyyy");
                List<getComic> NewComicList = new List<getComic>();
                NewComicList = context.LuotXemNgays.Where(x => x.Id.Contains(time))
                    .Select(x => new getComic
                    {
                        Id = x.Id_Truyen,
                        TenTruyen = x.Truyen.TenTruyen,
                        TenKhac = x.Truyen.TenKhac,
                        NamPhatHanh = x.Truyen.NamPhatHanh,
                        AnhBia = x.Truyen.AnhBia,
                        AnhDaiDien = x.Truyen.AnhDaiDien,
                        MoTa = x.Truyen.MoTa,
                        NgayTao = x.Truyen.NgayTao,
                        TrangThai = x.Truyen.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.Truyen.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.Truyen.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.Truyen.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList(),
                        view = x.View

                    }).OrderByDescending(x => x.view).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Lấy danh sách truyện theo tuần
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public List<getComic> GetComicOfWeek()
        {
            try
            {
                int TuanThu = new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                string time = TuanThu.ToString() + "/" + DateTime.Now.Year;
                List<getComic> NewComicList = new List<getComic>();
                NewComicList = context.LuotXemTuans.Where(x => x.Id.Contains(time))
                    .Select(x => new getComic
                    {
                        Id = x.Id_Truyen,
                        TenTruyen = x.Truyen.TenTruyen,
                        TenKhac = x.Truyen.TenKhac,
                        NamPhatHanh = x.Truyen.NamPhatHanh,
                        AnhBia = x.Truyen.AnhBia,
                        AnhDaiDien = x.Truyen.AnhDaiDien,
                        MoTa = x.Truyen.MoTa,
                        NgayTao = x.Truyen.NgayTao,
                        TrangThai = x.Truyen.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.Truyen.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.Truyen.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.Truyen.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList(),
                        view = x.View

                    }).OrderByDescending(x => x.view).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lấy danh sách truyện theo tháng
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public List<getComic> GetComicOfMonth()
        {
            try
            {
                string time = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                List<getComic> NewComicList = new List<getComic>();
                NewComicList = context.LuotXemThangs.Where(x => x.Id.Contains(time))
                    .Select(x => new getComic
                    {
                        Id = x.Id_Truyen,
                        TenTruyen = x.Truyen.TenTruyen,
                        TenKhac = x.Truyen.TenKhac,
                        NamPhatHanh = x.Truyen.NamPhatHanh,
                        AnhBia = x.Truyen.AnhBia,
                        AnhDaiDien = x.Truyen.AnhDaiDien,
                        MoTa = x.Truyen.MoTa,
                        NgayTao = x.Truyen.NgayTao,
                        TrangThai = x.Truyen.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh = x.Truyen.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia = x.Truyen.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai = x.Truyen.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList(),
                        view = x.View

                    }).OrderByDescending(x => x.view).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Đọc truyện
        /// Author       :   HoangNM - 019/04/2019 - create
        /// </summary>
        /// <returns>Danh sách truyện Mới. Exception nếu có lỗi</returns>
        public GetOneChapter ReadComic(int IdChuong)
        {
            try
            {
                //lấy thông tin của truyện ra
                GetOneChapter chuongTruyen = new GetOneChapter();
                chuongTruyen = context.Chuongs.Where(x => x.Id == IdChuong && !x.DelFlag).Select(x => new GetOneChapter
                {
                    Id_Truyen = x.Truyen.Id,
                    TenTruyen = x.Truyen.TenTruyen,
                    Id_Chuong = x.Id,
                    TenChuong = x.TenChuong,
                    SoThuTu = x.SoThuTu,
                    Id_NhomDich = x.Truyen.Id_Nhom,
                    TenNhomDich = x.Truyen.NhomDich.TenNhomDich,
                    LinkAnh = x.LinkAnh,
                    NgayTao = x.NgayTao,
                    LuotXem = x.LuotXem + 1
                }).FirstOrDefault();

                //cập nhật lượt xem của truyện

                context.Chuongs.Where(x => x.Id == IdChuong && !x.DelFlag)
                    .Update(x => new TblChuong
                    {
                        LuotXem = chuongTruyen.LuotXem,
                    });
                context.SaveChanges();


                //thêm hoặc cập nhật truyện vào lươt xem ngày
                string Id_LuotXemNgay = DateTime.Now.ToString("dd-MM-yyyy") +"_"+ + chuongTruyen.Id_Truyen;
                TblluotXemNgay luotXemNgay = context.LuotXemNgays.Where(x => x.Id == Id_LuotXemNgay).FirstOrDefault();
                if (luotXemNgay == null)
                {
                    context.LuotXemNgays.Add(new TblluotXemNgay
                    {
                        Id = Id_LuotXemNgay,
                        Id_Truyen = chuongTruyen.Id_Truyen,
                        View = 1
                    });
                    context.SaveChanges();

                }
                else
                {
                    luotXemNgay.View++;
                    context.SaveChanges();
                }


                //thêm hoặc cập nhật truyện vào lượt xem tuần

                int TuanThu = new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                string Id_LuotXemTuan = TuanThu.ToString() + "/" + DateTime.Now.Year + "_" + + chuongTruyen.Id_Truyen;
                TblLuotXemTuan luotXemTuan = context.LuotXemTuans.Where(x => x.Id == Id_LuotXemTuan).FirstOrDefault();
                if (luotXemTuan == null)
                {
                    context.LuotXemTuans.Add(new TblLuotXemTuan
                    {
                        Id = Id_LuotXemTuan,
                        Id_Truyen = chuongTruyen.Id_Truyen,
                        View = 1
                    });
                    context.SaveChanges();
                }
                else
                {
                    luotXemTuan.View++;
                    context.SaveChanges();
                }

                //thêm hoặc cập nhật truyện vào lượt xem tháng

                string Id_LuotXemThang = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + "_" + + chuongTruyen.Id_Truyen;
                TblLuotXemThang luotXemThang = context.LuotXemThangs.Where(x => x.Id == Id_LuotXemThang).FirstOrDefault();
                if (luotXemThang == null)
                {
                    context.LuotXemThangs.Add(new TblLuotXemThang
                    {
                        Id = Id_LuotXemThang,
                        Id_Truyen = chuongTruyen.Id_Truyen,
                        View = 1
                    });
                    context.SaveChanges();
                }
                else
                {
                    luotXemThang.View++;
                    context.SaveChanges();
                }

                return chuongTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}