namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_TblThongTinNguoiDung_2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.TaiKhoan", "Id", "dbo.ThongTinNguoiDung", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "Id", "dbo.ThongTinNguoiDung");
            DropIndex("dbo.TaiKhoan", new[] { "Id" });
        }
    }
}
