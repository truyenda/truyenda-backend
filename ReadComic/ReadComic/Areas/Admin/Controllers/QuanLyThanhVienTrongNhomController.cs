using ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom;
using ReadComic.Areas.Admin.Models.QuanLyThanhVienTrongNhom.Schema;
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
    public class QuanLyThanhVienTrongNhomController : ApiController
    {
        /// <summary>
        /// Lấy danh sách sách thành viên nhóm dịch của tài khoản
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <returns>
        /// Trang danh sách thành viên trong nhóm tài khoản
        /// </returns>
        /// <remarks>
        /// Method: GET
        /// RouterName: APIDanhSachNhomDichCuaTaiKhoan
        /// </remarks>
        [HttpGet]
        public ResponseInfo DanhSachNhomDichCuaTaiKhoan()
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAMMEM_LIS")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response.Data = new QuanLyThanhVienTrongNhomModel().GetDanhSachNhomDichCuaTaiKhoan();
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
            }
            else
            {
                var errorMsg = new GetErrorMsg().GetMsg((int)MessageEnum.MsgNO.BanKhongDuQuyen);
                response.TypeMsgError = errorMsg.Type;
                response.MsgError = errorMsg.Msg;
            }
                

            return response;
        }

        [HttpGet]
        public ResponseInfo Get(int id)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAMMEM_GET")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response.Data = new QuanLyThanhVienTrongNhomModel().LoadThanhVien(id);
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
        /// Dùng để thêm thành viên vào nhóm dịch
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch cần thêm</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thêm thành viên vào nhóm</returns>
        /// <remarks>
        /// Method: POST
        /// RouterName: APICreateNhomDich
        /// </remarks>
        [HttpPost]
        public ResponseInfo ThemThanhVienVaoNhom(AddThanhVien data)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAMMEM_ADD")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response = new QuanLyThanhVienTrongNhomModel().ThemThanhVienVaoNhom(data);
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
        /// Dùng để thay đổi quyền của thành viên
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <param name="data">Là nhóm dịch truyện cần thay đổi</param>
        /// <returns>Đối tượng chứa thông tin về quá trình thay đổi nhóm dịch</returns>
        /// <remarks>
        /// Method: PUT
        /// RouterName: APIUpdateNhomDich
        /// </remarks>
        [HttpPut]
        public ResponseInfo UpadateThanhVienRole(UpdateThanhVien data, int Id_TaiKhoan)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAMMEM_PER")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    response = new QuanLyThanhVienTrongNhomModel().UpadateThanhVienRole(data, Id_TaiKhoan);
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
        /// Xóa thành viên trong nhóm
        /// Author       :   HoangNM - 07/05/2019 - create
        /// </summary>
        /// <param name="ids">Danh sách id các nhóm dịch sẽ xóa</param>
        /// <returns>Đối tượng chứa thông tin về quá trình xóa nhóm dịch</returns>
        /// <remarks>
        /// Method: Delete
        /// RouterName: APIDeleteNhomDich
        /// </remarks>
        [HttpDelete]
        public ResponseInfo DeleteThanhVien(int Id_TaiKhoan)
        {
            ResponseInfo response = new ResponseInfo();
            var kt = Convert.ToInt64(new GetPermission().GetQuyen("TEAMMEM_DEL")) & Convert.ToInt64(Common.Common.GetTongQuyen());
            if (kt != 0)
            {
                try
                {
                    bool deleted = new QuanLyThanhVienTrongNhomModel().DeleteThanhVien(Id_TaiKhoan);
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
    }
}
