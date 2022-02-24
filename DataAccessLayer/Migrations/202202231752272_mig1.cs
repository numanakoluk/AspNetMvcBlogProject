namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "NoteImagePath", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "NoteImagePath");
        }
    }
}
