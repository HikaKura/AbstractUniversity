namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bbbb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomCourseBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassroomId = c.Int(nullable: false),
                        CourseId = c.Int(),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                        CourseBindingModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseBindingModels", t => t.CourseBindingModel_Id)
                .Index(t => t.CourseBindingModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassroomCourseBindingModels", "CourseBindingModel_Id", "dbo.CourseBindingModels");
            DropIndex("dbo.ClassroomCourseBindingModels", new[] { "CourseBindingModel_Id" });
            DropTable("dbo.ClassroomCourseBindingModels");
        }
    }
}
