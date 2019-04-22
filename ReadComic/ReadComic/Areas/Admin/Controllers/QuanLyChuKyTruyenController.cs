using ReadComic.Areas.Admin.Models.QuanLyChuKyTruyen.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý chu kỳ truyện
    /// Author       :   HoangNM - 10/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyChuKyTruyenController : ApiController
    {
        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách chu kỳ phát hành.
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachChuKyPhatHanh
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachChuKyPhatHanh()
        {
            ResponseInfo response = new ResponseInfo();

                try
                {
                    List<ChuKy> listLoaiTruyen = new QuanLyChuKyModel().GetDanhSachChuKyTruyen();
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
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyChuKyModel().LoadChuKy(id);
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
        /// Xóa các loại truyện theo danh sách id chu kỳ phát hành truyện được gửi lên.
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các chu kỳ phát hành truyện sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa chu kỳ phát hành truyện</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteChuKyPhatHanh
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteChuKyTruyen(int id)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("FREQUENCY_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    bool deleted = new QuanLyChuKyModel().DeleteChuKyTruyen(id);
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
        /// Dùng để thêm chu kỳ phát hành truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm loại truyện</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateChuKyPhatHanh
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemChuKy(ChuKy data)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("FREQUENCY_CRE")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response = new QuanLyChuKyModel().ThemChuKy(data);
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
        /// Dùng để thay đổi thông tin chu kỳ phát hành truyện 
        /// Author       :   HoangNM - 13/03/2019 - create
        /// </summary>
        /// <param name="data">Là chu kỳ phát hành truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin loại truyện</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateChuKyPhatHanh
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateChuKy(ChuKy data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("FREQUENCY_UPD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response = new QuanLyChuKyModel().UpadateChuKy(data, id);
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
    }
}
