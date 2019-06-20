namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClassroomCourseBindingModels", "Number", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClassroomCourseBindingModels", "Number", c => c.Int(nullable: false));
        }
    }
}
