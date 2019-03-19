using System.Web.Http;
using System.Web.Mvc;

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
                "api/admin/danh-sach-chu-ky",
                new { controller = "QuanLyChuKyTruyen", action = "DanhSachChuKyPhatHanh", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIChuKyPhatHanh",
                "api/admin/chu-ky/{id}",
                new { controller = "QuanLyChuKyTruyen", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteChuKyPhatHanh",
                "api/admin/delete/chu-ky/{id}",
                new { controller = "QuanLyChuKyTruyen", action = "DeleteChuKyTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APICreateChuKyPhatHanh",
                "api/admin/create/chu-ky",
                new { controller = "QuanLyChuKyTruyen", action = "ThemChuKy", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateChuKyPhatHanh",
                "api/admin/update/chu-ky",
                new { controller = "QuanLyChuKyTruyen", action = "UpdateChuKy", id = UrlParameter.Optional }
            );

            //loại truyện
            context.Routes.MapHttpRoute(
                "APIDanhSachLoaiTruyen",
                "api/admin/danh-sach-loai-truyen",
                new { controller = "QuanLyLoaiTruyen", action = "DanhSachLoaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APILoaiTruyenPhatHanh",
                "api/admin/loai-truyen/{id}",
                new { controller = "QuanLyLoaiTruyen", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteLoaiTruyen",
                "api/admin/delete/loai-truyen/{id}",
                new { controller = "QuanLyLoaiTruyen", action = "DeleteLoaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APICreateLoaiTruyen",
                "api/admin/create/loai-truyen",
                new { controller = "QuanLyLoaiTruyen", action = "ThemLoaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateLoaiTruyen",
                "api/admin/update/loai-truyen",
                new { controller = "QuanLyLoaiTruyen", action = "UpdateLoaiTruyen", id = UrlParameter.Optional }
            );

            //nhóm dịch----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachNhomDich",
                "api/admin/danh-sach-nhom-dich",
                new { controller = "QuanLyNhomDich", action = "DanhSachNhomDich", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APINhomDich",
                "api/admin/nhom-dich/{id}",
                new { controller = "QuanLyNhomDich", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteNhomDich",
                "api/admin/delete/nhom-dich/{id}",
                new { controller = "QuanLyNhomDich", action = "DeleteNhomDich", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APICreateNhomDich",
                "api/admin/create/nhom-dich",
                new { controller = "QuanLyNhomDich", action = "ThemNhomDich", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateNhomDich",
                "api/admin/update/nhom-dich",
                new { controller = "QuanLyNhomDich", action = "UpdateNhomDich", id = UrlParameter.Optional }
            );

            //tác giả  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTacGia",
                "api/admin/danh-sach-tac-gia",
                new { controller = "QuanLyTacGia", action = "DanhSachTacGia", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APITacGia",
                "api/admin/tac-gia/{id}",
                new { controller = "QuanLyTacGia", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTacGia",
                "api/admin/delete/tac-gia/{id}",
                new { controller = "QuanLyTacGia", action = "DeleteTacGia", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APICreateTacGia",
                "api/admin/create/tac-gia",
                new { controller = "QuanLyTacGia", action = "ThemTacGia", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTacGia",
                "api/admin/update/tac-gia",
                new { controller = "QuanLyTacGia", action = "UpdateTacGia", id = UrlParameter.Optional }
            );

            //tài khoản  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTaiKhoan",
                "api/admin/danh-sach-tai-khoan",
                new { controller = "QuanLyTaiKhoan", action = "DanhSachTacGia", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APITaiKhoan",
                "api/admin/tai-khoan/{id}",
                new { controller = "QuanLyTaiKhoan", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTaiKhoan",
                "api/admin/delete/tai-khoan/{id}",
                new { controller = "QuanLyTaiKhoan", action = "DeleteTaiKhoan", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTrangThai",
                "api/admin/update/trang-thai",
                new { controller = "QuanLyTaiKhoan", action = "UpdateTrangThaiTaiKhoan", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTaiKhoan",
                "api/admin/update/tai-khoan",
                new { controller = "QuanLyTaiKhoan", action = "UpdateTaiKhoan", id = UrlParameter.Optional }
            );
            context.Routes.MapHttpRoute(
                "APIUpdateNhom",
                "api/admin/update/nhom",
                new { controller = "QuanLyTaiKhoan", action = "UpdateNhomTaiKhoan", id = UrlParameter.Optional }
            );

            //trạng thái truyện  ----------------------------------------------------------------------------------------
            context.Routes.MapHttpRoute(
                "APIDanhSachTrangThaiTruyen",
                "api/admin/danh-sach-trang-thai-truyen",
                new { controller = "QuanLyTrangThaiTruyen", action = "DanhSachTrangThaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APITrangThaiTruyen",
                "api/admin/trang-thai-truyen/{id}",
                new { controller = "QuanLyTrangThaiTruyen", action = "Get", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIDeleteTrangThaiTruyen",
                "api/admin/delete/trang-thai-truyen/{id}",
                new { controller = "QuanLyTrangThaiTruyen", action = "DeleteTrangThaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APICreateTrangThaiTruyen",
                "api/admin/create/trang-thai-truyen",
                new { controller = "QuanLyTrangThaiTruyen", action = "ThemTrangThaiTruyen", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APIUpdateTrangThaiTruyen",
                "api/admin/update/trang-thai-truyen",
                new { controller = "QuanLyTrangThaiTruyen", action = "UpdateTrangThaiTruyen", id = UrlParameter.Optional }
            );



            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}