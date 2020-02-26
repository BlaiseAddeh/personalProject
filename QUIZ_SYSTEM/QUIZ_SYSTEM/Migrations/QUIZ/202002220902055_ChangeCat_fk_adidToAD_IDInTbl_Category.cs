namespace QUIZ_SYSTEM.Migrations.QUIZ
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCat_fk_adidToAD_IDInTbl_Category : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBL_Category", "TBL_ADMIN_AD_ID", "dbo.TBL_ADMIN");
            DropIndex("dbo.TBL_Category", new[] { "TBL_ADMIN_AD_ID" });
            RenameColumn(table: "dbo.TBL_Category", name: "TBL_ADMIN_AD_ID", newName: "AD_ID");
            AlterColumn("dbo.TBL_Category", "AD_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.TBL_Category", "AD_ID");
            AddForeignKey("dbo.TBL_Category", "AD_ID", "dbo.TBL_ADMIN", "AD_ID", cascadeDelete: true);
            DropColumn("dbo.TBL_Category", "Cat_fk_adid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBL_Category", "Cat_fk_adid", c => c.Int());
            DropForeignKey("dbo.TBL_Category", "AD_ID", "dbo.TBL_ADMIN");
            DropIndex("dbo.TBL_Category", new[] { "AD_ID" });
            AlterColumn("dbo.TBL_Category", "AD_ID", c => c.Int());
            RenameColumn(table: "dbo.TBL_Category", name: "AD_ID", newName: "TBL_ADMIN_AD_ID");
            CreateIndex("dbo.TBL_Category", "TBL_ADMIN_AD_ID");
            AddForeignKey("dbo.TBL_Category", "TBL_ADMIN_AD_ID", "dbo.TBL_ADMIN", "AD_ID");
        }
    }
}
