namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stu : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourseBindingModels", "StartCourse");
            DropColumn("dbo.CourseBindingModels", "EndCourse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseBindingModels", "EndCourse", c => c.String());
            AddColumn("dbo.CourseBindingModels", "StartCourse", c => c.String());
        }
    }
}
