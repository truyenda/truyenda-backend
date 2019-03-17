using ReadComic.Areas.Admin.Models.QuanLyNhomDich;
using ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý nhóm dịch
    /// Author       :   HoangNM - 17/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyNhomDichController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách nhóm dịch
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachNhomDich()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyNhomDichModel().GetDanhSachNhomDich();
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
                response.Data = new QuanLyNhomDichModel().LoadNhomDich(id);
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
        /// Xóa các nhóm dịch theo danh sách id chu kỳ phát hành truyện được gửi lên.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các nhóm dịch sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa nhóm dịch</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: 
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteNhomDich(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyNhomDichModel().DeleteNhomDich(id);
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
        /// Dùng để thêm nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: ThemNhomDich
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemNhomDich(NhomDich data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyNhomDichModel().ThemNhomDich(data);
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
        /// Dùng để thay đổi thông tin nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi nhóm dịch</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: SuaNhomDich
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateNhomDich(NhomDich data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyNhomDichModel().UpadateNhomDich(data);
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
