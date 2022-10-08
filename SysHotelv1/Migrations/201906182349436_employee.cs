namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "IdEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "Nationality", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "BookingStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Identification", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Phone", c => c.String(nullable: false));
            CreateIndex("dbo.Reservations", "IdEmployee");
            CreateIndex("dbo.Employees", "BookingStatusId");
            AddForeignKey("dbo.Employees", "BookingStatusId", "dbo.BookingStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "IdEmployee", "dbo.Employees", "Id");
            DropColumn("dbo.Countries", "Nationaly");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "Nationaly", c => c.String(nullable: false));
            DropForeignKey("dbo.Reservations", "IdEmployee", "dbo.Employees");
            DropForeignKey("dbo.Employees", "BookingStatusId", "dbo.BookingStatus");
            DropIndex("dbo.Employees", new[] { "BookingStatusId" });
            DropIndex("dbo.Reservations", new[] { "IdEmployee" });
            AlterColumn("dbo.Employees", "Phone", c => c.String());
            AlterColumn("dbo.Employees", "Address", c => c.String());
            AlterColumn("dbo.Employees", "Identification", c => c.String());
            AlterColumn("dbo.Employees", "JobTitle", c => c.String());
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            DropColumn("dbo.Employees", "BookingStatusId");
            DropColumn("dbo.Countries", "Nationality");
            DropColumn("dbo.Reservations", "IdEmployee");
        }
    }
}
