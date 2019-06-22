namespace WManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodavanjeKolicineNaStavkuOtpremnice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StavkaOtpremnices", "Kolicina", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StavkaOtpremnices", "Kolicina");
        }
    }
}
