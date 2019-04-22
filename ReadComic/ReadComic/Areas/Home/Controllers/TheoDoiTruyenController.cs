using ReadComic.Areas.Home.Models.TheoDoiTruyen;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadComic.Areas.Home.Controllers
{
    public class TheoDoiTruyenController : ApiController
    {
        //theo dõi truyện
        [HttpPost]
        public ResponseInfo TheoDoiTruyen(int Id_Truyen)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                new TheoDoiTruyenModel().TheoDoiTruyen(Id_Truyen);
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
    }
}
