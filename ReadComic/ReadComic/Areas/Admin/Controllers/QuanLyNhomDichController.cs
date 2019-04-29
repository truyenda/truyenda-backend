using ReadComic.Areas.Admin.Models.QuanLyNhomDich;
using ReadComic.Areas.Admin.Models.QuanLyNhomDich.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý nhóm dịch
    /// Author       :   HoangNM - 17/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuanLyNhomDichController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách nhóm dịch
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachNhomDich
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachNhomDich(int index = 1)
        {
            ResponseInfo response = new ResponseInfo();
                try
                {
                    response.Data = new QuanLyNhomDichModel().GetDanhSachNhomDich(index);
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

        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyNhomDichModel().LoadNhomDich(id);
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
        /// Xóa các nhóm dịch theo danh sách id chu kỳ phát hành truyện được gửi lên.
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các nhóm dịch sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa nhóm dịch</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteNhomDich
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteNhomDich(int id)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAM_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    bool deleted = new QuanLyNhomDichModel().DeleteNhomDich(id);
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
                catch (Exception e)
                {
                    response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    response.ThongTinBoSung1 = e.Message;
                }
            }
            else
            {
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
            }

            return response;
        }

        /// <summary>
        /// Dùng để thêm nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateNhomDich
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemNhomDich(NhomDich data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyNhomDichModel().ThemNhomDich(data);
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
        /// Dùng để thay đổi thông tin nhóm dịch
        /// Author       :   HoangNM - 18/03/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi nhóm dịch</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateNhomDich
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateNhomDich(NhomDich data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAM_UPD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response = new QuanLyNhomDichModel().UpadateNhomDich(data, id);
                }
                catch (Exception e)
                {
                    response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    response.ThongTinBoSung1 = e.Message;
                }
            }
            else
            {
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
            }

            return response;
        }

        /// <summary>
        /// Tìm kiếm nhóm dịch theo tên
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 27/04/2019 - create
        /// </summary>
        /// <remarks>
        /// Method: GET
        /// RouterName: APISearchNhomDich
        /// </remarks>
        [HttpGet]
        public ResponseInfo SearchDanhSachNhomDicn(string query, int index)
        {
            ResponseInfo response = new ResponseInfo();

            try
            {
                response.Data = new QuanLyNhomDichModel().GetListNhomSearch(query, index);
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
