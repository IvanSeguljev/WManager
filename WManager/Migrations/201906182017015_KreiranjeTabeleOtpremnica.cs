namespace WManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KreiranjeTabeleOtpremnica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Otpremnicas",
                c => new
                    {
                        OtpremnicaId = c.Int(nullable: false, identity: true),
                        MenadzerId = c.String(nullable: false, maxLength: 128),
                        Lokacija = c.String(nullable: false),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OtpremnicaId)
                .ForeignKey("dbo.AspNetUsers", t => t.MenadzerId, cascadeDelete: true)
                .Index(t => t.MenadzerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Otpremnicas", "MenadzerId", "dbo.AspNetUsers");
            DropIndex("dbo.Otpremnicas", new[] { "MenadzerId" });
            DropTable("dbo.Otpremnicas");
        }
    }
}
