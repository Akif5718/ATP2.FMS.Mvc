namespace FMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablenew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COMMENTSECs",
                c => new
                    {
                        CommunicationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectSectionId = c.Int(nullable: false),
                        Commt = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommunicationId);
            
            CreateTable(
                "dbo.SavedFiles",
                c => new
                    {
                        SavedFileId = c.Int(nullable: false, identity: true),
                        ProjectSectionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        FileLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SavedFileId);
            
            AlterColumn("dbo.EducationalBackgrounds", "School", c => c.String());
            AlterColumn("dbo.EducationalBackgrounds", "Collage", c => c.String());
            AlterColumn("dbo.EducationalBackgrounds", "UniversityPost", c => c.String());
            AlterColumn("dbo.EducationalBackgrounds", "UniversityUnder", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EducationalBackgrounds", "UniversityUnder", c => c.String(nullable: false));
            AlterColumn("dbo.EducationalBackgrounds", "UniversityPost", c => c.String(nullable: false));
            AlterColumn("dbo.EducationalBackgrounds", "Collage", c => c.String(nullable: false));
            AlterColumn("dbo.EducationalBackgrounds", "School", c => c.String(nullable: false));
            DropTable("dbo.SavedFiles");
            DropTable("dbo.COMMENTSECs");
        }
    }
}
