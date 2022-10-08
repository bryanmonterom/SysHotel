namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class direccion_fieldaddeds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Identification", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Identification", c => c.String());
        }
    }
}
