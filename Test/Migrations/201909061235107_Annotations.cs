namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "LastName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Books", "Title", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Title", c => c.String());
            AlterColumn("dbo.Authors", "LastName", c => c.String());
        }
    }
}
