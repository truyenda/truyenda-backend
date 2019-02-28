using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace ReadComic.Common
{
    /// <summary>
    /// Chứa các function sẽ sử dụng chung và nhiều lần trong dự án.
    /// Author       :   HoangNM - 23/02/2019 - create
    /// </summary>
    /// <remarks>
    /// Package      :   ReadComic.Common
    /// Copyright    :   Team HoangC#
    /// Version      :   1.0.0
    /// </remarks>
    public class Common
    {
        /// <summary>
        /// Sinh chuỗi token ngẫu nhiên theo id account đăng nhập, độ dài mặc định 40 ký tự.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="id">
        /// id của account đăng nhập.
        /// </param>
        /// <param name="length">
        /// Dộ dài của token, mặc định 40 ký tự
        /// </param>
        /// <returns>
        /// Chuỗi token.
        /// </returns>
        public static string GetToken(int id, int length = 49)
        {
            string token = "";
            Random ran = new Random();
            string tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
            for (int i = 0; i < length; i++)
            {
                token += tmp.Substring(ran.Next(0, 63), 1);
            }
            token += id;
            return token;
        }

        /// <summary>
        /// Sinh chuỗi token ngẫu nhiên theo id account đăng nhập, độ dài mặc định 40 ký tự.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi không trùng nhau sẽ cộng thêm vào token.
        /// </param>
        /// <param name="length">
        /// Dộ dài của token, mặc định 40 ký tự
        /// </param>
        /// <returns>
        /// Chuỗi token.
        /// </returns>
        public static string GetToken(string str, int length = 50)
        {
            string token = "";
            Random ran = new Random();
            string tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
            for (int i = 0; i < length; i++)
            {
                token += tmp.Substring(ran.Next(0, 63), 1);
            }
            token += str;
            return token;
        }

        /// <summary>
        /// Chuyển từ tiếng việt có dấu thành tiếng việt không dấu.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="s">
        /// Chuỗi tiếng việt cần chuyển.
        /// </param>
        /// <returns>
        /// Kết quả sau khi chuyển.
        /// </returns>
        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        /// <summary>
        /// Lấy dữ liệu từ cookies theo khóa, nếu không có dữ liệu thì trả về theo dữ liệu mặc định truyền vào hoặc rỗng
        /// Author          : HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="key">Khóa cần lấy dữ liệu trong cookie</param>
        /// <param name="returnDefault">Kết quả trả về mặc định nếu không có dữ lieeujt rong cookie, mặc định là chuỗi rỗng</param>
        /// <returns>Giá trị lưu trữ trong cookie</returns>
        public static string GetCookie(string key, string returnDefault = "")
        {
            try
            {
                var httpCookie = HttpContext.Current.Request.Cookies[key];
                if (httpCookie == null)
                {
                    return returnDefault;
                }
                return BaoMat.Base64Decode(HttpUtility.UrlDecode(httpCookie.Value));
            }
            catch
            {
                return returnDefault;
            }
        }

        public static string SaveFileUpload(HttpPostedFileBase file, string folder = "/public/img/upload/", string fileName = "", List<string> typeFiles = null, int sizeFile = 10)
        {
            try
            {
                if (fileName == "")
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss_") + file.FileName;
                }
                string path = HostingEnvironment.MapPath("~" + folder + fileName);
                int fileSize = file.ContentLength;
                string mimeType = Path.GetExtension(path);
                if (typeFiles != null && typeFiles.FirstOrDefault(x => x == mimeType) == null)
                {
                    return "1";
                }
                if (fileSize / 1024 / 1024 > 10)
                {
                    return "2";
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                file.SaveAs(path);
                return folder + fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get các error message theo id để trả về client
        /// Author       :   HoangNM - 28/02/2019 - create
        /// <param name="idError">ID error cần lấy</param>
        /// <returns>error message tương ứng</returns>
        /// </summary>
        public string GetErrorMessageById(string id)
        {
            return new DataContext().ErrorMsgs.FirstOrDefault(x => !x.DelFlag && x.Id.Equals(id)).mgs;
        }
    }
}