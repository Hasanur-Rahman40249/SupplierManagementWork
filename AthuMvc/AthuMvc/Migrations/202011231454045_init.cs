namespace AthuMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblGoods",
                c => new
                    {
                        GoodsId = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(nullable: false, maxLength: 50),
                        GoodsPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PDT = c.DateTime(nullable: false),
                        ImageName = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GoodsId);
            
            CreateTable(
                "dbo.tblSuppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        GoodsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId)
                .ForeignKey("dbo.tblGoods", t => t.GoodsId, cascadeDelete: true)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblRoles", "UserId", "dbo.tblUsers");
            DropForeignKey("dbo.tblSuppliers", "GoodsId", "dbo.tblGoods");
            DropIndex("dbo.tblRoles", new[] { "UserId" });
            DropIndex("dbo.tblSuppliers", new[] { "GoodsId" });
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblRoles");
            DropTable("dbo.tblSuppliers");
            DropTable("dbo.tblGoods");
        }
    }
}
