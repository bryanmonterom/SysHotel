namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Metadataupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
