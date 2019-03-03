using ReadComic.DataBase.Schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using Z.EntityFramework.Plus;
using System.Security.Cryptography;

namespace ReadComic.DataBase
{
    public class DataContext : DbContext
    {
        
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<ChuKyPhatHanh> ChuKyPhatHanhs { get; set; }
        public virtual DbSet<Chuong> Chuongs { get; set; }
        public virtual DbSet<DanhGiaTruyen> DanhGiaTruyens { get; set; }
        public virtual DbSet<ErrorMsg> ErrorMsgs { get; set; }
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
        public virtual DbSet<TrangThaiTaiKhoan> TrangThaiTaiKhoans { get; set; }
        public virtual DbSet<TrangThaiTruyen> ThaiTruyens { get; set; }
        public virtual DbSet<Truyen> Truyens { get; set; }
        public virtual DbSet<VaiTro> VaiTros { get; set; }

        public DataContext()
        //: base(@"data source=scomic.database.windows.net;initial catalog=ReadComic;User Id=minhduc;Password=5@03mn0l4ch0n9;")
        : base(@"data source=ADMIN\SQLEXPRESS;initial catalog=REadComic;User Id=sa;Password=123456;")
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
                return tokenLogin.ThongTinNguoiDung.Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}