using ReadComic.Areas.Admin.Models.QuanLyChuKyTruyen.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý chu kỳ truyện
    /// Author       :   HoangNM - 10/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyChuKyTruyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách chu kỳ phát hành.
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachChuKyPhatHanh()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                List<ChuKy> listLoaiTruyen = new QuanLyChuKyModel().GetDanhSachChuKyTruyen();
                response.IsSuccess = true;
                response.Data = listLoaiTruyen;
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
                response.Data = new QuanLyChuKyModel().LoadChuKy(id);
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
        /// Xóa các loại truyện theo danh sách id chu kỳ phát hành truyện được gửi lên.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các chu kỳ phát hành truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa chu kỳ phát hành truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: 
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteChuKyTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyChuKyModel().DeleteChuKyTruyen(id);
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
        /// Dùng để thêm chu kỳ phát hành truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: ThemChuKy
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemChuKy(ChuKy data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyChuKyModel().ThemChuKy(data);
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
        /// Dùng để thay đổi thông tin chu kỳ phát hành truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: SuaChuKy
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateChuKy(ChuKy data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyChuKyModel().UpadateChuKy(data);
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
