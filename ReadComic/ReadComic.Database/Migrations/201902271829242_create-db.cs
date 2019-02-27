namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BinhLuan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_TaiKhoan = c.Int(nullable: false),
                        Id_Truyen = c.Int(nullable: false),
                        NoiDung = c.String(nullable: false, maxLength: 1000),
                        TaiKhoan_Id = c.Int(),
                        Truyen_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.TaiKhoan_Id)
                .ForeignKey("dbo.Truyen", t => t.Truyen_Id)
                .Index(t => t.TaiKhoan_Id)
                .Index(t => t.Truyen_Id);
            
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_TrangThai = c.Int(nullable: false),
                        Id_User = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 50),
                        hash_Pass = c.String(nullable: false, maxLength: 50),
                        salt_Pass = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 50),
                        Id_Face = c.String(maxLength: 50),
                        Id_Google = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThongTinNguoiDung", t => t.Id_User, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiTaiKhoan", t => t.Id_TrangThai, cascadeDelete: true)
                .Index(t => t.Id_TrangThai)
                .Index(t => t.Id_User);
            
            CreateTable(
                "dbo.Bookmark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NguoiDoc = c.Int(nullable: false),
                        Id_ChuongDanhDau = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chuong", t => t.Id_ChuongDanhDau, cascadeDelete: true)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_NguoiDoc, cascadeDelete: true)
                .Index(t => t.Id_NguoiDoc)
                .Index(t => t.Id_ChuongDanhDau);
            
            CreateTable(
                "dbo.Chuong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Truyen = c.Int(nullable: false),
                        TenChuong = c.String(nullable: false, maxLength: 50),
                        SoThuTu = c.Long(nullable: false),
                        LinkAnh = c.String(nullable: false, maxLength: 256),
                        LuotXem = c.Long(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen, cascadeDelete: true)
                .Index(t => t.Id_Truyen);
            
            CreateTable(
                "dbo.Truyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Nhom = c.Int(nullable: false),
                        Id_ChuKy = c.Int(nullable: false),
                        Id_TrangThai = c.Int(nullable: false),
                        Id_LoaiTruyen = c.Int(nullable: false),
                        TenTruyen = c.String(nullable: false, maxLength: 50),
                        TenKhac = c.String(maxLength: 50),
                        DuongDan = c.String(nullable: false, maxLength: 256),
                        NamPhatHanh = c.Int(nullable: false),
                        AnhBia = c.String(nullable: false, maxLength: 256),
                        AnhDaiDien = c.String(nullable: false, maxLength: 256),
                        MoTa = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        ChuKyPhatHanh_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChuKyPhatHanh", t => t.ChuKyPhatHanh_Id)
                .ForeignKey("dbo.LoaiTruyen", t => t.Id_LoaiTruyen, cascadeDelete: true)
                .ForeignKey("dbo.NhomDich", t => t.Id_Nhom, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiTruyen", t => t.Id_TrangThai, cascadeDelete: true)
                .Index(t => t.Id_Nhom)
                .Index(t => t.Id_TrangThai)
                .Index(t => t.Id_LoaiTruyen)
                .Index(t => t.ChuKyPhatHanh_Id);
            
            CreateTable(
                "dbo.ChuKyPhatHanh",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenChuKy = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DanhGiaTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NguoiDanhGia = c.Int(nullable: false),
                        Id_Truyen = c.Int(nullable: false),
                        Diem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_NguoiDanhGia, cascadeDelete: true)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen, cascadeDelete: true)
                .Index(t => t.Id_NguoiDanhGia)
                .Index(t => t.Id_Truyen);
            
            CreateTable(
                "dbo.LoaiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LuuTacGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Truyen = c.Int(nullable: false),
                        Id_TacGia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TacGia", t => t.Id_TacGia, cascadeDelete: true)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen, cascadeDelete: true)
                .Index(t => t.Id_Truyen)
                .Index(t => t.Id_TacGia);
            
            CreateTable(
                "dbo.TacGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTacGia = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LuuTheLoai",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTruyen = c.Int(nullable: false),
                        IdTheLoai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheLoai", t => t.IdTheLoai, cascadeDelete: true)
                .ForeignKey("dbo.Truyen", t => t.IdTruyen, cascadeDelete: true)
                .Index(t => t.IdTruyen)
                .Index(t => t.IdTheLoai);
            
            CreateTable(
                "dbo.TheLoai",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTheLoai = c.String(nullable: false, maxLength: 50),
                        Mota = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NhomDich",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NhomTruong = c.Int(nullable: false),
                        TenNhomDich = c.String(nullable: false, maxLength: 50),
                        MoTa = c.String(),
                        Website = c.String(maxLength: 256),
                        Logo = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThongTinNguoiDung",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NhomDich = c.Int(nullable: false),
                        Ten = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(),
                        GioiTinh = c.Boolean(nullable: false),
                        token_EmailResetPass = c.String(maxLength: 50),
                        NhomDich_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NhomDich", t => t.NhomDich_Id)
                .Index(t => t.NhomDich_Id);
            
            CreateTable(
                "dbo.TrangThaiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTrangThai = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhanQuyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_VaiTro = c.Int(nullable: false),
                        Id_TaiKhoan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_TaiKhoan, cascadeDelete: true)
                .ForeignKey("dbo.VaiTro", t => t.Id_TaiKhoan, cascadeDelete: true)
                .Index(t => t.Id_TaiKhoan);
            
            CreateTable(
                "dbo.VaiTro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenVaiTro = c.String(nullable: false, maxLength: 50),
                        TongQuyen = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_TaiKhoan = c.Int(nullable: false),
                        ThoiGianHetHan = c.DateTime(nullable: false),
                        TokenTaiKhoan = c.String(maxLength: 50),
                        TaiKhoan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThongTinNguoiDung", t => t.Id_TaiKhoan, cascadeDelete: true)
                .ForeignKey("dbo.TaiKhoan", t => t.TaiKhoan_Id)
                .Index(t => t.Id_TaiKhoan)
                .Index(t => t.TaiKhoan_Id);
            
            CreateTable(
                "dbo.TrangThaiTaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTrangThai = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorMsg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        mgs = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BitQuyen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TenQuyen = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "Id_TrangThai", "dbo.TrangThaiTaiKhoan");
            DropForeignKey("dbo.Token", "TaiKhoan_Id", "dbo.TaiKhoan");
            DropForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.ThongTinNguoiDung");
            DropForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung");
            DropForeignKey("dbo.PhanQuyen", "Id_TaiKhoan", "dbo.VaiTro");
            DropForeignKey("dbo.PhanQuyen", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.Bookmark", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropForeignKey("dbo.Truyen", "Id_TrangThai", "dbo.TrangThaiTruyen");
            DropForeignKey("dbo.Truyen", "Id_Nhom", "dbo.NhomDich");
            DropForeignKey("dbo.ThongTinNguoiDung", "NhomDich_Id", "dbo.NhomDich");
            DropForeignKey("dbo.LuuTheLoai", "IdTruyen", "dbo.Truyen");
            DropForeignKey("dbo.LuuTheLoai", "IdTheLoai", "dbo.TheLoai");
            DropForeignKey("dbo.LuuTacGia", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.LuuTacGia", "Id_TacGia", "dbo.TacGia");
            DropForeignKey("dbo.Truyen", "Id_LoaiTruyen", "dbo.LoaiTruyen");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan");
            DropForeignKey("dbo.Chuong", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.Truyen", "ChuKyPhatHanh_Id", "dbo.ChuKyPhatHanh");
            DropForeignKey("dbo.BinhLuan", "Truyen_Id", "dbo.Truyen");
            DropForeignKey("dbo.Bookmark", "Id_ChuongDanhDau", "dbo.Chuong");
            DropForeignKey("dbo.BinhLuan", "TaiKhoan_Id", "dbo.TaiKhoan");
            DropIndex("dbo.Token", new[] { "TaiKhoan_Id" });
            DropIndex("dbo.Token", new[] { "Id_TaiKhoan" });
            DropIndex("dbo.PhanQuyen", new[] { "Id_TaiKhoan" });
            DropIndex("dbo.ThongTinNguoiDung", new[] { "NhomDich_Id" });
            DropIndex("dbo.LuuTheLoai", new[] { "IdTheLoai" });
            DropIndex("dbo.LuuTheLoai", new[] { "IdTruyen" });
            DropIndex("dbo.LuuTacGia", new[] { "Id_TacGia" });
            DropIndex("dbo.LuuTacGia", new[] { "Id_Truyen" });
            DropIndex("dbo.DanhGiaTruyen", new[] { "Id_Truyen" });
            DropIndex("dbo.DanhGiaTruyen", new[] { "Id_NguoiDanhGia" });
            DropIndex("dbo.Truyen", new[] { "ChuKyPhatHanh_Id" });
            DropIndex("dbo.Truyen", new[] { "Id_LoaiTruyen" });
            DropIndex("dbo.Truyen", new[] { "Id_TrangThai" });
            DropIndex("dbo.Truyen", new[] { "Id_Nhom" });
            DropIndex("dbo.Chuong", new[] { "Id_Truyen" });
            DropIndex("dbo.Bookmark", new[] { "Id_ChuongDanhDau" });
            DropIndex("dbo.Bookmark", new[] { "Id_NguoiDoc" });
            DropIndex("dbo.TaiKhoan", new[] { "Id_User" });
            DropIndex("dbo.TaiKhoan", new[] { "Id_TrangThai" });
            DropIndex("dbo.BinhLuan", new[] { "Truyen_Id" });
            DropIndex("dbo.BinhLuan", new[] { "TaiKhoan_Id" });
            DropTable("dbo.Quyen");
            DropTable("dbo.ErrorMsg");
            DropTable("dbo.TrangThaiTaiKhoan");
            DropTable("dbo.Token");
            DropTable("dbo.VaiTro");
            DropTable("dbo.PhanQuyen");
            DropTable("dbo.TrangThaiTruyen");
            DropTable("dbo.ThongTinNguoiDung");
            DropTable("dbo.NhomDich");
            DropTable("dbo.TheLoai");
            DropTable("dbo.LuuTheLoai");
            DropTable("dbo.TacGia");
            DropTable("dbo.LuuTacGia");
            DropTable("dbo.LoaiTruyen");
            DropTable("dbo.DanhGiaTruyen");
            DropTable("dbo.ChuKyPhatHanh");
            DropTable("dbo.Truyen");
            DropTable("dbo.Chuong");
            DropTable("dbo.Bookmark");
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.BinhLuan");
        }
    }
}
