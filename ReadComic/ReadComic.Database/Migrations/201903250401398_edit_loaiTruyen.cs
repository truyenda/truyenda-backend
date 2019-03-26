namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_loaiTruyen : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Truyen", "Id_LoaiTruyen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Truyen", "Id_LoaiTruyen", c => c.Int(nullable: false));
        }
    }
}
