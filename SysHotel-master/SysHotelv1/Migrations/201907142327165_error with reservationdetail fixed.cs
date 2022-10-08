namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorwithreservationdetailfixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationDetails", "RoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.ReservationDetails", new[] { "RoomType_Id" });
            DropColumn("dbo.ReservationDetails", "RoomType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationDetails", "RoomType_Id", c => c.Int());
            CreateIndex("dbo.ReservationDetails", "RoomType_Id");
            AddForeignKey("dbo.ReservationDetails", "RoomType_Id", "dbo.RoomTypes", "Id");
        }
    }
}
