using ReadComic.Areas.Home.Models.Information;
using ReadComic.Areas.Home.Models.Information.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadComic.Areas.Home.Controllers
{
    /// <summary>
    /// Nơi tương tác với thông tin tài khoản
    /// Author       :   HoangNM - 29/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class InformationController : ApiController
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
        /// Lấy thông tin tài khoản đang đăng nhập
        /// Author: HoangNM - 29/03/2019 - create
        /// </summary>
        /// <param name="token">token của tài khoản đang đăng nhập</param>
        /// <remarks>
        /// Method: GET
        /// RouterName: /accounts/my
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetAccount(string token)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new InformationModel().GetAccount(token);
                response.Code = 200;
                response.IsValid = true;

            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                //response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }


        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// Author       :   HoangNM - 29/03/2019 - create
        /// </summary>
        /// <returns>
        /// Thông báo của việc cập nhật thông tin tài khoản
        /// </returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: 
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateAccount(UpdateAccount account)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new InformationModel().UpdateAccount(account);
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
