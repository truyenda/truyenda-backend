using ReadComic.Areas.Home.Models.TheoDoiTruyen;
using ReadComic.Areas.Home.Models.TheoDoiTruyen.Schema;
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
        public ResponseInfo TheoDoiTruyen(AddBookMark data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                new TheoDoiTruyenModel().TheoDoiTruyen(data);
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

        //Lấy danh sách truyện theo dõi ra
        [HttpGet]
        public ResponseInfo GetTheoDoiTruyen()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new TheoDoiTruyenModel().GetTheoDoiTruyen();
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

        //Lấy danh sách truyện theo dõi ra
        [HttpDelete]
        public ResponseInfo XoaTheoDoiTruyen(int Id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                bool deleted = new TheoDoiTruyenModel().DeleteTheoDoiTruyen(Id);
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
        /// Dùng để thay đổi thông tin theo dõi truyện
        /// Author       :   HoangNM - 22/04/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin loại truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateChuKyPhatHanh
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateTheoDoi(int id, UpdateTheoDoi data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new TheoDoiTruyenModel().UpadateTheoDoi(id, data);
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
