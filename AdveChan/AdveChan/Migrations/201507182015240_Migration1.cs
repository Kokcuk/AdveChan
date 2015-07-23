namespace AdveChan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Content", c => c.String());
            CreateIndex("dbo.Threads", "BoardId");
            AddForeignKey("dbo.Threads", "BoardId", "dbo.Boards", "Id", cascadeDelete: true);
            DropColumn("dbo.Boards", "BoardId");
            DropColumn("dbo.Posts", "PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PostId", c => c.Int(nullable: false));
            AddColumn("dbo.Boards", "BoardId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Threads", "BoardId", "dbo.Boards");
            DropIndex("dbo.Threads", new[] { "BoardId" });
            DropColumn("dbo.Posts", "Content");
        }
    }
}
