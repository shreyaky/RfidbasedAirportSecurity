namespace RfidBasedAirportSecurity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FlightDetails", "Boarding_Time", c => c.DateTime());
            AlterColumn("dbo.FlightDetails", "Arrival_Time", c => c.DateTime());
            AlterColumn("dbo.FlightDetails", "Departure_Time", c => c.DateTime());
            AlterColumn("dbo.Luggages", "TimeStamp", c => c.DateTime());
            AlterColumn("dbo.PassengerDetails", "TimeStamp", c => c.DateTime());
            AlterColumn("dbo.PassengerAreaAccesses", "Access_Time", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PassengerAreaAccesses", "Access_Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PassengerDetails", "TimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Luggages", "TimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FlightDetails", "Departure_Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FlightDetails", "Arrival_Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FlightDetails", "Boarding_Time", c => c.DateTime(nullable: false));
        }
    }
}
