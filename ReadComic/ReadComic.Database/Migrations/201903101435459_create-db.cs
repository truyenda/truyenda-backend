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
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_TaiKhoan)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .Index(t => t.Id_TaiKhoan)
                .Index(t => t.Id_Truyen);
            
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_TrangThai = c.Int(nullable: false),
                        Id_User = c.Int(nullable: false),
                        Id_NhomDich = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 50),
                        hash_Pass = c.String(nullable: false, maxLength: 256),
                        salt_Pass = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 256),
                        Id_Face = c.String(maxLength: 256),
                        Id_Google = c.String(maxLength: 256),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NhomDich", t => t.Id_NhomDich)
                .ForeignKey("dbo.ThongTinNguoiDung", t => t.Id_User, cascadeDelete: true)
                .ForeignKey("dbo.TrangThaiTaiKhoan", t => t.Id_TrangThai)
                .Index(t => t.Id_TrangThai)
                .Index(t => t.Id_User)
                .Index(t => t.Id_NhomDich);
            
            CreateTable(
                "dbo.DanhGiaTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NguoiDanhGia = c.Int(nullable: false),
                        Id_Truyen = c.Int(nullable: false),
                        Diem = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_NguoiDanhGia)
                .Index(t => t.Id_NguoiDanhGia)
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
                        TenTruyen = c.String(nullable: false, maxLength: 256),
                        TenKhac = c.String(maxLength: 256),
                        NamPhatHanh = c.Int(nullable: false),
                        AnhBia = c.String(nullable: false, maxLength: 256),
                        AnhDaiDien = c.String(nullable: false, maxLength: 256),
                        MoTa = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChuKyPhatHanh", t => t.Id_ChuKy)
                .ForeignKey("dbo.LoaiTruyen", t => t.Id_LoaiTruyen, cascadeDelete: true)
                .ForeignKey("dbo.NhomDich", t => t.Id_Nhom)
                .ForeignKey("dbo.TrangThaiTruyen", t => t.Id_TrangThai, cascadeDelete: true)
                .Index(t => t.Id_Nhom)
                .Index(t => t.Id_ChuKy)
                .Index(t => t.Id_TrangThai)
                .Index(t => t.Id_LoaiTruyen);
            
            CreateTable(
                "dbo.ChuKyPhatHanh",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenChuKy = c.String(nullable: false, maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chuong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Truyen = c.Int(nullable: false),
                        TenChuong = c.String(nullable: false, maxLength: 256),
                        SoThuTu = c.Single(nullable: false),
                        LinkAnh = c.String(nullable: false),
                        LuotXem = c.Long(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .Index(t => t.Id_Truyen);
            
            CreateTable(
                "dbo.TheoDoiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NguoiDoc = c.Int(nullable: false),
                        Id_Truyen = c.Int(nullable: false),
                        Id_ChuongDanhDau = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chuong", t => t.Id_ChuongDanhDau)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_NguoiDoc)
                .Index(t => t.Id_NguoiDoc)
                .Index(t => t.Id_Truyen)
                .Index(t => t.Id_ChuongDanhDau);
            
            CreateTable(
                "dbo.LoaiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTheLoai = c.String(nullable: false, maxLength: 50),
                        Mota = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LuuLoaiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTruyen = c.Int(nullable: false),
                        IdLoaiTruyen = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoaiTruyen", t => t.IdLoaiTruyen)
                .ForeignKey("dbo.Truyen", t => t.IdTruyen)
                .Index(t => t.IdTruyen)
                .Index(t => t.IdLoaiTruyen);
            
            CreateTable(
                "dbo.LuuTacGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Truyen = c.Int(nullable: false),
                        Id_TacGia = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TacGia", t => t.Id_TacGia)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .Index(t => t.Id_Truyen)
                .Index(t => t.Id_TacGia);
            
            CreateTable(
                "dbo.TacGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTacGia = c.String(nullable: false, maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NhomDich",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenNhomDich = c.String(nullable: false, maxLength: 50),
                        MoTa = c.String(),
                        Logo = c.String(maxLength: 256),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrangThaiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTrangThai = c.String(nullable: false, maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhanQuyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenVaiTro = c.String(),
                        Id_TaiKhoan = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_TaiKhoan)
                .Index(t => t.Id_TaiKhoan);
            
            CreateTable(
                "dbo.ResetPassWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_TaiKhoan = c.Int(nullable: false),
                        ThoiGianHetHan = c.DateTime(nullable: false),
                        TokenReset = c.String(maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_TaiKhoan)
                .Index(t => t.Id_TaiKhoan);
            
            CreateTable(
                "dbo.ThongTinNguoiDung",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(),
                        GioiTinh = c.Boolean(nullable: false),
                        token_EmailResetPass = c.String(maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
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
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaiKhoan", t => t.Id_TaiKhoan)
                .Index(t => t.Id_TaiKhoan);
            
            CreateTable(
                "dbo.TrangThaiTaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTrangThai = c.String(nullable: false, maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorMsg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        mgs = c.String(nullable: false, maxLength: 256),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(maxLength: 50),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        DelFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "Id_TrangThai", "dbo.TrangThaiTaiKhoan");
            DropForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropForeignKey("dbo.ResetPassWord", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.PhanQuyen", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan");
            DropForeignKey("dbo.Truyen", "Id_TrangThai", "dbo.TrangThaiTruyen");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.Truyen", "Id_Nhom", "dbo.NhomDich");
            DropForeignKey("dbo.TaiKhoan", "Id_NhomDich", "dbo.NhomDich");
            DropForeignKey("dbo.LuuTacGia", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.LuuTacGia", "Id_TacGia", "dbo.TacGia");
            DropForeignKey("dbo.LuuLoaiTruyen", "IdTruyen", "dbo.Truyen");
            DropForeignKey("dbo.Truyen", "Id_LoaiTruyen", "dbo.LoaiTruyen");
            DropForeignKey("dbo.LuuLoaiTruyen", "IdLoaiTruyen", "dbo.LoaiTruyen");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.Chuong", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_ChuongDanhDau", "dbo.Chuong");
            DropForeignKey("dbo.Truyen", "Id_ChuKy", "dbo.ChuKyPhatHanh");
            DropForeignKey("dbo.BinhLuan", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.BinhLuan", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropIndex("dbo.Token", new[] { "Id_TaiKhoan" });
            DropIndex("dbo.ResetPassWord", new[] { "Id_TaiKhoan" });
            DropIndex("dbo.PhanQuyen", new[] { "Id_TaiKhoan" });
            DropIndex("dbo.LuuTacGia", new[] { "Id_TacGia" });
            DropIndex("dbo.LuuTacGia", new[] { "Id_Truyen" });
            DropIndex("dbo.LuuLoaiTruyen", new[] { "IdLoaiTruyen" });
            DropIndex("dbo.LuuLoaiTruyen", new[] { "IdTruyen" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_ChuongDanhDau" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_Truyen" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_NguoiDoc" });
            DropIndex("dbo.Chuong", new[] { "Id_Truyen" });
            DropIndex("dbo.Truyen", new[] { "Id_LoaiTruyen" });
            DropIndex("dbo.Truyen", new[] { "Id_TrangThai" });
            DropIndex("dbo.Truyen", new[] { "Id_ChuKy" });
            DropIndex("dbo.Truyen", new[] { "Id_Nhom" });
            DropIndex("dbo.DanhGiaTruyen", new[] { "Id_Truyen" });
            DropIndex("dbo.DanhGiaTruyen", new[] { "Id_NguoiDanhGia" });
            DropIndex("dbo.TaiKhoan", new[] { "Id_NhomDich" });
            DropIndex("dbo.TaiKhoan", new[] { "Id_User" });
            DropIndex("dbo.TaiKhoan", new[] { "Id_TrangThai" });
            DropIndex("dbo.BinhLuan", new[] { "Id_Truyen" });
            DropIndex("dbo.BinhLuan", new[] { "Id_TaiKhoan" });
            DropTable("dbo.Quyen");
            DropTable("dbo.ErrorMsg");
            DropTable("dbo.TrangThaiTaiKhoan");
            DropTable("dbo.Token");
            DropTable("dbo.ThongTinNguoiDung");
            DropTable("dbo.ResetPassWord");
            DropTable("dbo.PhanQuyen");
            DropTable("dbo.TrangThaiTruyen");
            DropTable("dbo.NhomDich");
            DropTable("dbo.TacGia");
            DropTable("dbo.LuuTacGia");
            DropTable("dbo.LuuLoaiTruyen");
            DropTable("dbo.LoaiTruyen");
            DropTable("dbo.TheoDoiTruyen");
            DropTable("dbo.Chuong");
            DropTable("dbo.ChuKyPhatHanh");
            DropTable("dbo.Truyen");
            DropTable("dbo.DanhGiaTruyen");
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.BinhLuan");
        }
    }
}
