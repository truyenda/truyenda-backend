using ReadComic.Areas.Admin.Models.QuanLyTaiKhoan;
using ReadComic.Areas.Admin.Models.QuanLyTaiKhoan.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReadComic.Areas.Admin.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến quản lý tài khoản
    /// Author       :   HoangNM - 18/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuanLyTaiKhoanController : ApiController
    {
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách tài khoản
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách tài khoản
        /// </returns>
        /// <remarks>
        /// Method: Post
        /// RouterName: APIDanhSachTaiKhoan
        /// </remarks>
        [HttpPost]
        public ResponseInfo DanhSachTacGia(TaiKhoanConditionSearch condition)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTaiKhoanModel().GetListTaiKhoan(condition);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTaiKhoanModel().LoadTaiKhoan(id);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Xóa các tài khoản theo danh sách id được gửi lên.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các tác giả sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa tài khoản</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteTaiKhoan
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteTaiKhoan(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyTaiKhoanModel().DeleteTaiKhoan(id);
                if (deleted)
                {
                    response.IsSuccess = true;
                }
                else
                {
                    response.MsgError = "Xóa thất bại. Vui lòng thử lại";
                }
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }



        /// <summary>
        /// Dùng để thay đổi thông tin tài khoản
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="data">Là tài khoản cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin tài khoản</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIUpdateTaiKhoan
        /// </remarks>

        //[HttpPut]
        //public ResponseInfo UpdateTaiKhoan(TaiKhoan data)
        //{
        //    ResponseInfo response = new ResponseInfo();
        //    try
        //    {
        //        response = new QuanLyTaiKhoanModel().UpadateTaiKhoan(data);
        //    }
        //    catch (Exception e)
        //    {
        //        response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
        //        response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
        //        response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
        //        response.ThongTinBoSung1 = e.Message;
        //    }
        //    return response;
        //}

        /// <summary>
        /// Dùng để cập nhật trạng thái cho tài khoán (khóa hay bình thường)
        /// Author       :   HoangNM - 19/03/2019 - create
        /// </summary>
        /// <param name="TrangThai">Là trạng thái tài khoản cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi trạng thái tài khoản</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIUpdateTrangThai
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateTrangThaiTaiKhoan(TrangThaiTaiKhoan TrangThai)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTaiKhoanModel().UpadateTrangThaiTaiKhoan(TrangThai);
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Dùng để cập nhật nhóm mới cho tài khoản cho tài khoán (chỉ admin mới có quyền)
        /// Author       :   HoangNM - 19/03/2019 - create
        /// </summary>
        /// <param name="nhom">Là nhóm tài khoản cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi nhóm cho tài khoản</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIUpdateNhom
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateNhomTaiKhoan(Nhom nhom)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTaiKhoanModel().UpadateNhomTaiKhoan(nhom);
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }
    }
}
