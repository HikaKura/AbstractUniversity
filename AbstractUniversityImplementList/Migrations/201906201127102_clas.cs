namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseBindingModels", "ClassroomId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseBindingModels", "ClassroomId");
        }
    }
}
