namespace PurpleNetwork.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FollowerUserEmail = c.String(),
                        FollowingUserEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Friendships");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId1 = c.String(),
                        UserId2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Subscriptions");
        }
    }
}
