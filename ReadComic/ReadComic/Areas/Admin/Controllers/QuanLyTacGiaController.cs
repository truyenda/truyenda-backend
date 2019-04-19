using ReadComic.Areas.Admin.Models.QuanLyTacGia;
using ReadComic.Areas.Admin.Models.QuanLyTacGia.Schema;
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
    /// Class chứa các điều hướng liên quan đến quản lý tác giả
    /// Author       :   HoangNM - 16/03/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ControlPanel
    /// Copyright    :   Team Hoang_C#
    /// Version      :   1.0.0
    /// </remarks>
    public class QuanLyTacGiaController : ApiController
    {
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Điều hướng đến trang hiển thị danh sách tác giả.
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách chu kỳ phát hành.
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTacGia
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachTacGia(int page = 1)
        {
            ResponseInfo response = new ResponseInfo();
            //var kt = Convert.ToInt64(new GetPermission().GetQuyen("AUTHOR_MAN")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            //if (kt != 0)
            //{
                try
                {
                    response.Data = new QuanLyTacGiaModel().GetListTacGia(page);
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
            //}
            //else
            //{
            //    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
            //    response.TypeMsgError = errorMsg.Type;
            //    response.MsgError = errorMsg.Msg;
            //}
            
            return response;
        }

        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTacGiaModel().LoadTacGia(id);
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
        /// Xóa các tác giả theo danh sách id được gửi lên.
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các tác giả sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa tác giả</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteTacGia
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteTacGia(int id)
        {
            ResponseInfo response = new ResponseInfo();
            //var kt = Convert.ToInt64(new GetPermission().GetQuyen("AUTHOR_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            //if (kt != 0)
            //{
                try
                {
                    bool deleted = new QuanLyTacGiaModel().DeleteTacGia(id);
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
            //}
            //else
            //{
            //    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
            //    response.TypeMsgError = errorMsg.Type;
            //    response.MsgError = errorMsg.Msg;
            //}
            
            return response;
        }

        /// <summary>
        /// Dùng để thêm tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="data">Là tác giả cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm tác giả</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateTacGia
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemTacGia(TacGia data)
        {
            ResponseInfo response = new ResponseInfo();
            //var kt = Convert.ToInt64(new GetPermission().GetQuyen("AUTHOR_CRE")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            //if (kt != 0)
            //{
                try
                {
                    response = new QuanLyTacGiaModel().ThemTacGia(data);
                }
                catch (Exception e)
                {
                    response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    response.ThongTinBoSung1 = e.Message;
                }
            //}
            //else
            //{
            //    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
            //    response.TypeMsgError = errorMsg.Type;
            //    response.MsgError = errorMsg.Msg;
            //}
            
            return response;
        }

        /// <summary>
        /// Dùng để thay đổi thông tin tác giả
        /// Author       :   HoangNM - 16/03/2019 - create
        /// </summary>
        /// <param name="data">Là tác giả cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi thông tin tác giả</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APIUpdateTacGia
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpdateTacGia(TacGia data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            //var kt = Convert.ToInt64(new GetPermission().GetQuyen("AUTHOR_UPD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            //if (kt != 0)
            //{
                try
                {
                    response = new QuanLyTacGiaModel().UpadateTacGia(data, id);
                }
                catch (Exception e)
                {
                    response.Code = (int)ConstantsEnum.CodeResponse.ServerError;
                    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.ServerError);
                    response.TypeMsgError = errorMsg.Type;
                    response.MsgError = errorMsg.Msg;
                    response.ThongTinBoSung1 = e.Message;
                }
            //}
            //else
            //{
            //    var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
            //    response.TypeMsgError = errorMsg.Type;
            //    response.MsgError = errorMsg.Msg;
            //}
            
            return response;
        }

        /// <summary>
        /// Lấy danh sách truyện theo tác giả
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 14/04/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách truyện với tác giả tìm kiếm
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyenVoiTacGia
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachTruyenVoiTacGia(int Id_TacGia )
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTacGiaModel().GetListTruyenWithAuthor(Id_TacGia);
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
