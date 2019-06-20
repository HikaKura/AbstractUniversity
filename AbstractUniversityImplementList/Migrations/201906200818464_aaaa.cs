namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "EndCourse", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "EndCourse", c => c.DateTime(nullable: false));
        }
    }
}
