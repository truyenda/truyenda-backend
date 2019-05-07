using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReadComic.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            

            //chu kỳ phát hành
            context.Routes.MapHttpRoute(
                "APIDanhSachChuKyPhatHanh",
                "frequencies",
                new { controller = "QuanLyChuKyTruyen", action = "DanhSachChuKyPhatHanh", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIChuKyPhatHanh",
                "frequencies/{id}",
                new { controller = "QuanLyChuKyTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteChuKyPhatHanh",
                "frequencies/{id}",
                new { controller = "QuanLyChuKyTruyen", action = "DeleteChuKyTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APICreateChuKyPhatHanh",
                "frequencies",
                new { controller = "QuanLyChuKyTruyen", action = "ThemChuKy", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateChuKyPhatHanh",
                "frequencies/{id}",
                new { controller = "QuanLyChuKyTruyen", action = "UpdateChuKy", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //loại truyện-------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachLoaiTruyen",
                "categories",
                new { controller = "QuanLyLoaiTruyen", action = "DanhSachLoaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APILoaiTruyenPhatHanh",
                "categories/{id}",
                new { controller = "QuanLyLoaiTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteLoaiTruyen",
                "categories/{id}",
                new { controller = "QuanLyLoaiTruyen", action = "DeleteLoaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APICreateLoaiTruyen",
                "categories",
                new { controller = "QuanLyLoaiTruyen", action = "ThemLoaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateLoaiTruyen",
                "categories/{id}",
                new { controller = "QuanLyLoaiTruyen", action = "UpdateLoaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //nhóm dịch----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachNhomDich",
                "teams/page/{index}",
                new { controller = "QuanLyNhomDich", action = "DanhSachNhomDich", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APISearchNhomDich",
                "teams/search/{query}/page/{index}",
                new { controller = "QuanLyNhomDich", action = "SearchDanhSachNhomDicn", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APINhomDich",
                "teams/{id}",
                new { controller = "QuanLyNhomDich", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteNhomDich",
                "teams/{id}",
                new { controller = "QuanLyNhomDich", action = "DeleteNhomDich", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APICreateNhomDich",
                "teams",
                new { controller = "QuanLyNhomDich", action = "ThemNhomDich", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateNhomDich",
                "teams/{id}",
                new { controller = "QuanLyNhomDich", action = "UpdateNhomDich", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //thành viên trong nhóm  ----------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
               "APIDanhSachThanhVienCuaTaiKhoan",
               "teams/my/members",
               new { controller = "QuanLyThanhVienTrongNhom", action = "DanhSachNhomDichCuaTaiKhoan", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "APIThanhVienTrongNHom",
               "teams/my/members/{id}",
               new { controller = "QuanLyThanhVienTrongNhom", action = "Get", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "APIThemThanhVien",
               "teams/my/members",
               new { controller = "QuanLyThanhVienTrongNhom", action = "ThemThanhVienVaoNhom", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("POST") }
           );
            context.Routes.MapHttpRoute(
               "APIUpdateThanhVien",
               "teams/my/members/{Id_TaiKhoan}/permissions",
               new { controller = "QuanLyThanhVienTrongNhom", action = "UpadateThanhVienRole", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
           );
            context.Routes.MapHttpRoute(
               "APIDeleteThanhVien",
               "teams/my/members/{Id_TaiKhoan}",
               new { controller = "QuanLyThanhVienTrongNhom", action = "DeleteThanhVien", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
           );

            //tác giả  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTacGia",
                "authors/page/{index}",
                new { controller = "QuanLyTacGia", action = "DanhSachTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APISearchDanhSachTacGia",
                "authors/search/{query}/page/{index}",
                new { controller = "QuanLyTacGia", action = "SearchDanhSachTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APITacGia",
                "authors/{id}",
                new { controller = "QuanLyTacGia", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTacGia",
                "authors/{id}",
                new { controller = "QuanLyTacGia", action = "DeleteTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APICreateTacGia",
                "authors",
                new { controller = "QuanLyTacGia", action = "ThemTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTacGia",
                "authors/{id}",
                new { controller = "QuanLyTacGia", action = "UpdateTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyenVoiTacGia",
                "authors/{Id_TacGia}/stories",
                new { controller = "QuanLyTacGia", action = "DanhSachTruyenVoiTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            //tài khoản  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTaiKhoan",
                "account/page/{index}",
                new { controller = "QuanLyTaiKhoan", action = "DanhSachTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APISearchDanhSachTaiKhoan",
                "account/search/{query}/page/{index}",
                new { controller = "QuanLyTaiKhoan", action = "SearchDanhSachTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APITaiKhoan",
                "account/{id}",
                new { controller = "QuanLyTaiKhoan", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTaiKhoan",
                "account/{id}",
                new { controller = "QuanLyTaiKhoan", action = "DeleteTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTrangThai",
                "account/sstatus/{id}",
                new { controller = "QuanLyTaiKhoan", action = "UpdateTrangThaiTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTaiKhoan",
                "account/{id}",
                new { controller = "QuanLyTaiKhoan", action = "UpdateTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );
            context.Routes.MapHttpRoute(
                "APIUpdateNhom",
                "account/nhom/{id}",
                new { controller = "QuanLyTaiKhoan", action = "UpdateNhomTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //trạng thái truyện  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTrangThaiTruyen",
                "sstatus",
                new { controller = "QuanLyTrangThaiTruyen", action = "DanhSachTrangThaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APITrangThaiTruyen",
                "sstatus/{id}",
                new { controller = "QuanLyTrangThaiTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTrangThaiTruyen",
                "sstatus/{id}",
                new { controller = "QuanLyTrangThaiTruyen", action = "DeleteTrangThaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APICreateTrangThaiTruyen",
                "sstatus",
                new { controller = "QuanLyTrangThaiTruyen", action = "ThemTrangThaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTrangThaiTruyen",
                "sstatus/{id}",
                new { controller = "QuanLyTrangThaiTruyen", action = "UpdateTrangThaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //truyện  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
               "APITruyenWithViewTrending",
               "stories/trending",
               new { controller = "QuanLyTruyen", action = "GetTruyenWithViewTrending", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyen",
                "stories/page/{index}",
                new { controller = "QuanLyTruyen", action = "DanhSachTatCaTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APTruyen",
                "stories/{id}",
                new { controller = "QuanLyTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

           

            context.Routes.MapHttpRoute(
                "APIDeleteTruyen",
                "stories/{id}",
                new { controller = "QuanLyTruyen", action = "DeleteTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
               "APIUpdateTruyen",
               "stories/{id}",
               new { controller = "QuanLyTruyen", action = "UpdateTruyen", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
           );

           

            context.Routes.MapHttpRoute(
                "APIAddTruyen",
                "stories",
                new { controller = "QuanLyTruyen", action = "AddTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyenNhomCuaTaiKhoan",
                "stories/my/page/{index}",
                new { controller = "QuanLyTruyen", action = "DanhSachTruyenTrongNhomCuaTaiKhoan", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyenNhom",
                "stories/team/{idNhom}/page/{index}",
                new { controller = "QuanLyTruyen", action = "DanhSachTruyenTrongNhom", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyenFilter",
                "stories/filter/page/{index}",
                new { controller = "QuanLyTruyen", action = "DanhSachTruyenFilter", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );


            context.Routes.MapHttpRoute(
                "APIThemTruyenTuJson",
                "json/add/story",
                new { controller = "QuanLyTruyen", action = "GetJson", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            //context.Routes.MapHttpRoute(
            //    "APTruyenTrongNhom",
            //    "stories/my",
            //    new { controller = "QuanLyTruyen", action = "DanhSachTatCaTruyenTrongNhom", id = UrlParameter.Optional },
            //    constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            //);

            //Chương truyện ---------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
                "APIDanhSachChuongTruyen",
                "stories/{Id_Truyen}/chapters",
                new { controller = "QuanLyChuongTruyen", action = "DanhSachTatCaTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );


            context.Routes.MapHttpRoute(
                "APIDeleteChuong",
                "chapters/{id}",
                new { controller = "QuanLyChuongTruyen", action = "DeleteChuongTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APChuongTruyen",
                "chapters/{id}",
                new { controller = "QuanLyChuongTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIAddChuongTruyen",
                "chapters",
                new { controller = "QuanLyChuongTruyen", action = "ThemChuongTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
              "APIUpdateChuongTruyen",
              "chapters/{id}",
              new { controller = "QuanLyChuongTruyen", action = "UpdateChuongTruyen", id = UrlParameter.Optional },
              constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
          );


            //ErrorMsg  -----------------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
                "APIDanhSachErrorMgs",
                "errorMsg",
                new { controller = "QuanLyErrorMgs", action = "DanhSachErrorMgs", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );


            context.Routes.MapHttpRoute(
                "APIDeleteErrorMsg",
                "errorMsg/{id}",
                new { controller = "QuanLyErrorMgs", action = "DeleteErrorMsg", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );


            context.Routes.MapHttpRoute(
                "APICreateErrorMgs",
                "errorMsg",
                new { controller = "QuanLyErrorMgs", action = "ThemErrorMgs", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
              "APIUpdateErrorMsg",
              "errorMsg/{id}",
              new { controller = "QuanLyErrorMgs", action = "UpdateErrorMsg", id = UrlParameter.Optional },
              constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
          );


            //Quyền  -----------------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
                "APIDanhSachQuyen",
                "permissions",
                new { controller = "QuanLyQuyen", action = "DanhSachQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachQuyenHeThong",
                "permissions/system",
                new { controller = "QuanLyQuyen", action = "DanhSachQuyenHeThong", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachQuyenTeam",
                "permissions/team",
                new { controller = "QuanLyQuyen", action = "DanhSachQuyenNhom", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APiQuyen",
                "permissions/{id}",
                new { controller = "QuanLyQuyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteQuyen",
                "permissions/{id}",
                new { controller = "QuanLyQuyen", action = "DeleteQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );


            context.Routes.MapHttpRoute(
                "APICreateQuyen",
                "permissions",
                new { controller = "QuanLyQuyen", action = "ThemQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
              "APIUpdateQuyen",
              "permissions/{id}",
              new { controller = "QuanLyQuyen", action = "UpdateQuyen", id = UrlParameter.Optional },
              constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
          );

            //Phân Quyền-----------------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
                "APIDanhSachPhanQuyen",
                "role",
                new { controller = "QuanLyPhanQuyen", action = "DanhSachPhanQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachPhanQuyenTheoTeam",
                "role/team",
                new { controller = "QuanLyPhanQuyen", action = "DanhSachPhanQuyenTheoTeam", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APiPhanQuyen",
                "role/{id}",
                new { controller = "QuanLyPhanQuyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIDeletePhanQuyen",
                "role/{id}",
                new { controller = "QuanLyPhanQuyen", action = "DeletePhanQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );


            context.Routes.MapHttpRoute(
                "APICreatePhanQuyen",
                "role",
                new { controller = "QuanLyPhanQuyen", action = "ThemPhanQuyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
              "APIUpdatePhanQuyen",
              "role/{id}",
              new { controller = "QuanLyPhanQuyen", action = "UpdatePhanQuyen", id = UrlParameter.Optional },
              constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
          );


            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}