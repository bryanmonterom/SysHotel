namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class direccion_fieldadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Direccion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Direccion");
        }
    }
}
