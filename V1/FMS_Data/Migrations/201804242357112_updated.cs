namespace FMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COMMENTSECs", "UserName", c => c.String());
            AddColumn("dbo.ProjectSections", "SectionDescription", c => c.String());
            AddColumn("dbo.ProjectSections", "WorkerName", c => c.String());
            AddColumn("dbo.ProjectSections", "FinishTime", c => c.String());
            AddColumn("dbo.ProjectSections", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.PostAProjects", "ProjectSection");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostAProjects", "ProjectSection", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectSections", "Price");
            DropColumn("dbo.ProjectSections", "FinishTime");
            DropColumn("dbo.ProjectSections", "WorkerName");
            DropColumn("dbo.ProjectSections", "SectionDescription");
            DropColumn("dbo.COMMENTSECs", "UserName");
        }
    }
}
