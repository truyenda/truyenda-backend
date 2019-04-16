using ReadComic.Areas.Admin.Models.QuanLyLoaiTruyen;
using ReadComic.Areas.Admin.Models.QuanLyLoaiTruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
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
    /// Class chứa các điều hướng liên quan đến quản lý loại truyện
    /// Author       :   HoangNM - 10/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuanLyLoaiTruyenController : ApiController
    {

        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách loại truyên.
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>

        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachLoaiTruyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachLoaiTruyen()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                List<LoaiTruyen> listLoaiTruyen = new QuanLyLoaiTruyenModel().GetDanhSachLoaiTruyen();
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

        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyLoaiTruyenModel().LoadLoaiTruyen(id);
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
        /// Xóa các loại truyện theo danh sách id loại truyện được gửi lên.
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các loại truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIDeleteLoaiTruyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteLoaiTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyLoaiTruyenModel().DeleteLoaiTruyen(id);
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
        /// Dùng để thêm loại truyện 
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="data">Là loại truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateLoaiTruyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemLoaiTruyen(LoaiTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyLoaiTruyenModel().ThemLoaiTruyen(data);
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
        /// Dùng để thay đổi thông tin loại truyện 
        /// Author       :   HoangNM - 10/03/2019 - create
        /// </summary>
        /// <param name="data">Là loại truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin loại truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateLoaiTruyen
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateLoaiTruyen(LoaiTruyen data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyLoaiTruyenModel().UpadateLoaiTruyen(data,id);
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
