namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ClassroomCourses", new[] { "CourseId" });
            AlterColumn("dbo.ClassroomCourses", "CourseId", c => c.Int());
            CreateIndex("dbo.ClassroomCourses", "CourseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ClassroomCourses", new[] { "CourseId" });
            AlterColumn("dbo.ClassroomCourses", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassroomCourses", "CourseId");
        }
    }
}
