namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCourseClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassroomCourses", "Number", c => c.Int());
            DropColumn("dbo.ClassroomCourses", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassroomCourses", "Name", c => c.String());
            AlterColumn("dbo.ClassroomCourses", "Number", c => c.Int(nullable: false));
        }
    }
}
