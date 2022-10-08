namespace SysHotelv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationaddidentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PersonTypes");
            AlterColumn("dbo.PersonTypes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PersonTypes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PersonTypes");
            AlterColumn("dbo.PersonTypes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PersonTypes", "Id");
        }
    }
}
