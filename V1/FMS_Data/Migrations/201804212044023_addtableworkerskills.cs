namespace FMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableworkerskills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkerSkills",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkerSkills");
        }
    }
}
