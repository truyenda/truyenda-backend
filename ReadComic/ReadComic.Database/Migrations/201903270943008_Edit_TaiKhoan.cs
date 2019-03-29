namespace ReadComic.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_TaiKhoan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiKhoan", "Username", c => c.String(nullable: false, maxLength: 24));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiKhoan", "Username", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
