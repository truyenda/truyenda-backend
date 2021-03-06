﻿using ReadComic.Areas.Home.Models.HomeModel;
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
        public ResponseInfo GetComicListWithCategorys(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetDanhSachTruyenTheoTheLoai(id);
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
        public ResponseInfo SearchComic(string query, int index)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().SearchComic(query, index);
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

        /// <summary>
        /// Lấy danh sách truyện theo ngày
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách các truyện vừa cập nhật
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetComicOfDay()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetComicOfDay();
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
        /// Lấy danh sách truyện theo tuần
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách các truyện dựa vào lượng view theo tuần
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetComicOfWeek()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetComicOfWeek();
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
        /// Lấy danh sách truyện theo tháng
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách các truyện dựa vào lượng view theo tháng
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetComicOfMonth()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().GetComicOfMonth();
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
        /// Người dùng đọc chương truyện
        /// Author       :   HoangNM - 19/04/2019 - create
        /// </summary>
        /// <returns>
        /// Thông tin của chương
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: 
        /// </remarks>
        [HttpGet]
        public ResponseInfo ReadComic(int Id_Chuong)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {

                response.Data = new HomeModel().ReadComic(Id_Chuong);
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
