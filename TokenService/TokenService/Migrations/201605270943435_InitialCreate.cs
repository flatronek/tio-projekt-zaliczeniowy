namespace TokenService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.TokenObjects",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        ValidityDate = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("public.TokenObjects");
        }
    }
}
