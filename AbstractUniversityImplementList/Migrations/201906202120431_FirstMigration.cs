namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassroomId = c.Int(nullable: false),
                        CourseId = c.Int(),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.ClassroomId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Pavilion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartCourse = c.DateTime(nullable: false),
                        EndCourse = c.DateTime(),
                        Content = c.String(),
                        Student_Count = c.Int(nullable: false),
                        StudyId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ClassroomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Studies", t => t.StudyId)
                .Index(t => t.StudyId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        Mail = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Department = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Orientation = c.String(),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
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
                        ClassroomId = c.Int(nullable: false),
                        StudyBindingModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudyBindingModels", t => t.StudyBindingModel_Id)
                .Index(t => t.StudyBindingModel_Id);
            
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
            DropForeignKey("dbo.CourseBindingModels", "StudyBindingModel_Id", "dbo.StudyBindingModels");
            DropForeignKey("dbo.ClassroomCourseBindingModels", "CourseBindingModel_Id", "dbo.CourseBindingModels");
            DropForeignKey("dbo.Studies", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Courses", "StudyId", "dbo.Studies");
            DropForeignKey("dbo.Requests", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Requests", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassroomCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassroomCourses", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.ClassroomCourseBindingModels", new[] { "CourseBindingModel_Id" });
            DropIndex("dbo.CourseBindingModels", new[] { "StudyBindingModel_Id" });
            DropIndex("dbo.Studies", new[] { "TeacherId" });
            DropIndex("dbo.Requests", new[] { "CourseId" });
            DropIndex("dbo.Requests", new[] { "TeacherId" });
            DropIndex("dbo.Courses", new[] { "StudyId" });
            DropIndex("dbo.ClassroomCourses", new[] { "CourseId" });
            DropIndex("dbo.ClassroomCourses", new[] { "ClassroomId" });
            DropTable("dbo.ClassroomCourseBindingModels");
            DropTable("dbo.CourseBindingModels");
            DropTable("dbo.StudyBindingModels");
            DropTable("dbo.Studies");
            DropTable("dbo.Teachers");
            DropTable("dbo.Requests");
            DropTable("dbo.Courses");
            DropTable("dbo.Classrooms");
            DropTable("dbo.ClassroomCourses");
        }
    }
}
