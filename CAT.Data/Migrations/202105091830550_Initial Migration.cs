namespace CAT.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moments",
                c => new
                    {
                        MomentId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(),
                        OwnerId = c.Guid(nullable: false),
                        MomentCategory = c.String(nullable: false),
                        DateOfMoment = c.DateTimeOffset(nullable: false, precision: 7),
                        MomentSet = c.String(nullable: false),
                        MomentSeries = c.Int(nullable: false),
                        MomentSerialNumber = c.Int(nullable: false),
                        MomentCirculatingCount = c.Int(nullable: false),
                        MomentTier = c.Int(nullable: false),
                        MomentMint = c.Int(nullable: false),
                        PurchasedInPack = c.Boolean(nullable: false),
                        PackPrice = c.Decimal(precision: 18, scale: 2),
                        AmountInPack = c.Decimal(precision: 18, scale: 2),
                        IndividualMomentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MomentId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        PlayerFirstName = c.String(nullable: false),
                        PlayerLastName = c.String(nullable: false),
                        PositionOfPlayer = c.Int(nullable: false),
                        PlayerTeam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.SoldMoments",
                c => new
                    {
                        SoldMomentId = c.Int(nullable: false, identity: true),
                        MomentId = c.Int(),
                        PlayerId = c.Int(),
                        OwnerId = c.Guid(nullable: false),
                        MomentCategory = c.String(nullable: false),
                        DateOfMoment = c.DateTimeOffset(nullable: false, precision: 7),
                        MomentSet = c.String(nullable: false),
                        MomentSeries = c.Int(nullable: false),
                        MomentSerialNumber = c.Int(nullable: false),
                        MomentCirculatingCount = c.Int(nullable: false),
                        MomentTier = c.Int(nullable: false),
                        MomentMint = c.Int(nullable: false),
                        PurchasedInPack = c.Boolean(nullable: false),
                        PackPrice = c.Decimal(precision: 18, scale: 2),
                        AmountInPack = c.Decimal(precision: 18, scale: 2),
                        IndividualMomentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoldForAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SoldMomentId)
                .ForeignKey("dbo.Moments", t => t.MomentId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .Index(t => t.MomentId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.MomentShowcases",
                c => new
                    {
                        MomentId = c.Int(nullable: false),
                        ShowcaseId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MomentId, t.ShowcaseId })
                .ForeignKey("dbo.Moments", t => t.MomentId, cascadeDelete: true)
                .ForeignKey("dbo.Showcases", t => t.ShowcaseId, cascadeDelete: true)
                .Index(t => t.MomentId)
                .Index(t => t.ShowcaseId);
            
            CreateTable(
                "dbo.Showcases",
                c => new
                    {
                        ShowcaseId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ShowcaseName = c.String(nullable: false),
                        ShowcaseDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ShowcaseId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MomentShowcases", "ShowcaseId", "dbo.Showcases");
            DropForeignKey("dbo.MomentShowcases", "MomentId", "dbo.Moments");
            DropForeignKey("dbo.Moments", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.SoldMoments", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.SoldMoments", "MomentId", "dbo.Moments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MomentShowcases", new[] { "ShowcaseId" });
            DropIndex("dbo.MomentShowcases", new[] { "MomentId" });
            DropIndex("dbo.SoldMoments", new[] { "PlayerId" });
            DropIndex("dbo.SoldMoments", new[] { "MomentId" });
            DropIndex("dbo.Moments", new[] { "PlayerId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Showcases");
            DropTable("dbo.MomentShowcases");
            DropTable("dbo.SoldMoments");
            DropTable("dbo.Players");
            DropTable("dbo.Moments");
        }
    }
}
