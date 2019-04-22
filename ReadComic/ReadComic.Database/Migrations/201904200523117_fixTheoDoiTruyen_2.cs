namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTheoDoiTruyen_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TheoDoiTruyen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_NguoiDoc = c.Int(nullable: false),
                        Id_Truyen = c.Int(nullable: false),
                        Id_ChuongDanhDau = c.Int(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TheoDoiTruyen", "Id_NguoiDoc", "dbo.TaiKhoan");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.TheoDoiTruyen", "Id_ChuongDanhDau", "dbo.Chuong");
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_ChuongDanhDau" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_Truyen" });
            DropIndex("dbo.TheoDoiTruyen", new[] { "Id_NguoiDoc" });
            DropTable("dbo.TheoDoiTruyen");
        }
    }
}
