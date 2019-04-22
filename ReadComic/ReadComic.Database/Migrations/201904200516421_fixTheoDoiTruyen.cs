namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTheoDoiTruyen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TheoDoiTruyen", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_ChuongDanhDau", "dbo.Chuong");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_NguoiDoc" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_Truyen" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_ChuongDanhDau" });
            DropTable("dbo.TheoDoiTruyen");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TheoDoiTruyen", "Id_ChuongDanhDau");
            CreateIndex("dbo.TheoDoiTruyen", "Id_Truyen");
            CreateIndex("dbo.TheoDoiTruyen", "Id_NguoiDoc");
            AddForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan", "Id");
            AddForeignKey("dbo.TheoDoiTruyen", "Id_ChuongDanhDau", "dbo.Chuong", "Id");
            AddForeignKey("dbo.TheoDoiTruyen", "Id_Truyen", "dbo.Truyen", "Id", cascadeDelete: true);
        }
    }
}
