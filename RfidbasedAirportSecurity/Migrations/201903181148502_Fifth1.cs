namespace RfidBasedAirportSecurity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifth1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Luggages", new[] { "PassengerDetails_PassengerId", "PassengerDetails_PNR" }, "dbo.PassengerDetails");
            DropIndex("dbo.Luggages", new[] { "PassengerDetails_PassengerId", "PassengerDetails_PNR" });
            DropPrimaryKey("dbo.PassengerDetails");
            AlterColumn("dbo.PassengerDetails", "PNR", c => c.String());
            AddPrimaryKey("dbo.PassengerDetails", "PassengerId");
            CreateIndex("dbo.Luggages", "PassengerDetails_PassengerId");
            AddForeignKey("dbo.Luggages", "PassengerDetails_PassengerId", "dbo.PassengerDetails", "PassengerId");
            DropColumn("dbo.Luggages", "PassengerDetails_PNR");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Luggages", "PassengerDetails_PNR", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Luggages", "PassengerDetails_PassengerId", "dbo.PassengerDetails");
            DropIndex("dbo.Luggages", new[] { "PassengerDetails_PassengerId" });
            DropPrimaryKey("dbo.PassengerDetails");
            AlterColumn("dbo.PassengerDetails", "PNR", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PassengerDetails", new[] { "PassengerId", "PNR" });
            CreateIndex("dbo.Luggages", new[] { "PassengerDetails_PassengerId", "PassengerDetails_PNR" });
            AddForeignKey("dbo.Luggages", new[] { "PassengerDetails_PassengerId", "PassengerDetails_PNR" }, "dbo.PassengerDetails", new[] { "PassengerId", "PNR" });
        }
    }
}
