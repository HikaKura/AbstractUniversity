namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Web : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Orientation = c.String(),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        StartCourse = c.String(),
                        EndCourse = c.String(),
                        Student_Count = c.Int(nullable: false),
                        StudyId = c.Int(nullable: false),
                        StudyBindingModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudyBindingModels", t => t.StudyBindingModel_Id)
                .Index(t => t.StudyBindingModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseBindingModels", "StudyBindingModel_Id", "dbo.StudyBindingModels");
            DropIndex("dbo.CourseBindingModels", new[] { "StudyBindingModel_Id" });
            DropTable("dbo.CourseBindingModels");
            DropTable("dbo.StudyBindingModels");
        }
    }
}
