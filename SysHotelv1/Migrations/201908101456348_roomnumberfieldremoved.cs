namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomnumberfieldremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "RoomNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "RoomNumber", c => c.String(nullable: false));
        }
    }
}
