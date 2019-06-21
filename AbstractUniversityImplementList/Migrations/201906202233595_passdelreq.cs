namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passdelreq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "Password", c => c.String(nullable: false));
        }
    }
}
