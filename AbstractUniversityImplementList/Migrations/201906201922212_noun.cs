namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noun : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassroomCourses", "Number", c => c.Int(nullable: false));
            AlterColumn("dbo.ClassroomCourseBindingModels", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.ClassroomCourseBindingModels", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassroomCourseBindingModels", "Name", c => c.String());
            AlterColumn("dbo.ClassroomCourseBindingModels", "Number", c => c.Int());
            AlterColumn("dbo.ClassroomCourses", "Number", c => c.Int());
        }
    }
}
