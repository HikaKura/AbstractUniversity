namespace AbstractUniversityImplementList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
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
                "dbo.ClassroomCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassroomId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.ClassroomId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartCourse = c.DateTime(nullable: false),
                        EndCourse = c.DateTime(nullable: false),
                        Content = c.String(),
                        Student_Count = c.Int(nullable: false),
                        StudyId = c.Int(nullable: false),
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
                        Department = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Studies", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Courses", "StudyId", "dbo.Studies");
            DropForeignKey("dbo.Requests", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Requests", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassroomCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassroomCourses", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.Studies", new[] { "TeacherId" });
            DropIndex("dbo.Requests", new[] { "CourseId" });
            DropIndex("dbo.Requests", new[] { "TeacherId" });
            DropIndex("dbo.Courses", new[] { "StudyId" });
            DropIndex("dbo.ClassroomCourses", new[] { "CourseId" });
            DropIndex("dbo.ClassroomCourses", new[] { "ClassroomId" });
            DropTable("dbo.Studies");
            DropTable("dbo.Teachers");
            DropTable("dbo.Requests");
            DropTable("dbo.Courses");
            DropTable("dbo.ClassroomCourses");
            DropTable("dbo.Classrooms");
        }
    }
}
