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
using TblToken = ReadComic.DataBase.Schema.Token;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Enum;
using ReadComic.Common.Permission;
using ReadComic.Areas.Admin.Models.HomeModel.Schema;
using ReadComic.Areas.Home.Models.HomeModel.Schema;

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
        public DanhSachTruyen GetListTruyen(int index)
        {
            try
            {
                
                DanhSachTruyen listTruyen = new DanhSachTruyen();

                // Lấy các thông tin dùng để phân trang
                listTruyen.Paging = new Paging(context.Truyens.Count(x =>!x.DelFlag), index);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTruyen.listTruyen = context.Truyens.Where(x =>!x.DelFlag).OrderBy(x => x.Id)
                    .Skip((listTruyen.Paging.CurrentPage - 1) * listTruyen.Paging.NumberOfRecord)
                    .Take(listTruyen.Paging.NumberOfRecord).Select(x => new Truyen
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        TenKhac=x.TenKhac,
                        Id_ChuKy = x.Id_ChuKy,
                        Id_TrangThai = x.Id_TrangThai,
                        TrangThai=x.TrangThaiTruyen.TenTrangThai,
                        Id_Nhom=x.Id_Nhom,
                        TenNhom = x.NhomDich.TenNhomDich,
                        AnhDaiDien = x.AnhDaiDien,
                        AnhBia=x.AnhBia,
                        NamPhatHanh=x.NamPhatHanh,
                        MoTa=x.MoTa,
                        DanhSachTacGia= x.LuuTacGias.Where(y => !y.DelFlag).Select(y => new TacGia
                        {
                            Id = y.TacGia.Id,
                            TenTacGia = y.TacGia.TenTacGia
                        }).ToList(),
                        DanhSachTheLoai= x.LuuLoaiTruyens.Where(y => !y.DelFlag).Select(y => new TheLoai
                        {
                            Id = y.LoaiTruyen.Id,
                            TenTheLoai = y.LoaiTruyen.TenTheLoai,
                            MoTa = y.LoaiTruyen.Mota
                        }).ToList()

                    }).ToList();

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
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("STORY_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    if (context.Truyens.FirstOrDefault(x => x.Id == id && !x.DelFlag) != null)
                    {
                        TblTruyen truyen = context.Truyens.FirstOrDefault(x => x.Id == id && !x.DelFlag);
                        truyen.DelFlag = true;
                        context.Chuongs.Where(x => x.Id_Truyen == id && !x.DelFlag).Update(x => new TblChuongTruyen
                        {
                            DelFlag=true
                        });
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    if (context.Truyens.FirstOrDefault(x => x.Id == id && x.Id_Nhom==Common.Common.GetAccount().IdNhom && !x.DelFlag) != null)
                    {
                        TblTruyen truyen = context.Truyens.FirstOrDefault(x => x.Id == id && x.Id_Nhom == Common.Common.GetAccount().IdNhom && !x.DelFlag);
                        truyen.DelFlag = true;
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
        /// Cập nhật thông tin truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="truyen">thông tin về truyện muốn thay đổi</param>
        /// <returns>Trả về các thông tin khi cập nhật truyện, Excetion nếu có lỗi</returns>
        public ResponseInfo UpadateTruyen(NewComic truyen,int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("STORY_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    XuLyUpDateTruyen(truyen, id);
                }
                else
                {
                    int Id_nhom = Common.Common.GetAccount().IdNhom;
                    TblTruyen tblTruyen = context.Truyens.FirstOrDefault(x => x.Id == id && x.Id_Nhom == Id_nhom && !x.DelFlag);
                    if (tblTruyen != null)
                    {
                        XuLyUpDateTruyen(truyen, id);
                    }
                    else
                    {
                        var errorMsg1 = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.CapNhatThongTinThanhCong);
                        response.TypeMsgError = errorMsg1.Type;
                        response.MsgError = errorMsg1.Msg;
                        return response;
                    }
                    
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
                int IdNhom = Common.Common.GetAccount().IdNhom;

                TblTruyen tblTruyen=context.Truyens.Add(new TblTruyen
                {
                    TenTruyen = truyen.TenTruyen,
                    TenKhac = truyen.TenKhac,
                    Id_Nhom = IdNhom,
                    Id_ChuKy = truyen.Id_ChuKy,
                    Id_TrangThai = truyen.Id_TrangThai,
                    NamPhatHanh = truyen.NamPhatHanh,
                    AnhBia = truyen.AnhBia,
                    AnhDaiDien = truyen.AnhDaiDien,
                    MoTa = truyen.MoTa,
                    NgayTao=DateTime.Now
                });
                context.SaveChanges();

                //Thêm thể loại truyên

                foreach (int idTheLoai in truyen.TheLoai)
                {
                    context.LuuLoaiTruyens.Add(new TblLuuLoaiTruyen
                    {
                        IdTruyen = tblTruyen.Id,
                        IdLoaiTruyen = idTheLoai
                    });
                }

                context.SaveChanges();

                //Thêm tác giả

                string[] tacGia = truyen.TacGia.Split(',');
                foreach (string tacgia in tacGia)
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
                        Id_Truyen = tblTruyen.Id,
                        Id_TacGia = Id_TacGia
                    });
                    context.SaveChanges();

                }

                response.ThongTinBoSung1 = tblTruyen.Id + "";
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
        public DanhSachTruyen GetListTruyenNhom(int index)
        {
            try
            {
                // Nếu không tồn tại điều kiện tìm kiếm thì khởi tạo giá trị tìm kiếm ban đầu
                
                DanhSachTruyen listTruyen = new DanhSachTruyen();

                string token = HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d", "=");
                int IdNhom = Common.Common.GetAccount().IdNhom;
                    
                // Lấy các thông tin dùng để phân trang
                listTruyen.Paging = new Paging(context.Truyens.Count(x =>!x.DelFlag), index);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTruyen.listTruyen = context.Truyens.Where(x => x.Id_Nhom==IdNhom && !x.DelFlag).OrderBy(x => x.Id)
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

                return listTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void XuLyUpDateTruyen(NewComic truyen,int id)
        {
            //cập nhật các thông tin cơ bản cho truyện
            context.Truyens.Where(x => x.Id == id && !x.DelFlag)
                    .Update(x => new TblTruyen
                    {
                        Id_ChuKy = truyen.Id_ChuKy,
                        TenTruyen = truyen.TenTruyen,
                        TenKhac = truyen.TenKhac,
                        Id_TrangThai = truyen.Id_TrangThai,
                        NamPhatHanh = truyen.NamPhatHanh,
                        AnhBia = truyen.AnhBia,
                        AnhDaiDien = truyen.AnhDaiDien,
                        MoTa = truyen.MoTa
                    });
            context.SaveChanges();
            //Xóa các thể loại trước đó lưu trong bảng lưu thể loại
            context.LuuLoaiTruyens.Where(x => x.IdTruyen == id).Delete();
            context.SaveChanges();

            //Cập nhật lưu thể loại
            foreach (int idTheLoai in truyen.TheLoai)
            {
                context.LuuLoaiTruyens.Add(new TblLuuLoaiTruyen
                {
                    IdTruyen = id,
                    IdLoaiTruyen = idTheLoai
                });
            }
            context.SaveChanges();
            //xóa các tác giả của truyện trước đó
            context.LuuTacGias.Where(x => x.Id_Truyen == id).Delete();
            context.SaveChanges();
            //Cập nhật tác giả
            string[] tacGia = truyen.TacGia.Split(',');
            foreach (string tacgia in tacGia)
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
                    Id_Truyen = id,
                    Id_TacGia = Id_TacGia
                });
                context.SaveChanges();

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

                //foreach (string idTheLoai in Story.cate)
                //{
                //    context.LuuLoaiTruyens.Add(new TblLuuLoaiTruyen
                //    {
                //        IdTruyen = id_truyen,
                //        IdLoaiTruyen = Convert.ToInt32(idTheLoai)
                //    });
                //}

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

        /// <summary>
        /// Tìm kiếm các truyện phân trang theo yêu cầu tìm kiếm của người dung phân trang và tìm kiếm
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="condition">Đối tượng chứa điều kiện tìm kiếm</param>
        /// <returns>Danh sách các truyện đã tìm kiếm được. Exception nếu có lỗi</returns>
        public DanhSachTruyenTimKiem GetListTruyenSearch(int index,DataSearch data)
        {
            try
            {
                //sắp xếp mảng thể loại
                data.Category.Sort();


                DanhSachTruyenTimKiem listTruyen = new DanhSachTruyenTimKiem();

                // Lấy các thông tin dùng để phân trang
                listTruyen.Paging = new Paging(context.Truyens.Count(x => !x.DelFlag), index);
                // Tìm kiếm và lấy dữ liệu theo trang
                listTruyen.listTruyen = context.Truyens.Where(x =>
                (data.Status == 0 ||(data.Status != 0 && x.Id_TrangThai == data.Status))
                &&(data.Rank==0||
                                (data.Rank !=0 && data.Rank <3
                                && x.Chuongs.Sum(y=>y.LuotXem)>((data.Rank-1)*1000) 
                                && x.Chuongs.Sum(y=>y.LuotXem)<(data.Rank*1000))) ||
                                (data.Rank >2 
                                && x.Chuongs.Sum(y=>y.LuotXem)>((data.Rank - 1) * 1000))
                &&(data.Category==null||
                                        (data.Category!=null 
                                        && string.Join("", x.LuuLoaiTruyens.Select(y=>y.IdLoaiTruyen).ToList()).Contains(string.Join("",data.Category))))

                &&  !x.DelFlag ).OrderBy(x => x.Id)
                    .Skip((listTruyen.Paging.CurrentPage - 1) * listTruyen.Paging.NumberOfRecord)
                    .Take(listTruyen.Paging.NumberOfRecord).Select(x => new SearchTruyen
                    {
                        Id = x.Id,
                        TenTruyen = x.TenTruyen,
                        Id_ChuKy = x.Id_ChuKy,
                        Id_TrangThai = x.Id_TrangThai,
                        TenNhom = x.NhomDich.TenNhomDich,
                        AnhDaiDien = x.AnhDaiDien,
                        AnhBia=x.AnhBia,
                        NgayTao=x.NgayTao,
                        View=1

                    }).ToList();

                return listTruyen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}