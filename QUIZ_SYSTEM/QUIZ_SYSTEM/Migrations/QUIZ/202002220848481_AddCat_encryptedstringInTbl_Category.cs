namespace QUIZ_SYSTEM.Migrations.QUIZ
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCat_encryptedstringInTbl_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBL_Category", "Cat_encryptedstring", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBL_Category", "Cat_encryptedstring");
        }
    }
}
