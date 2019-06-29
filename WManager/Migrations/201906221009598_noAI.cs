namespace WManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noAI : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaOtpremnices", "OtpremnicaId", "dbo.Otpremnicas");
            DropPrimaryKey("dbo.Otpremnicas");
            AlterColumn("dbo.Otpremnicas", "OtpremnicaId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Otpremnicas", "OtpremnicaId");
            AddForeignKey("dbo.StavkaOtpremnices", "OtpremnicaId", "dbo.Otpremnicas", "OtpremnicaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkaOtpremnices", "OtpremnicaId", "dbo.Otpremnicas");
            DropPrimaryKey("dbo.Otpremnicas");
            AlterColumn("dbo.Otpremnicas", "OtpremnicaId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Otpremnicas", "OtpremnicaId");
            AddForeignKey("dbo.StavkaOtpremnices", "OtpremnicaId", "dbo.Otpremnicas", "OtpremnicaId", cascadeDelete: true);
        }
    }
}
