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
using System.Web.Http;
using System.Web.Http.Cors;
using static ReadComic.Common.Enum.ConstantsEnum;
using static ReadComic.Common.Enum.MessageEnum;

namespace ReadComic.Areas.Home.Controllers
{
    /// <summary>
    /// Controller điều hướng cho các xử lý của việc đăng ký tài khoản.
    /// Author       :   HoangNM - 28/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   Home.Models
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    //[EnableCors(origins: "http://localhost,http://truyenda.tk", headers: "*", methods: "*")]
    public class RegisterController : ApiController
    {
        /// <summary>
        /// Tạo tài khoản theo thông tin người dùng đưa lên.
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="account">Thông tin tài khoản đăng ký mà người dùng nhập vào</param>
        /// <returns>Chỗi Json chứa kết quả của việc tạo tài khoản</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: home/api/create-account
        /// </remarks>
        [HttpPost]
        public ResponseInfo CreateAccount(NewAccount account)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                if (ModelState.IsValid)
                {
                    response = new RegisterModel().TaoAccount(account);
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
            return response;
        }

        /// <summary>
        /// Kiểm tra email hoặc username đã tồn tại hay chưa.
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="value">giá trị của email hoặc username cần kiểm tra</param>
        /// <returns>Nếu có tồn tại trả về true, ngược lại trả về false</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: home/api/check-exist-acc
        /// </remarks>
        [HttpPost]
        public bool CheckExistAccount([FromBody]string value)
        {
            try
            {
                return new RegisterModel().CheckExistAccount(value);
            }
            catch
            {
                return false;
            }
        }

        
    }
}
