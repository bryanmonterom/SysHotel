namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorwithreservatondetailfixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationDetails", "BedType_Id", "dbo.BedTypes");
            DropIndex("dbo.ReservationDetails", new[] { "BedType_Id" });
            DropColumn("dbo.ReservationDetails", "BedType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationDetails", "BedType_Id", c => c.Int());
            CreateIndex("dbo.ReservationDetails", "BedType_Id");
            AddForeignKey("dbo.ReservationDetails", "BedType_Id", "dbo.BedTypes", "Id");
        }
    }
}
