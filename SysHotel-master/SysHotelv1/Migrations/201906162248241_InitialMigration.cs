namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BedTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PricerPerBed = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReservationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                        ChildQty = c.Int(nullable: false),
                        AdultQty = c.Int(nullable: false),
                        RoomType_Id = c.Int(),
                        BedType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.BedTypes", t => t.BedType_Id)
                .Index(t => t.RoomId)
                .Index(t => t.ReservationId)
                .Index(t => t.RoomType_Id)
                .Index(t => t.BedType_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationDetailsId = c.Int(nullable: false),
                        PriceAdult = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceChild = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DayCount = c.Int(nullable: false),
                        SeasonalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReservationDetails", t => t.ReservationDetailsId, cascadeDelete: true)
                .Index(t => t.ReservationDetailsId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        RoomNumber = c.String(),
                        AllInclusive = c.Boolean(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        DaysNumber = c.Int(nullable: false),
                        BookingStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingStatus", t => t.BookingStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.BookingStatusId);
            
            CreateTable(
                "dbo.BookingStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DescriptionStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Identification = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        IdCountry = c.Int(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.IdCountry, cascadeDelete: true)
                .Index(t => t.IdCountry);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nationaly = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomTypeId = c.Int(nullable: false),
                        BedTypeId = c.Int(nullable: false),
                        RoomNumber = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BedTypes", t => t.BedTypeId, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId)
                .Index(t => t.BedTypeId);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PricePerRoom = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationDetails", "BedType_Id", "dbo.BedTypes");
            DropForeignKey("dbo.ReservationDetails", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.ReservationDetails", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Rooms", "BedTypeId", "dbo.BedTypes");
            DropForeignKey("dbo.ReservationDetails", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "IdCountry", "dbo.Countries");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Reservations", "BookingStatusId", "dbo.BookingStatus");
            DropForeignKey("dbo.Invoices", "ReservationDetailsId", "dbo.ReservationDetails");
            DropIndex("dbo.Rooms", new[] { "BedTypeId" });
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Clients", new[] { "IdCountry" });
            DropIndex("dbo.Reservations", new[] { "BookingStatusId" });
            DropIndex("dbo.Reservations", new[] { "ClientId" });
            DropIndex("dbo.Invoices", new[] { "ReservationDetailsId" });
            DropIndex("dbo.ReservationDetails", new[] { "BedType_Id" });
            DropIndex("dbo.ReservationDetails", new[] { "RoomType_Id" });
            DropIndex("dbo.ReservationDetails", new[] { "ReservationId" });
            DropIndex("dbo.ReservationDetails", new[] { "RoomId" });
            DropTable("dbo.PersonTypes");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Rooms");
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
            DropTable("dbo.Clients");
            DropTable("dbo.BookingStatus");
            DropTable("dbo.Reservations");
            DropTable("dbo.Invoices");
            DropTable("dbo.ReservationDetails");
            DropTable("dbo.BedTypes");
        }
    }
}
