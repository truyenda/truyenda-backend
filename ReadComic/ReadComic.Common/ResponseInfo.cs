using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ReadComic.Common.Enum.ConstantsEnum;

namespace ReadComic.Common
{
    public class ResponseInfo
    {
        // 200: Thành công
        // 201: Validate sai
        // 203: Thất bại
        // 500: Lỗi server
        // 401: Không có quyền truy cập
        // 403: Bị cấm truy nhập
        // 404: không tìm thấy
        // 405: Phương thức không được phép
        // 406: Không được chấp nhận
        // 409: Xung đột
        // 400: Bad Request

        public int Code { set; get; }

        public int TypeMsgError { set; get; }
        public string MsgError { set; get; }
        public Dictionary<string, string> ListError { set; get; }
        public string ThongTinBoSung1 { set; get; }
        public string ThongTinBoSung2 { set; get; }
        public string ThongTinBoSung3 { set; get; }
        public List<object> ThongTinBoSung4 { get; set; }
        public object ThongTinBoSung5 { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsValid { get; set; }
        public object Data { get; set; }

        public ResponseInfo()
        {
            Code = (int)CodeResponse.OK;
            TypeMsgError = 0;
            IsSuccess = false;
            IsValid = false;
        }
    }
}