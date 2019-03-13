namespace ReadComic.Database.Migrations
{
    using ReadComic.DataBase.Schema;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;

    internal sealed class Configuration : DbMigrationsConfiguration<ReadComic.DataBase.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReadComic.DataBase.DataContext context)
        {
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 1,
                TenQuyen = "Bookmark,follow và đánh giá"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 2,
                TenQuyen = "Khóa tài khoản,cấp phát quyền cho thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 4,
                TenQuyen = "Xem danh sách thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 8,
                TenQuyen = "Xóa tài khoản thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 16,
                TenQuyen = "Xem danh sách tất cả các truyện"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 32,
                TenQuyen = "Xóa truyện"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 64,
                TenQuyen = "Sửa thông tin của truyện"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 128,
                TenQuyen = "Xem danh sách các nhóm dịch"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 256,
                TenQuyen = "Sửa thông tin của nhóm dịch"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 512,
                TenQuyen = "Thêm truyện"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 1024,
                TenQuyen = "Xem danh sách truyện trong nhóm"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 2048,
                TenQuyen = "Xem danh sách thành viên trong nhóm dịch"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 4096,
                TenQuyen = "Cập nhật quyền trong nhóm"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 8192,
                TenQuyen = "Xóa tài khoản trong nhóm"
            });

            context.SaveChanges();


            context.TrangThaiTaiKhoans.Add(new TrangThaiTaiKhoan
            {
                TenTrangThai = "Bình thường",
            });

            context.TrangThaiTaiKhoans.Add(new TrangThaiTaiKhoan
            {
                TenTrangThai = "Bị khóa",
            });
            context.SaveChanges();

            context.ThongTinNguoiDungs.Add(new ThongTinNguoiDung
            {
                Ten = "MinhHoang",
                GioiTinh = true,
                NgaySinh = new DateTime(1997, 4, 16),
            });
            context.SaveChanges();

            context.NhomDiches.Add(new NhomDich
            {
                TenNhomDich = "Thần Long Bang",
                MoTa = "",
                Logo = ""
            });
            context.NhomDiches.Add(new NhomDich
            {
                TenNhomDich = "Thiên Địa Hội",
                MoTa = "",
                Logo = ""
            });
            context.SaveChanges();


            context.TaiKhoans.Add(new TaiKhoan
            {
                Id_TrangThai = 1,
                Id_User = 1,
                Id_NhomDich = 1,
                Username = "Admin",
                salt_Pass = "hoang01",
                hash_Pass = GetMD5(GetSimpleMD5("admin"), "hoang01"),
                Email = "minhhoang97hk@gmail.com",
                Id_Face = "",
                Id_Google = ""

            });
            context.SaveChanges();
            context.PhanQuyens.Add(new PhanQuyen
            {
                TenVaiTro = "Admin",
                TongQuyen = 16383,
                Id_TaiKhoan = 1
            });
            context.SaveChanges();

        }
        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi cần mã hóa.
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa.
        /// </returns>
        public static string GetMD5(string str, string salt)
        {
            str = "READCOMIC" + str + salt;
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }
        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi không có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 23/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi cần mã hóa.
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa
        /// </returns>
        public static string GetSimpleMD5(string str)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }


    }
}
