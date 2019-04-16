using ReadComic.Areas.Admin.Models.QuanLyQuyen;
using ReadComic.Areas.Admin.Models.QuanLyQuyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadComic.Areas.Admin.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến quản lý quyền
    /// Author       :   HoangNM - 16/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyQuyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách quyền
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách quyền
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachQuyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachQuyen()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.IsSuccess = true;
                response.Data = new QuanLyQuyenModel().GetDanhSachQuyen();
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
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
                response.Data = new QuanLyQuyenModel().LoadQuyen(id);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Xóa quyền theo id của quyền được gửi lên.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="id">id quyền sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa quyền</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteQuyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteQuyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyQuyenModel().DeleteQuyen(id);
                if (deleted)
                {
                    response.IsSuccess = true;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.XoaDuLieuThanhCong);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                }
                else
                {
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.XoaDuLieuThatBai);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                }
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Dùng để thêm quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="data">Là quyền cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm quyền</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateQuyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemQuyen(NewQuyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyQuyenModel().ThemQuyen(data);
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Dùng để thay đổi thông tin quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="data">Là dữ liệu quyền cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin quyền</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateQuyen
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateQuyen(NewQuyen data, int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyQuyenModel().UpadateQuyen(data, id);
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }
    }
}
