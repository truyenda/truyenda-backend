namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_TblThongTinNguoiDung_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung");
            DropIndex("dbo.TaiKhoan", new[] { "Id_User" });
            DropColumn("dbo.TaiKhoan", "Id_User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaiKhoan", "Id_User", c => c.Int(nullable: false));
            CreateIndex("dbo.TaiKhoan", "Id_User");
            AddForeignKey("dbo.TaiKhoan", "Id_User", "dbo.ThongTinNguoiDung", "Id", cascadeDelete: true);
        }
    }
}
