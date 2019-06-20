namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stud : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Courses", new[] { "StudyId" });
            AlterColumn("dbo.Courses", "StudyId", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseBindingModels", "StudyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "StudyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "StudyId" });
            AlterColumn("dbo.CourseBindingModels", "StudyId", c => c.Int());
            AlterColumn("dbo.Courses", "StudyId", c => c.Int());
            CreateIndex("dbo.Courses", "StudyId");
        }
    }
}
