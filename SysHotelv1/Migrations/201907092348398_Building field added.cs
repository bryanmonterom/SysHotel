namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Buildingfieldadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Building", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Building");
        }
    }
}
