﻿using ReadComic.Areas.Admin.Models.HomeModel.Schema;
using ReadComic.Areas.Home.Models.HomeModel.Schema;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                        TenKhac=x.TenKhac,
                        NamPhatHanh=x.NamPhatHanh,
                        AnhBia=x.AnhBia,
                        AnhDaiDien= x.AnhDaiDien,
                        MoTa=x.MoTa,
                        NgayTao=x.NgayTao,
                        TrangThai=x.TrangThaiTruyen.TenTrangThai,
                        ChuKyPhatHanh=x.ChuKyPhatHanh.TenChuKy,
                        DanhSachTacGia=x.LuuTacGias.Where(y=> !y.DelFlag).Select(y=>new TacGia
                        {
                            Id=y.TacGia.Id,
                            TenTacGia=y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai=x.LuuLoaiTruyens.Where(y=> !y.DelFlag).Select(y=>new TheLoai
                        {
                            Id=y.LoaiTruyen.Id,
                            TenTheLoai=y.LoaiTruyen.TenTheLoai,
                            MoTa=y.LoaiTruyen.Mota
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
                chuongTruyen=context.Truyens.Where(x => x.Id == IdTruyen && !x.DelFlag).Select(x => new ChuongTruyen {
                    IdTruyen=x.Id,
                    TenTruyen=x.TenTruyen,
                    AnhBia=x.AnhBia,
                    listTheLoai= x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                    {
                        Id = y.LoaiTruyen.Id,
                        TenTheLoai = y.LoaiTruyen.TenTheLoai,
                        MoTa = y.LoaiTruyen.Mota
                    }).ToList(),
                    listChuong=x.Chuongs.Select(y=> new Chuong
                    {
                        IdChuong=y.Id,
                        TenChuong=y.TenChuong,
                        soThuTu=y.SoThuTu,
                        linkAnh=y.LinkAnh,
                        luotXem=y.LuotXem,
                        ngayTao=y.NgayTao

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
                NewComicList = context.LuuLoaiTruyens.Where(x =>x.IdLoaiTruyen==IdTheLoai && !x.DelFlag)
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
        public List<Comic> SearchComic(string search)
        {
            try
            {
                List<Comic> NewComicList = new List<Comic>();
                NewComicList = context.Truyens.Where(x => !x.DelFlag && x.TenTruyen.Contains(search))
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

                    }).OrderBy(x=>Guid.NewGuid()).Take(5).ToList();
                return NewComicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}