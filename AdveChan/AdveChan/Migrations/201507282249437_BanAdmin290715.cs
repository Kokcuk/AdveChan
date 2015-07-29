namespace AdveChan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BanAdmin290715 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IpAdress = c.String(),
                        Terms = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "Ip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Ip");
            DropTable("dbo.Bans");
            DropTable("dbo.Admins");
        }
    }
}
