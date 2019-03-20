namespace RfidBasedAirportSecurity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PassengerAreaAccesses", "RFID_ID", "dbo.RfidInventories");
            DropIndex("dbo.PassengerAreaAccesses", new[] { "RFID_ID" });
            DropPrimaryKey("dbo.PassengerAreaAccesses");
            AlterColumn("dbo.PassengerAreaAccesses", "ZoneId", c => c.String());
            AlterColumn("dbo.PassengerAreaAccesses", "RFID_ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PassengerAreaAccesses", "RFID_ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PassengerAreaAccesses");
            AlterColumn("dbo.PassengerAreaAccesses", "RFID_ID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PassengerAreaAccesses", "ZoneId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PassengerAreaAccesses", "ZoneId");
            CreateIndex("dbo.PassengerAreaAccesses", "RFID_ID");
            AddForeignKey("dbo.PassengerAreaAccesses", "RFID_ID", "dbo.RfidInventories", "RFID_ID");
        }
    }
}
