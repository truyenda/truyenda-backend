using ReadComic.DataBase.Schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using Z.EntityFramework.Plus;
using System.Security.Cryptography;
using ReadComic.Database.Schema;

namespace ReadComic.DataBase
{
    public class DataContext : DbContext
    {
        
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<TheoDoiTruyen> TheoDoiTruyens { get; set; }
        public virtual DbSet<ChuKyPhatHanh> ChuKyPhatHanhs { get; set; }
        public virtual DbSet<Chuong> Chuongs { get; set; }
        public virtual DbSet<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual DbSet<ErrorMsg> ErrorMsgs { get; set; }
        public virtual DbSet<LoaiTruyen> LoaiTruyens { get; set; }
        public virtual DbSet<LuuTacGia> LuuTacGias { get; set; }
        public virtual DbSet<LuuLoaiTruyen> LuuLoaiTruyens { get; set; }
        public virtual DbSet<NhomDich> NhomDiches { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<TrangThaiTaiKhoan> TrangThaiTaiKhoans { get; set; }
        public virtual DbSet<TrangThaiTruyen> ThaiTruyens { get; set; }
        public virtual DbSet<Truyen> Truyens { get; set; }
        public virtual DbSet<ResetPassWord> ResetPassWords { get; set; }
        public virtual DbSet<LuotXemNgay> LuotXemNgays { get; set; }
        public virtual DbSet<LuotXemTuan> LuotXemTuans { get; set; }
        public virtual DbSet<LuotXemThang> LuotXemThangs { get; set; }

        public DataContext()
        
        : base(Connection.stringConnection())
        {
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            //Database.SetInitializer<DataContext>(new TaoDataBase());
        }

        public override int SaveChanges()
        {
            try
            {
                if (ChangeTracker.HasChanges())
                {
                    foreach (var entry
                        in ChangeTracker.Entries())
                    {
                        try
                        {
                            var root = (Schema.Table)entry.Entity;
                            var now = DateTime.Now;
                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    {
                                        root.Created_at = now;
                                        root.Created_by = TaoDataBase.GetIdAccount();
                                        root.Updated_at = null;
                                        root.Updated_by = null;
                                        root.DelFlag = false;
                                        break;
                                    }
                                case EntityState.Modified:
                                    {
                                        root.Updated_at = now;
                                        root.Updated_by = TaoDataBase.GetIdAccount();
                                        break;
                                    }
                            }
                        }
                        catch
                        {
                        }
                    }

                    var audit = new Audit();
                    audit.PreSaveChanges(this);
                    var rowAffecteds = base.SaveChanges();
                    audit.PostSaveChanges();

                    if (audit.Configuration.AutoSavePreAction != null)
                    {
                        audit.Configuration.AutoSavePreAction(this, audit);
                    }

                    return base.SaveChanges();
                }

                return 0;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChuKyPhatHanh>()
                .HasMany(e => e.Truyens)
                .WithRequired(e => e.ChuKyPhatHanh)
                .HasForeignKey(e => e.Id_ChuKy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chuong>()
                .HasMany(e => e.TheoDoiTruyens)
                .WithRequired(e => e.Chuong)
                .HasForeignKey(e => e.Id_ChuongDanhDau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiTruyen>()
                .HasMany(e => e.LuuLoaiTruyen)
                .WithRequired(e => e.LoaiTruyen)
                .HasForeignKey(e => e.IdLoaiTruyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhomDich>()
               .HasMany(e => e.Truyens)
               .WithRequired(e => e.NhomDich)
               .HasForeignKey(e => e.Id_Nhom)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhomDich>()
               .HasMany(e => e.TaiKhoans)
               .WithRequired(e => e.NhomDich)
               .HasForeignKey(e => e.Id_NhomDich)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TacGia>()
               .HasMany(e => e.LuuTacGias)
               .WithRequired(e => e.TacGia)
               .HasForeignKey(e => e.Id_TacGia)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.PhanQuyens)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_TaiKhoan)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.TheoDoiTruyens)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_NguoiDoc)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.BinhLuans)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_TaiKhoan)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.DanhGiaTruyens)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_NguoiDanhGia)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.Tokens)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_TaiKhoan)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
               .HasMany(e => e.ResetPassWords)
               .WithRequired(e => e.TaiKhoan)
               .HasForeignKey(e => e.Id_TaiKhoan)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrangThaiTaiKhoan>()
               .HasMany(e => e.TaiKhoans)
               .WithRequired(e => e.TrangThaiTaiKhoan)
               .HasForeignKey(e => e.Id_TrangThai)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
               .HasMany(e => e.Chuongs)
               .WithRequired(e => e.Truyen)
               .HasForeignKey(e => e.Id_Truyen)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.DanhGiaTruyens)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.Id_Truyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.LuuLoaiTruyens)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.IdTruyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.LuuTacGias)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.Id_Truyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.BinhLuans)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.Id_Truyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.TheoDoiTruyens)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.Id_Truyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
              .HasMany(e => e.LuotXemNgay)
              .WithRequired(e => e.Truyen)
              .HasForeignKey(e => e.Id_Truyen)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
             .HasMany(e => e.LuotXemTuan)
             .WithRequired(e => e.Truyen)
             .HasForeignKey(e => e.Id_Truyen)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Truyen>()
             .HasMany(e => e.LuotXemThang)
             .WithRequired(e => e.Truyen)
             .HasForeignKey(e => e.Id_Truyen)
             .WillCascadeOnDelete(false);

            
        }

    }

    public class TaoDataBase : CreateDatabaseIfNotExists<DataContext>
    {
        //protected override void Seed(DataContext context)
        //{
        //    context.Quyens.Add(new Quyen
        //    {
        //        BitQuyen=1,
        //        TenQuyen="Xem danh sách tài khoản thành viên"
        //    });
        //    context.Quyens.Add(new Quyen
        //    {
        //        BitQuyen = 2,
        //        TenQuyen = "Sửa tài khoản thành viên"
        //    });
        //    context.Quyens.Add(new Quyen
        //    {
        //        BitQuyen = 4,
        //        TenQuyen = "Thêm tài khoản thành viên"
        //    });
        //    context.Quyens.Add(new Quyen
        //    {
        //        BitQuyen = 8,
        //        TenQuyen = "Xóa tài khoản thành viên"
        //    });
        //    context.Quyens.Add(new Quyen
        //    {
        //        BitQuyen = 16,
        //        TenQuyen = "Khóa tài khoản thành viên"
        //    });
        //    context.SaveChanges();


        //    context.TrangThaiTaiKhoans.Add(new TrangThaiTaiKhoan
        //    {
        //        TenTrangThai = "Bình thường",
        //    });

        //    context.TrangThaiTaiKhoans.Add(new TrangThaiTaiKhoan
        //    {
        //        TenTrangThai = "Bị khóa",
        //    });

        //    context.ThongTinNguoiDungs.Add(new ThongTinNguoiDung
        //    {
        //        Ten="MinhHoang",
        //        GioiTinh=true,
        //        NgaySinh= new DateTime(1997, 4, 16),
        //    });
        //    context.SaveChanges();

        //    context.TaiKhoans.Add(new TaiKhoan
        //    {
        //        Username = "Admin",
        //        salt_Pass="hoang01",
        //        hash_Pass = GetMD5(GetSimpleMD5("admin")),
        //        Id_User=1,
        //        Id_TrangThai=2,
        //        Email="minhhoang97hk@gmail.com"

        //    });
        //    context.VaiTros.Add(new VaiTro
        //    {
        //        TenVaiTro="Admin",
        //        TongQuyen=31
        //    });
        //    context.SaveChanges();

        //    context.PhanQuyens.Add(new PhanQuyen
        //    {
        //        Id_TaiKhoan = 1,
        //        Id_VaiTro = 1
        //    });
        //    context.SaveChanges();

        //}

        

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
                return tokenLogin.TaiKhoan.Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}