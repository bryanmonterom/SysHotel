namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelbuildingadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Rooms", "BuildingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "BuildingId");
            AddForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings", "Id", cascadeDelete: true);
            DropColumn("dbo.Rooms", "Building");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Building", c => c.String());
            DropForeignKey("dbo.Rooms", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.Rooms", new[] { "BuildingId" });
            DropColumn("dbo.Rooms", "BuildingId");
            DropTable("dbo.Buildings");
        }
    }
}
