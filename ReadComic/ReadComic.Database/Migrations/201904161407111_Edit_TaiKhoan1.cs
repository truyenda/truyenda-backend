namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_TaiKhoan1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaiKhoan", "Id", "dbo.ThongTinNguoiDung");
            DropForeignKey("dbo.BinhLuan", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan");
            DropForeignKey("dbo.ResetPassWord", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropIndex("dbo.TaiKhoan", new[] { "Id" });
            DropPrimaryKey("dbo.TaiKhoan");
            AddColumn("dbo.TaiKhoan", "Id_User", c => c.Int(nullable: false));
            AlterColumn("dbo.TaiKhoan", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TaiKhoan", "Id");
            CreateIndex("dbo.TaiKhoan", "Id_User");
            AddForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BinhLuan", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.ResetPassWord", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropForeignKey("dbo.ResetPassWord", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan");
            DropForeignKey("dbo.BinhLuan", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung");
            DropIndex("dbo.TaiKhoan", new[] { "Id_User" });
            DropPrimaryKey("dbo.TaiKhoan");
            AlterColumn("dbo.TaiKhoan", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.TaiKhoan", "Id_User");
            AddPrimaryKey("dbo.TaiKhoan", "Id");
            CreateIndex("dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.Token", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.ResetPassWord", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.DanhGiaTruyen", "Id_NguoiDanhGia", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.BinhLuan", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.TaiKhoan", "Id", "dbo.ThongTinNguoiDung", "Id");
        }
    }
}
