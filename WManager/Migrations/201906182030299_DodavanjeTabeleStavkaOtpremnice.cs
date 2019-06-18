namespace WManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeTabeleStavkaOtpremnice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StavkaOtpremnices",
                c => new
                    {
                        OtpremnicaId = c.Int(nullable: false),
                        Barkod = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => new { t.OtpremnicaId, t.Barkod })
                .ForeignKey("dbo.Artikals", t => t.Barkod, cascadeDelete: true)
                .ForeignKey("dbo.Otpremnicas", t => t.OtpremnicaId, cascadeDelete: true)
                .Index(t => t.OtpremnicaId)
                .Index(t => t.Barkod);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkaOtpremnices", "OtpremnicaId", "dbo.Otpremnicas");
            DropForeignKey("dbo.StavkaOtpremnices", "Barkod", "dbo.Artikals");
            DropIndex("dbo.StavkaOtpremnices", new[] { "Barkod" });
            DropIndex("dbo.StavkaOtpremnices", new[] { "OtpremnicaId" });
            DropTable("dbo.StavkaOtpremnices");
        }
    }
}
