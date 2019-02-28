using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Common.Enum
{
    public class MessageEnum
    {
        public enum MsgNO
        {
            BatBuocNhap = 1,
            SaiMaxlength = 2,
            SaiMinLength = 3,
            SaiFormatNgayThang = 4,
            EmailSaiFormat = 5,
            SaiFormatSDT = 6,
            SaiNgayBatDauVaKetThuc = 7,
            PhaiLonHon0 = 11,
            SaiFormatMatKhau = 19,
            XacNhanMatKhauSai = 20,
            KhongCoTaiKhoan = 28,
            TaiKhoanBiKhoa = 29,
            MatKhauKhongDung = 31,
            XacThucKhongHopLe = 33,
            TenDangNhapSai = 34,
            EmailKhongTonTai = 38,
            DaGuiMailKichHoat = 39,
            MatKhauSai = 20,
            ChuaChonFile = 50,
            DungLuongFileQuaLon = 51,
            FileKhongDungDinhDang = 53,
            TaiFileBiLoi = 55,
            BanKhongCoQuyenDangNhap = 56,
            BeautyIdDaTonTai = 60,
            ServerError = 500,
            permissions = 57
        }
    }
}