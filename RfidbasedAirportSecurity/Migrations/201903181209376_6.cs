namespace RfidBasedAirportSecurity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZoneAccessMetadatas",
                c => new
                    {
                        Role = c.String(nullable: false, maxLength: 128),
                        Type = c.String(nullable: false, maxLength: 128),
                        B1 = c.Boolean(nullable: false),
                        C1 = c.Boolean(nullable: false),
                        D3 = c.Boolean(nullable: false),
                        E1 = c.Boolean(nullable: false),
                        F1 = c.Boolean(nullable: false),
                        G1 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role, t.Type });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZoneAccessMetadatas");
        }
    }
}
