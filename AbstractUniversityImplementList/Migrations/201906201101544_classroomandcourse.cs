namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classroomandcourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassroomCourses", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassroomCourses", "Number");
        }
    }
}
