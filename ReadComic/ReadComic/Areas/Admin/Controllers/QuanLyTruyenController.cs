using ReadComic.Areas.Admin.Models.QuanLyTruyen;
using ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema;
using ReadComic.Common;
using ReadComic.Common.Enum;
using ReadComic.Common.ErrorMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReadComic.Areas.Admin.Controllers
{
    /// <summary>
    /// Class chứa các điều hướng liên quan đến quản lý truyện
    /// Author       :   HoangNM - 1/04/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTruyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách truyên
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách tất cả truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo DanhSachTatCaTruyen(TruyenConditionSearch condition)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyen(condition);
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
                response.Data = new QuanLyTruyenModel().LoadTruyen(id);
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
        /// Xóa truyện theo danh sách id được gửi lên.
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="id">id truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa truyện</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteTruyen
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                bool deleted = new QuanLyTruyenModel().DeleteTruyen(id);
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
            return response;
        }



        /// <summary>
        /// Dùng để thay đổi thông tin truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="data">Là thông tin truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin tài truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateTruyên
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateTruyen(NewComic data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().UpadateTruyen(data);
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
        /// Dùng để cập nhật trạng thái cho truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="TrangThai">Là trạng thái truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi trạng thái tài khoản</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateTrangThaiTruyen
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateTrangThaiTruyen(T_TrangThaiTruyen TrangThai)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().UpadateTrangThaiTruyen(TrangThai);
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
        /// Dùng để cập nhật chu kỳ cho truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="chuky">Là chu kỳ truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi chu kỳ cho truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateChuKyTruyen
        /// </remarks>

        [HttpPut]
        public ResponseInfo UpdateChuKyTruyen(ChuKyTruyen chuky)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().UpadateChuKyTruyen(chuky);
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
        /// Dùng để thêm truyện
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <param name="data">Là thông tin truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIAddTruyên
        /// </remarks>

        [HttpPost]
        public ResponseInfo AddTruyen(NewComic data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().AddTruyen(data);
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
        /// Điều hướng đến trang hiển thị danh sách truyện của nhóm
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách truyện trong nhóm
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyenNhom
        /// </remarks>
        [HttpPost]
        public ResponseInfo DanhSachTruyenTrongNhom(TruyenConditionSearch condition)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyen(condition);
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
        /// Dùng để thêm tác giả cho truyên
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="data">Là thông tin chứa tác giả thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIAddTacGiaChoTruyen
        /// </remarks>

        [HttpPost]
        public ResponseInfo AddTacGiaChoTruyen(ThemTacGiaChoTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().AddTacGiaChoTruyen(data);
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
        /// Dùng để thêm thể loại cho truyện
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="data">Là thông tin thể loại cần thêm cho truyện</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIThemTheLoaiChoTruyen
        /// </remarks>

        [HttpPost]
        public ResponseInfo AddTheLoaiChoTruyen(TheLoaiChoTruyen data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response = new QuanLyTruyenModel().AddTheLoaiChoTruyen(data);
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
        /// Điều hướng đến trang hiển thị danh sách truyên
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 01/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách tất cả truyện
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyen
        /// </remarks>
        [HttpPost]
        public ResponseInfo GetJson(string test)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().XulyJson(test);
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
