using ReadComic.Areas.Admin.Models.QuanLyPhanQuyen;
using ReadComic.Areas.Admin.Models.QuanLyPhanQuyen.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý phân quyền
    /// Author       :   HoangNM - 16/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyPhanQuyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách phân quyền
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách phân quyền
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachPhanQuyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachPhanQuyen()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.IsSuccess = true;
                response.Data = new QuanLyPhanQuyenModel().GetDanhSachPhanQuyen();
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
                response.Data = new QuanLyPhanQuyenModel().LoadPhanQuyen(id);
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
        /// Xóa quyền theo id của phân quyền được gửi lên.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="id">id quyền sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa phân quyền</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeletePhanQuyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeletePhanQuyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyPhanQuyenModel().DeletePhanQuyen(id);
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
        /// Dùng để thêm phân quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="data">Là phân quyền cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm phân quyền</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreatePhanQuyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemPhanQuyen(NewPhanQuyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyPhanQuyenModel().ThemPhanQuyen(data);
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
        /// Dùng để thay đổi thông tin phân quyền
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <param name="data">Là dữ liệu quyền cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin phân quyền</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdatePhanQuyen
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdatePhanQuyen(NewPhanQuyen data, int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyPhanQuyenModel().UpadatePhanQuyen(data, id);
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
        /// Điều hướng đến trang hiển thị danh sách phân quyền
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 16/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách phân quyền
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachPhanQuyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachPhanQuyenTheoTeam()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.IsSuccess = true;
                response.Data = new QuanLyPhanQuyenModel().DanhSachPhanQuyenTheoTeam();
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
