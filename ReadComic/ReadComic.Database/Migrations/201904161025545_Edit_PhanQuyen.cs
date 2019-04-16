namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_PhanQuyen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhanQuyen", "Id_TaiKhoan", "dbo.TaiKhoan");
            DropIndex("dbo.PhanQuyen", new[] { "Id_TaiKhoan" });
            DropColumn("dbo.PhanQuyen", "Id_TaiKhoan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhanQuyen", "Id_TaiKhoan", c => c.Int(nullable: false));
            CreateIndex("dbo.PhanQuyen", "Id_TaiKhoan");
            AddForeignKey("dbo.PhanQuyen", "Id_TaiKhoan", "dbo.TaiKhoan", "Id");
        }
    }
}
