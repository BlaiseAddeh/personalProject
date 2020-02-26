namespace QUIZ_SYSTEM.Migrations.QUIZ
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCat_fk_catidToCat_IddInTbl_CategoryAndAddSD_IDInTBL_SETEXAM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBL_QUESTIONS", "TBL_Category_Cat_Id", "dbo.TBL_Category");
            DropForeignKey("dbo.TBL_SETEXAM", "TBL_STUDENT_S_ID", "dbo.TBL_STUDENT");
            DropIndex("dbo.TBL_QUESTIONS", new[] { "TBL_Category_Cat_Id" });
            DropIndex("dbo.TBL_SETEXAM", new[] { "TBL_STUDENT_S_ID" });
            RenameColumn(table: "dbo.TBL_QUESTIONS", name: "TBL_Category_Cat_Id", newName: "Cat_Id");
            RenameColumn(table: "dbo.TBL_SETEXAM", name: "TBL_STUDENT_S_ID", newName: "S_ID");
            AlterColumn("dbo.TBL_QUESTIONS", "Cat_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_SETEXAM", "S_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.TBL_QUESTIONS", "Cat_Id");
            CreateIndex("dbo.TBL_SETEXAM", "S_ID");
            AddForeignKey("dbo.TBL_QUESTIONS", "Cat_Id", "dbo.TBL_Category", "Cat_Id", cascadeDelete: true);
            AddForeignKey("dbo.TBL_SETEXAM", "S_ID", "dbo.TBL_STUDENT", "S_ID", cascadeDelete: true);
            DropColumn("dbo.TBL_QUESTIONS", "Cat_fk_catid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBL_QUESTIONS", "Cat_fk_catid", c => c.Int(nullable: false));
            DropForeignKey("dbo.TBL_SETEXAM", "S_ID", "dbo.TBL_STUDENT");
            DropForeignKey("dbo.TBL_QUESTIONS", "Cat_Id", "dbo.TBL_Category");
            DropIndex("dbo.TBL_SETEXAM", new[] { "S_ID" });
            DropIndex("dbo.TBL_QUESTIONS", new[] { "Cat_Id" });
            AlterColumn("dbo.TBL_SETEXAM", "S_ID", c => c.Int());
            AlterColumn("dbo.TBL_QUESTIONS", "Cat_Id", c => c.Int());
            RenameColumn(table: "dbo.TBL_SETEXAM", name: "S_ID", newName: "TBL_STUDENT_S_ID");
            RenameColumn(table: "dbo.TBL_QUESTIONS", name: "Cat_Id", newName: "TBL_Category_Cat_Id");
            CreateIndex("dbo.TBL_SETEXAM", "TBL_STUDENT_S_ID");
            CreateIndex("dbo.TBL_QUESTIONS", "TBL_Category_Cat_Id");
            AddForeignKey("dbo.TBL_SETEXAM", "TBL_STUDENT_S_ID", "dbo.TBL_STUDENT", "S_ID");
            AddForeignKey("dbo.TBL_QUESTIONS", "TBL_Category_Cat_Id", "dbo.TBL_Category", "Cat_Id");
        }
    }
}
