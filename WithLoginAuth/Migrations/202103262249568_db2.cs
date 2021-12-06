namespace WithLoginAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentCourses",
                c => new
                    {
                        DeptId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeptId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: true)
                .Index(t => t.DeptId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentCourses", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.DepartmentCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.DepartmentCourses", new[] { "CourseId" });
            DropIndex("dbo.DepartmentCourses", new[] { "DeptId" });
            DropTable("dbo.DepartmentCourses");
        }
    }
}
