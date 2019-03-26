namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_LuotXemNgay_2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.LuotXemNgay");
            AddColumn("dbo.LuotXemNgay", "Id", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.LuotXemNgay", "Id");
            CreateIndex("dbo.LuotXemNgay", "Id_Truyen");
            AddForeignKey("dbo.LuotXemNgay", "Id_Truyen", "dbo.Truyen", "Id");
            DropColumn("dbo.LuotXemNgay", "Id_luotXem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LuotXemNgay", "Id_luotXem", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.LuotXemNgay", "Id_Truyen", "dbo.Truyen");
            DropIndex("dbo.LuotXemNgay", new[] { "Id_Truyen" });
            DropPrimaryKey("dbo.LuotXemNgay");
            DropColumn("dbo.LuotXemNgay", "Id");
            AddPrimaryKey("dbo.LuotXemNgay", "Id_luotXem");
        }
    }
}
