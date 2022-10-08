namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employeetableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        JobTitle = c.String(),
                        CountryId = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        HiredDate = c.DateTime(nullable: false),
                        Identification = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            AlterColumn("dbo.BookingStatus", "DescriptionStatus", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Clients", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Nationaly", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.RoomTypes", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.PersonTypes", "Description", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CountryId", "dbo.Countries");
            DropIndex("dbo.Employees", new[] { "CountryId" });
            AlterColumn("dbo.PersonTypes", "Description", c => c.String());
            AlterColumn("dbo.RoomTypes", "Description", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            AlterColumn("dbo.Countries", "Nationaly", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Clients", "Phone", c => c.String());
            AlterColumn("dbo.Clients", "Email", c => c.String());
            AlterColumn("dbo.Clients", "LastName", c => c.String());
            AlterColumn("dbo.Clients", "FullName", c => c.String());
            AlterColumn("dbo.BookingStatus", "DescriptionStatus", c => c.String());
            DropTable("dbo.Employees");
        }
    }
}
