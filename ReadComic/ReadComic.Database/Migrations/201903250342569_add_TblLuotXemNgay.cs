namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TblLuotXemNgay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Truyen", "Id_LoaiTruyen", "dbo.LoaiTruyen");
            DropIndex("dbo.Truyen", new[] { "Id_LoaiTruyen" });
            CreateTable(
                "dbo.LuotXemNgay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        View = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Truyen", "Id_luotXemNgay", c => c.Int(nullable: false));
            CreateIndex("dbo.Truyen", "Id_luotXemNgay");
            AddForeignKey("dbo.Truyen", "Id_luotXemNgay", "dbo.LuotXemNgay", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Truyen", "Id_luotXemNgay", "dbo.LuotXemNgay");
            DropIndex("dbo.Truyen", new[] { "Id_luotXemNgay" });
            DropColumn("dbo.Truyen", "Id_luotXemNgay");
            DropTable("dbo.LuotXemNgay");
            CreateIndex("dbo.Truyen", "Id_LoaiTruyen");
            AddForeignKey("dbo.Truyen", "Id_LoaiTruyen", "dbo.LoaiTruyen", "Id", cascadeDelete: true);
        }
    }
}
