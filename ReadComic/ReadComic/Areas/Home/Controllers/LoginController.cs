using ReadComic.Areas.Home.Models;
using ReadComic.Areas.Home.Models.Schema;
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
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }

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
        public ResponseInfo CheckLogin(TaiKhoan account)
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
                   // response.ListError = ModelState.GetModelErrors();
                }
            }
            catch (Exception e)
            {
                response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                //response.MsgNo = (int)MessageEnum.MsgNO.ServerError;
                //response.MsgError = new Common.Common().GetErrorMessageById(response.MsgNo.ToString());
                response.ThongTinBoSung1 = e.Message;
            }
            return response;
        }
    }
}
