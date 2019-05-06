using ReadComic.Areas.Admin.Models.QuanLyTruyen;
using ReadComic.Areas.Admin.Models.QuanLyTruyen.Schema;
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
        [HttpGet]
        public ResponseInfo DanhSachTatCaTruyen(int index)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyen(index);
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
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("STORY_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
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
        public ResponseInfo UpdateTruyen(NewComic data,int id)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("STORY_UPD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    response = new QuanLyTruyenModel().UpadateTruyen(data,id);
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
                var kt = Convert.ToInt64(new GetPermission().GetQuyen("STORY_CRE")) & Convert.ToInt64(Common.Common.GetTongQuyen());
                if (kt != 0)
                {
                    response = new QuanLyTruyenModel().AddTruyen(data);
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
        [HttpGet]
        public ResponseInfo DanhSachTruyenTrongNhomCuaTaiKhoan(int index)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyenNhomCuaTaiKhoan(index);
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
        /// Điều hướng đến trang hiển thị danh sách truyện của nhóm
        /// Điều hướng về trang lỗi nếu có lỗi sảy ra.
        /// Author       :   HoangNM - 27/04/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách truyện trong nhóm
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyenNhom
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachTruyenTrongNhom(int idNhom,int index)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyenNhom(idNhom,index);
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

        /// <summary>
        /// Lấy danh sách truyện sử dụng bộ lọc
        /// Author       :   HoangNM - 27/04/2019 - create
        /// </summary>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyenFilter
        /// </remarks>
        [HttpPost]
        public ResponseInfo DanhSachTruyenFilter(int index, DataSearch data)
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetListTruyenSearch(index,data);
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
        /// Trả về 4 truyện có lượt view cao nhất
        /// Author       :   HoangNM - 06/05/2019 - create
        /// </summary>
        /// <returns>
        /// Danh sách 4 truyện có lượt view cao nhất
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachTruyenTheoLuotView
        /// </remarks>
        [HttpGet]
        public ResponseInfo GetTruyenWithViewTrending()
        {
            ResponseInfo response = new ResponseInfo();
            try
            {
                response.Data = new QuanLyTruyenModel().GetTruyenWithView();
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
