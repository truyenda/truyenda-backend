using ReadComic.Areas.Admin.Models.QuanLyTacGia;
using ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadComic.Areas.Admin.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến quản lý tác giả
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTacGiaController : ApiController
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
        /// Điều hướng đến trang hiển thị danh sách tác giả.
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpPost]
        public ResponseInfo DanhSachTacGia(TacGiaConditionSearch condition)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTacGiaModel().GetListTacGia(condition);
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
                response.Data = new QuanLyTacGiaModel().LoadTacGia(id);
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
        /// Xóa các tác giả theo danh sách id được gửi lên.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các tác giả sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa tác giả</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: 
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteTacGia(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyTacGiaModel().DeleteTacGia(id);
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
        /// Dùng để thêm tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="data">Là tác giả cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm tác giả</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: ThemTacGia
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemTacGia(TacGia data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTacGiaModel().ThemTacGia(data);
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
        /// Dùng để thay đổi thông tin tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="data">Là tác giả cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin tác giả</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: SuaTacGia
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateTacGia(TacGia data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTacGiaModel().UpadateTacGia(data);
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
