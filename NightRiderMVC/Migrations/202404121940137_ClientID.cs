namespace NightRiderMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClientID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ClientID");
        }
    }
}
