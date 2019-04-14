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

            //loại truyện
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
                "teams",
                new { controller = "QuanLyNhomDich", action = "DanhSachNhomDich", id = UrlParameter.Optional },
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

            //tác giả  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTacGia",
                "authors",
                new { controller = "QuanLyTacGia", action = "DanhSachTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
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
                "authors/add",
                new { controller = "QuanLyTacGia", action = "ThemTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTacGia",
                "authors/{id}",
                new { controller = "QuanLyTacGia", action = "UpdateTacGia", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            //tài khoản  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTaiKhoan",
                "account",
                new { controller = "QuanLyTaiKhoan", action = "DanhSachTacGia", id = UrlParameter.Optional },
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
                "account/status/{id}",
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
                "APIDanhSachTruyen",
                "stories",
                new { controller = "QuanLyTruyen", action = "DanhSachTatCaTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
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
               "stories",
               new { controller = "QuanLyTruyen", action = "UpdateTruyen", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
           );

            context.Routes.MapHttpRoute(
                "APIUpdate",
                "stories/status/{id}",
                new { controller = "QuanLyTruyen", action = "UpdateTrangThaiTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.Routes.MapHttpRoute(
                "APIAddTruyen",
                "stories/add",
                new { controller = "QuanLyTruyen", action = "AddTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateChuKy",
                "stories/frequencies",
                new { controller = "QuanLyTruyen", action = "UpdateChuKyTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.Routes.MapHttpRoute(
                "APIDanhSachTruyenNhom",
                "stories/teams",
                new { controller = "QuanLyTruyen", action = "DanhSachTruyenTrongNhom", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIAddTacGiaChoTruyen",
                "stories/authors",
                new { controller = "QuanLyTruyen", action = "AddTacGiaChoTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "APIThemTheLoaiChoTruyen",
                "stories/categories",
                new { controller = "QuanLyTruyen", action = "AddTheLoaiChoTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            //Chương truyện ---------------------------------------------------------------------------------------

            context.Routes.MapHttpRoute(
                "APIDanhSachChuongTruyen",
                "chapers",
                new { controller = "QuanLyChuongTruyen", action = "DanhSachTatCaTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );


            context.Routes.MapHttpRoute(
                "APIDeleteChuong",
                "chapers/{id}",
                new { controller = "QuanLyChuongTruyen", action = "DeleteChuongTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.Routes.MapHttpRoute(
                "APChuongTruyen",
                "chapers/{id}",
                new { controller = "QuanLyChuongTruyen", action = "Get", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "APIAddChuongTruyen",
                "chapers",
                new { controller = "QuanLyChuongTruyen", action = "ThemChuongTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
              "APIUpdateChuongTruyen",
              "chapers/{id}",
              new { controller = "QuanLyChuongTruyen", action = "UpdateChuongTruyen", id = UrlParameter.Optional },
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