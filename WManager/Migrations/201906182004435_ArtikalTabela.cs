namespace WManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtikalTabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artikals",
                c => new
                    {
                        Barkod = c.String(nullable: false, maxLength: 13),
                        Naziv = c.String(nullable: false, maxLength: 250),
                        Kolicina = c.Int(nullable: false),
                        ZemljaPorekla = c.String(nullable: false, maxLength: 250),
                        Proizvodjac = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Barkod);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Artikals");
        }
    }
}
