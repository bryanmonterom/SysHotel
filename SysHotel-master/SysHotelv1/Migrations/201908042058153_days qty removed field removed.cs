namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daysqtyremovedfieldremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "DaysNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "DaysNumber", c => c.Int(nullable: false));
        }
    }
}
