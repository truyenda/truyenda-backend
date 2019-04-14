﻿using ReadComic.Areas.Admin.Models.QuanLyChuongtruyen;
using ReadComic.Areas.Admin.Models.QuanLyChuongtruyen.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý chương truyện
    /// Author       :   HoangNM - 04/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyChuongTruyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách chương truyện
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách tất cả truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachChuongTruyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachTatCaTruyen(int CurrentPage=1, float SoThuTu=0, string TenChuong="")
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyChuongTruyenModel().GetListChuongTruyen(new ChuongConditionSearch { CurrentPage=CurrentPage,SoThuTu=SoThuTu,TenChuong=TenChuong});
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


        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyChuongTruyenModel().LoadChuongTruyen(id);
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
        /// Xóa chương truyện theo danh sách id được gửi lên.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <param name="id">id chương truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa chương truyện</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteChuongTruyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteChuongTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyChuongTruyenModel().DeleteChuongTruyen(id);
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
        /// Dùng để thay đổi thông tin chương truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="data">Là thông tin chương truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin tài truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateTruyên
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateChuongTruyen(ChuongCuaTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyChuongTruyenModel().UpadateChuongTruyen(data);
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
        /// Dùng để thêm chương truyện
        /// Author       :   HoangNM - 04/04/2019 - create
        /// </summary>
        /// <param name="data">Là loại chương truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm chương truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateChuongTruyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemChuongTruyen(ChuongCuaTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyChuongTruyenModel().ThemChuongTruyen(data);
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
