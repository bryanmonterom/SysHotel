namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookingStatus", "DescriptionStatus", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookingStatus", "DescriptionStatus", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
