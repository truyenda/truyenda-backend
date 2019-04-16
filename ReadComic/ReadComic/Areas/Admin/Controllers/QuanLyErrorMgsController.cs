using ReadComic.Areas.Admin.Models.QuanLyErrorMgs;
using ReadComic.Areas.Admin.Models.QuanLyErrorMgs.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý phần errorMgs
    /// Author       :   HoangNM - 14/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyErrorMgsController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách errorMgs
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách errorMgs
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachErrorMgs
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachErrorMgs()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                List<ErrorMgs> listLoaiTruyen = new QuanLyErrorMsgModel().GetDanhSachErrorMsg();
                response.IsSuccess = true;
                response.Data = listLoaiTruyen;
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
        /// Xóa ErrorMgs theo id ErrorMgs được gửi lên.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="id">id errorMsg sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa ErrorMgs</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteErrorMsg
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteErrorMsg(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyErrorMsgModel().DeleteErrorMgs(id);
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
        /// Dùng để thêm errorMsg
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateErrorMgs
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemErrorMgs(ErrorMgs data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyErrorMsgModel().ThemErrorMgs(data);
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
        /// Dùng để thay đổi thông tin ErrorMgs
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="data">Là ErrorMsg cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin ErrorMsg</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateErrorMsg
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateErrorMsg(ErrorMgs data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyErrorMsgModel().UpadateErrorMgs(data);
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
