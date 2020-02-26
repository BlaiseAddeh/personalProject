namespace QUIZ_SYSTEM.Migrations.QUIZ
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBL_ADMIN",
                c => new
                    {
                        AD_ID = c.Int(nullable: false, identity: true),
                        AD_NAME = c.String(nullable: false),
                        AD_PASSWORD = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AD_ID);
            
            CreateTable(
                "dbo.TBL_Category",
                c => new
                    {
                        Cat_Id = c.Int(nullable: false, identity: true),
                        Cat_Name = c.String(nullable: false),
                        Cat_fk_adid = c.Int(),
                        TBL_ADMIN_AD_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Cat_Id)
                .ForeignKey("dbo.TBL_ADMIN", t => t.TBL_ADMIN_AD_ID)
                .Index(t => t.TBL_ADMIN_AD_ID);
            
            CreateTable(
                "dbo.TBL_QUESTIONS",
                c => new
                    {
                        QUESTION_ID = c.Int(nullable: false, identity: true),
                        Q_TEXT = c.String(nullable: false),
                        OPA = c.String(nullable: false),
                        OPB = c.String(nullable: false),
                        OPC = c.String(nullable: false),
                        OPD = c.String(nullable: false),
                        COP = c.String(nullable: false),
                        Cat_fk_catid = c.Int(nullable: false),
                        TBL_Category_Cat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.QUESTION_ID)
                .ForeignKey("dbo.TBL_Category", t => t.TBL_Category_Cat_Id)
                .Index(t => t.TBL_Category_Cat_Id);
            
            CreateTable(
                "dbo.TBL_SETEXAM",
                c => new
                    {
                        EXAM_ID = c.Int(nullable: false, identity: true),
                        EXAM_DATE = c.DateTime(),
                        EXAM_FK_STU = c.Int(),
                        EXAM_NAME = c.String(),
                        EXAM_STD_SCORE = c.Int(),
                        TBL_STUDENT_S_ID = c.Int(),
                    })
                .PrimaryKey(t => t.EXAM_ID)
                .ForeignKey("dbo.TBL_STUDENT", t => t.TBL_STUDENT_S_ID)
                .Index(t => t.TBL_STUDENT_S_ID);
            
            CreateTable(
                "dbo.TBL_STUDENT",
                c => new
                    {
                        S_ID = c.Int(nullable: false, identity: true),
                        S_NAME = c.String(nullable: false),
                        S_PASSWORD = c.String(nullable: false),
                        S_IMAGE = c.String(),
                    })
                .PrimaryKey(t => t.S_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBL_SETEXAM", "TBL_STUDENT_S_ID", "dbo.TBL_STUDENT");
            DropForeignKey("dbo.TBL_QUESTIONS", "TBL_Category_Cat_Id", "dbo.TBL_Category");
            DropForeignKey("dbo.TBL_Category", "TBL_ADMIN_AD_ID", "dbo.TBL_ADMIN");
            DropIndex("dbo.TBL_SETEXAM", new[] { "TBL_STUDENT_S_ID" });
            DropIndex("dbo.TBL_QUESTIONS", new[] { "TBL_Category_Cat_Id" });
            DropIndex("dbo.TBL_Category", new[] { "TBL_ADMIN_AD_ID" });
            DropTable("dbo.TBL_STUDENT");
            DropTable("dbo.TBL_SETEXAM");
            DropTable("dbo.TBL_QUESTIONS");
            DropTable("dbo.TBL_Category");
            DropTable("dbo.TBL_ADMIN");
        }
    }
}
