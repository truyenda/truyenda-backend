using ReadComic.Database.Schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace ReadComic.Database
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<ChuKyPhatHanh> ChuKyPhatHanhs { get; set; }
        public virtual DbSet<Chuong> Chuongs { get; set; }
        public virtual DbSet<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual DbSet<LoaiTruyen> LoaiTruyens { get; set; }
        public virtual DbSet<LuuTacGia> LuuTacGias { get; set; }
        public virtual DbSet<LuuTheLoai> LuuTheLoais { get; set; }
        public virtual DbSet<NhomDich> NhomDiches { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<TrangThaiReset> TrangThaiResets { get; set; }
        public virtual DbSet<TrangThaiTaiKhoan> TrangThaiTaiKhoans { get; set; }
        public virtual DbSet<TrangThaiTruyen> ThaiTruyens { get; set; }
        public virtual DbSet<Truyen> Truyens { get; set; }
        public virtual DbSet<VaiTro> VaiTros { get; set; }

        public DataContext()
        : base(@"data source=ADMIN\SQLEXPRESS;initial catalog=REadComic;User Id=sa;Password=123456;")
        {
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            //Database.SetInitializer<DataContext>(new TaoDataBase());
        }
    }

    public class TaoDataBase : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            
        }

        /// <summary>
        /// Get IdAccount đang login
        /// Author       :   HoangNM - 25/02/2019 - create
        /// </summary>
        /// <returns>
        /// IdAccount nếu tồn tại, trả về 0 nếu không tồn tại
        /// </returns>
        public static int GetIdAccount()
        {
            try
            {
                var cookieToken = HttpContext.Current.Request.Cookies["token"];
                if (cookieToken == null)
                {
                    return 0;
                }
                var base64EncodedBytes = System.Convert.FromBase64String(HttpUtility.UrlDecode(cookieToken.Value));
                string token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                DataContext context = new DataContext();
                Token tokenLogin = context.Tokens.FirstOrDefault(x => x.TokenTaiKhoan == token && x.ThoiGianHetHan >= DateTime.Now );
                if (tokenLogin == null)
                {
                    return 0;
                }
                return tokenLogin.ThongTinNguoiDung.Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}