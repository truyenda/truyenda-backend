using ReadComic.Areas.Home.Models.HomeModel;
using ReadComic.Areas.Home.Models.HomeModel.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ReadComic.Areas.Home.Controllers
{
    /// <summary>
    /// Class chứa tương tác với trang chủ
    /// Author       :   HoangNM - 26/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class HomeController : ApiController
    {
        /// <summary>
        /// Lấy danh sách truyện vừa mới cập nhật ra. (5 truyện)
        /// Author       :   HoangNM - 26/03/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách các truyện vừa cập nhật
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetNewComicList()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                
                //return resp;
                response.Data = new HomeModel().GetDanhSachTruyenMoi();
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
        /// Lấy thông tin truyện và thông tin các chương của truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <returns>
        /// Thông tin truyên và các chương truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: GetTruyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetTruyen(int IdTruyen)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                //return resp;
                response.Data = new HomeModel().GetThongTinTruyen(IdTruyen);
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
    }
}
