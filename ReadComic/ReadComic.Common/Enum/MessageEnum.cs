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
            DangNhapThanhCong=1,
            DangNhapThatBai=2,
            XacNhanMatKhauSai=3,
            KhongCoTaiKhoan=4,
            TaiKhoanBiKhoa = 5,
            MatKhauSai = 6,
            BanKhongDuQuyen = 7,
            DuLieuNhapSai = 8,
            TaoTaiKhoanThanhCong = 9,
            TaoTaiKhoanThatBai = 10,
            EmailDaTonTai = 11,
            UserNameDaDung = 12,
            CapNhatThongTinThanhCong = 13,
            XoaDuLieuThanhCong = 14,
            XoaDuLieuThatBai = 15,
            CapNhatDuLieuThanhCong = 16,
            ThemDuLieuThanhCong = 17,
            DangXuatThanhCong=18,
            GuiEmailThanhCong=19,
            ThayDoiMatKhauThanhCong=20,
            TokenResetHetHan=21,
            ThayDoiMatKhauThatBai = 22,




            ServerError = 500
        }
    }
}