namespace ReadComic.Database.Migrations
{
    using ReadComic.DataBase.Schema;
    using System;
    using System.Data.Entity.Migrations;
    using System.Security.Cryptography;

    internal sealed class Configuration : DbMigrationsConfiguration<ReadComic.DataBase.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReadComic.DataBase.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Quyens.Add(new Quyen
            {
                BitQuyen = 1,
                TenQuyen = "Xem danh sách tài khoản thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 2,
                TenQuyen = "Sửa tài khoản thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 4,
                TenQuyen = "Thêm tài khoản thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 8,
                TenQuyen = "Xóa tài khoản thành viên"
            });
            context.Quyens.Add(new Quyen
            {
                BitQuyen = 16,
                TenQuyen = "Khóa tài khoản thành viên"
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

            context.ThongTinNguoiDungs.Add(new ThongTinNguoiDung
            {
                Ten = "MinhHoang",
                GioiTinh = true,
                NgaySinh = new DateTime(1997, 4, 16),
            });
            context.SaveChanges();

            context.TaiKhoans.Add(new TaiKhoan
            {
                Username = "Admin",
                salt_Pass = "hoang01",
                hash_Pass = GetMD5(GetSimpleMD5("admin")),
                Id_User = 1,
                Id_TrangThai = 2,
                Email = "minhhoang97hk@gmail.com"

            });
            context.VaiTros.Add(new VaiTro
            {
                TenVaiTro = "Admin",
                TongQuyen = 31
            });
            context.SaveChanges();

            context.PhanQuyens.Add(new PhanQuyen
            {
                Id_TaiKhoan = 1,
                Id_VaiTro = 1
            });
            context.SaveChanges();
        }
        /// <summary>
        /// Mã hóa MD5 của 1 chuỗi có thêm chuối khóa đầu và cuối.
        /// Author       :   HoangNM - 28/02/2019 - create
        /// </summary>
        /// <param name="str">
        /// Chuỗi cần mã hóa.
        /// </param>
        /// <returns>
        /// Chuỗi sau khi đã được mã hóa.
        /// </returns>
        public static string GetMD5(string str)
        {
            str = "READCOMIC" + str + "hoang01";
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
        /// Author       :   HoangNM - 28/02/2019 - create
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
