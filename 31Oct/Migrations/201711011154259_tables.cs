namespace _31Oct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCities1",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.CStates1",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Customrs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CAddress",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customrs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CStates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customrs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CCities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customrs", t => t.Id)
                .Index(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.CustomrVM_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        Address = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Customrs]([Name], [Email])
                      VALUES (@Name, @Email)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Customrs]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      INSERT [dbo].[CAddress]([Id], [Address])
                      VALUES (@Id, @Address)
                      
                      INSERT [dbo].[CStates]([Id], [State])
                      VALUES (@Id, @State)
                      
                      INSERT [dbo].[CCities]([Id], [City])
                      VALUES (@Id, @City)
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Customrs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CustomrVM_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        Address = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Customrs]
                      SET [Name] = @Name, [Email] = @Email
                      WHERE ([Id] = @Id)
                      
                      UPDATE [dbo].[CAddress]
                      SET [Address] = @Address
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[CStates]
                      SET [State] = @State
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[CCities]
                      SET [City] = @City
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
            CreateStoredProcedure(
                "dbo.CustomrVM_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CAddress]
                      WHERE ([Id] = @Id)
                      
                      DELETE [dbo].[CStates]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[CCities]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[Customrs]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.CustomrVM_Delete");
            DropStoredProcedure("dbo.CustomrVM_Update");
            DropStoredProcedure("dbo.CustomrVM_Insert");
            DropForeignKey("dbo.CCities", "Id", "dbo.Customrs");
            DropForeignKey("dbo.CStates", "Id", "dbo.Customrs");
            DropForeignKey("dbo.CAddress", "Id", "dbo.Customrs");
            DropIndex("dbo.CCities", new[] { "Id" });
            DropIndex("dbo.CStates", new[] { "Id" });
            DropIndex("dbo.CAddress", new[] { "Id" });
            DropTable("dbo.CCities");
            DropTable("dbo.CStates");
            DropTable("dbo.CAddress");
            DropTable("dbo.Customrs");
            DropTable("dbo.CStates1");
            DropTable("dbo.CCities1");
        }
    }
}
