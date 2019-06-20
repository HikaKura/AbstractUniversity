namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nounaa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassroomCourses", "Name", c => c.String());
            AddColumn("dbo.ClassroomCourseBindingModels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassroomCourseBindingModels", "Name");
            DropColumn("dbo.ClassroomCourses", "Name");
        }
    }
}
