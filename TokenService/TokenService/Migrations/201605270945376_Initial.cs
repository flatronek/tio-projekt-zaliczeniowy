namespace TokenService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("public.TokenObjects");
            AddColumn("public.TokenObjects", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("public.TokenObjects", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("public.TokenObjects", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("public.TokenObjects");
            AlterColumn("public.TokenObjects", "UserId", c => c.Int(nullable: false, identity: true));
            DropColumn("public.TokenObjects", "Id");
            AddPrimaryKey("public.TokenObjects", "UserId");
        }
    }
}
