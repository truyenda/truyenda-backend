﻿using ReadComic.Areas.Admin.Models.QuanLyTrangThaiTruyen;
using ReadComic.Areas.Admin.Models.QuanLyTrangThaiTruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using ReadComic.Common.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReadComic.Areas.Admin.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến quản lý trạng thái truyện
    /// Author       :   HoangNM - 13/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTrangThaiTruyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách trạng thái truyện
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách trạng thái truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTrangThaiTruyen
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachTrangThaiTruyen()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                List<TrangThaiTruyen> listLoaiTruyen = new QuanLyTrangThaiTruyenModel().GetDanhSachTrangThaiTruyen();
                response.IsSuccess = true;
                response.Data = listLoaiTruyen;
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

        [HttpGet]
        public ResponseInfo GetTrangThaiTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTrangThaiTruyenModel().LoadTrangThaiTruyen(id);
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
        /// Xóa các trạng thái truyện theo danh sách id chu kỳ phát hành truyện được gửi lên.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các trạng thái phát hành truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa trạng thái truyện</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteTrangThaiTruyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteTrangThaiTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("SSTATUS_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    bool deleted = new QuanLyTrangThaiTruyenModel().DeleteTrangThaiTruyen(id);
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
                }
                else
                {
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
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
        /// Dùng để thêm trạng thái truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là trạng thái truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm trạng thái truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateTrangThaiTruyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemTrangThaiTruyen(TrangThaiTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("SSTATUS_CRE")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    response = new QuanLyTrangThaiTruyenModel().ThemTrangThai(data);
                }
                else
                {
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
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
        /// Dùng để thay đổi thông tin trạng thái truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là trạng thái truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin trạng thái truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateTrangThaiTruyen
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateTrangThaiTruyen(TrangThaiTruyen data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("SSTATUS_UPD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    response = new QuanLyTrangThaiTruyenModel().UpadateTrangThaiTruyen(data, id);
                }
                else
                {
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
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
    }
}
