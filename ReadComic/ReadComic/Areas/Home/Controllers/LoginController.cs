using ReadComic.Areas.Home.Models;
using ReadComic.Areas.Home.Models.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReadComic.Areas.Home.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến login
    /// Author       :   HoangNM - 25/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class LoginController : ApiController
    {

        /// <summary>
        /// Xác thực thông tin người dùng gửi lên.
        /// Author: HoangNM - 25/02/2019 - create
        /// </summary>
        /// <param name="account">Đối tượng chưa thông tin tài khoản của người dùng</param>
        /// <returns>Chuỗi Json chứa kết quả kiểm tra</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: home/api/login
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage CheckLogin(TaiKhoan account)
        {
            
            ResponseInfo response = new ResponseInfo();
            try
            {
                if (ModelState.IsValid)
                {
                    response = new LoginModel().CheckAccount(account);
                    response.IsValid = true;
                }
                else
                {
                    response.Code = (int)ConstantsEnum.CodeResponse.NotValidate;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.DuLieuNhapSai);
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

            var cookie = new CookieHeaderValue("ToKen", response.ThongTinBoSung1);
            response.ThongTinBoSung1 = null;
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = "truyenda.tk";
           // cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            var resp = Request.CreateResponse(HttpStatusCode.OK, response);
            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            Request.CreateResponse(response);
            return resp;
        }

        /// <summary>
        /// Xử lý việc logout khỏi hệ thống.
        /// Author       :   HoangNM - 03/03/2019 - create
        /// </summary>
        /// <returns>Chuỗi Json chứa kết quả logout</returns>
        /// <param name="token">token của người dùng từ client gửi lên</param>
        /// <remarks>
        /// Method: POST
        /// RouterName: home/api/logout
        /// </remarks>
        [HttpPost]
        public ResponseInfo Logout()
        {
            ResponseInfo response = new ResponseInfo();
            string token= HttpContext.Current.Request.Cookies["ToKen"].Value.Replace("%3d","=");
            
            try
            {
                response = new LoginModel().RemoveToken(token);
                response.IsSuccess = true;
                response.IsValid = true;
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
