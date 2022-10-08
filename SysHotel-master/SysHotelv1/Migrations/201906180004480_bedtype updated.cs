namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bedtypeupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BedTypes", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BedTypes", "Description", c => c.String());
        }
    }
}
