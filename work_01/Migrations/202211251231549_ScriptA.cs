namespace work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CId = c.Int(nullable: false, identity: true),
                        CName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CId);
            
            CreateTable(
                "dbo.Toys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToyName = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        StoreDate = c.DateTime(nullable: false, storeType: "date"),
                        PicturePath = c.String(),
                        CId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CId, cascadeDelete: true)
                .Index(t => t.CId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toys", "CId", "dbo.Categories");
            DropIndex("dbo.Toys", new[] { "CId" });
            DropTable("dbo.Toys");
            DropTable("dbo.Categories");
        }
    }
}
