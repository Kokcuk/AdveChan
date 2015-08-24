namespace AdveChan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Admin290715 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Role");
        }
    }
}
