namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthDateInAccountViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
    }
}
