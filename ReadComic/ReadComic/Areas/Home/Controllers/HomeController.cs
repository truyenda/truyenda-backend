using ReadComic.Areas.Home.Models.HomeModel;
using ReadComic.Areas.Home.Models.HomeModel.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
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
                
                response.Data = new HomeModel().GetDanhSachTruyenMoi();
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

                response.Data = new HomeModel().GetThongTinTruyen(IdTruyen);
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
        /// Lấy danh sách truyện tất cả các truyện
        /// Author       :   HoangNM - 05/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách tất cả các truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetAllComicList()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetDanhSachTruyen();
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
        /// Lấy danh sách truyện theo thể loại 
        /// Author       :   HoangNM - 05/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách tất cả các truyện theo thể loại 
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetComicListWithCategorys(int IdTheLoai)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetDanhSachTruyenTheoTheLoai(IdTheLoai);
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
        /// Dùng để tìm kiếm truyện theo tên
        /// Author       :   HoangNM - 13/04/2019 - create
        /// </summary>
        /// <returns>
        /// Các truyện phù hợp với mục tìm kiếm
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo SearchComic(string search)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().SearchComic(search);
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
        public ResponseInfo GetRandomComicList()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetDanhSachTruyenRandom();
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
