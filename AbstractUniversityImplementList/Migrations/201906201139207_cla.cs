namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cla : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ClassroomId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "ClassroomId");
        }
    }
}
