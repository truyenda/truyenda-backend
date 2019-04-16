namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_PhanQuyen_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "Id_PhanQuyen", c => c.Int(nullable: false));
            CreateIndex("dbo.TaiKhoan", "Id_PhanQuyen");
            AddForeignKey("dbo.TaiKhoan", "Id_PhanQuyen", "dbo.PhanQuyen", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "Id_PhanQuyen", "dbo.PhanQuyen");
            DropIndex("dbo.TaiKhoan", new[] { "Id_PhanQuyen" });
            DropColumn("dbo.TaiKhoan", "Id_PhanQuyen");
        }
    }
}
