namespace DDD.MsSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aggregates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(maxLength: 100),
                        Events = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Type);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Aggregates", new[] { "Type" });
            DropIndex("dbo.Aggregates", new[] { "Id" });
            DropTable("dbo.Aggregates");
        }
    }
}
