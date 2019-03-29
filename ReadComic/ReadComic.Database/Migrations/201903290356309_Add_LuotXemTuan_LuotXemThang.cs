namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LuotXemTuan_LuotXemThang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LuotXemThang",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Id_Truyen = c.Int(nullable: false),
                        View = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .Index(t => t.Id_Truyen);
            
            CreateTable(
                "dbo.LuotXemTuan",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Id_Truyen = c.Int(nullable: false),
                        View = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Truyen", t => t.Id_Truyen)
                .Index(t => t.Id_Truyen);
            
            DropColumn("dbo.LuotXemNgay", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LuotXemNgay", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.LuotXemTuan", "Id_Truyen", "dbo.Truyen");
            DropForeignKey("dbo.LuotXemThang", "Id_Truyen", "dbo.Truyen");
            DropIndex("dbo.LuotXemTuan", new[] { "Id_Truyen" });
            DropIndex("dbo.LuotXemThang", new[] { "Id_Truyen" });
            DropTable("dbo.LuotXemTuan");
            DropTable("dbo.LuotXemThang");
        }
    }
}
