namespace AdveChan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BoardId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ThreadId = c.Long(nullable: false),
                        Title = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Attachments = c.String(),
                        Content = c.String(),
                        Ip = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.ThreadId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Login = c.String(),
                        PasswordHash = c.String(),
                        RegisterDate = c.DateTime(),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Threads", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Threads", new[] { "BoardId" });
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Threads");
            DropTable("dbo.Boards");
        }
    }
}
