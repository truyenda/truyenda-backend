namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_LuotXemNgay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Truyen", "Id_luotXemNgay", "dbo.LuotXemNgay");
            DropIndex("dbo.Truyen", new[] { "Id_luotXemNgay" });
            DropPrimaryKey("dbo.LuotXemNgay");
            AddColumn("dbo.LuotXemNgay", "Id_luotXem", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.LuotXemNgay", "Id_Truyen", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.LuotXemNgay", "Id_luotXem");
            DropColumn("dbo.Truyen", "Id_luotXemNgay");
            DropColumn("dbo.LuotXemNgay", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LuotXemNgay", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Truyen", "Id_luotXemNgay", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.LuotXemNgay");
            DropColumn("dbo.LuotXemNgay", "Id_Truyen");
            DropColumn("dbo.LuotXemNgay", "Id_luotXem");
            AddPrimaryKey("dbo.LuotXemNgay", "Id");
            CreateIndex("dbo.Truyen", "Id_luotXemNgay");
            AddForeignKey("dbo.Truyen", "Id_luotXemNgay", "dbo.LuotXemNgay", "Id");
        }
    }
}
