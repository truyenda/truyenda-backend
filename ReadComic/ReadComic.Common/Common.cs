
using ReadComic.Common.Schema;
using ReadComic.DataBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using TblToken = ReadComic.DataBase.Schema.Token;

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

        /// <summary>
        /// Lấy thông tin của tài khoản đang đăng nhập
        /// Author       :   HoangNM - 1/04/2019 - create
        /// </summary>
        /// <param name="token">
        /// token của tài khoản đang đăng nhập
        /// </param>
        /// <returns>
        /// Trả về tài khoản đang đăng nhập
        /// </returns>
        public static GetAccount GetAccount(string token)
        {
            DataContext context = new DataContext();
            string Token = BaoMat.Base64Decode(token);
            TblToken TblToken = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == Token);
            return context.PhanQuyens.Where(x => x.Id_TaiKhoan == TblToken.Id_TaiKhoan && !x.DelFlag).Select(x => new GetAccount
            {
                Id = x.Id_TaiKhoan,
                IdNhom = x.TaiKhoan.Id_NhomDich,
                IdQuyen = x.Id,
                TongQuyen = x.TongQuyen
            }).FirstOrDefault();
        }



        /// <summary>
        /// Chuyển đổi từ base64String về Image
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="base64String">
        /// base64String
        /// </param>
        /// <returns>
        /// file ảnh
        /// </returns>
        private System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }

        /// <summary>
        /// Lưu ảnh vào thư mục đinh trước
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="imagerPath">
        /// đường dẫn hình ảnh
        /// </param>
        /// <param name="IdCommon">
        /// Id đối tượng
        /// </param>
        /// <returns>
        /// file ảnh
        /// </returns>
        public string SaveImage(string imagerPath,int IdCommon,string name)
        {



            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + new BaoMat().ClientId);
            System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                Keys.Add("image", imagerPath);
                Keys.Add("album", new BaoMat().AlbumId);
                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/upload.xml", Keys);
                String result = Encoding.ASCII.GetString(responseArray);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                return url;
            }
            catch (Exception s)
            {
                //MessageBox.Show("Something went wrong. " + s.Message);
                return "Failed!";
            }


            //string strFileName = null;
            //if (!string.IsNullOrEmpty(imagerPath))
            //{
            //    using (Image image = Base64ToImage(imagerPath))
            //    {
            //        strFileName = "Public/Imagers/"+ DateTime.Now.ToString("yyyyMMddHHmmss_") + IdCommon +"_"+name+ ".jpg";
            //        image.Save(HttpContext.Current.Server.MapPath("~/"+strFileName), ImageFormat.Jpeg);
            //    }
            //}
            //return strFileName;
        }

        /// <summary>
        /// Chuyển đổi từ Image về base64String 
        /// Author       :   HoangNM - 03/04/2019 - create
        /// </summary>
        /// <param name="image">
        /// file ảnh cần chuyển đổi
        /// </param>
        /// <returns>
        /// chuổi base 64
        /// </returns>
        private string ImageToBase64(System.Drawing.Image image, ImageFormat format)
        {
            string base64String;
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                ms.Position = 0;
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);
            }
            return base64String;
        }

        public string GetImgager(string linkAnh)
        {
            string imagePath;
            string base64String= null;
            try
            {
                imagePath = HostingEnvironment.MapPath("~/" + linkAnh);
                if (File.Exists(imagePath))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                    {
                        if (img != null)
                        {
                            base64String = ImageToBase64(img, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return base64String;
        }


        
    }

}