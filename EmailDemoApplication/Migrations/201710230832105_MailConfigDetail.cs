namespace EmailDemoApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailConfigDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        HostName = c.String(),
                        PortNo = c.Int(nullable: false),
                        enableSSl = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MailConfig");
        }
    }
}
