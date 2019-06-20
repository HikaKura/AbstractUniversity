namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedbcatalog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Department", c => c.String(nullable: false));
            DropColumn("dbo.Teachers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Department", c => c.String());
        }
    }
}
