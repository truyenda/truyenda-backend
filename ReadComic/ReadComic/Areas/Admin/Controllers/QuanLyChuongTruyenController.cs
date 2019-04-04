using ReadComic.Areas.Admin.Models.QuanLyChuongtruyen;
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
    public class QuanLyChuongTruyenController : ApiController
    {

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
