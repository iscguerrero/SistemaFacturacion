namespace Persistence.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Clientes",
					c => new {
						RFC = c.String(nullable: false, maxLength: 13),
						Id = c.Int(nullable: false),
						Nombre = c.String(maxLength: 50),
						IsDeleted = c.Boolean(nullable: false, defaultValue: false),
					})
					.PrimaryKey(t => t.RFC);

			CreateTable(
					"dbo.Facturas",
					c => new {
						Folio = c.Int(nullable: false, identity: true),
						Fecha = c.DateTime(nullable: false),
						IsDeleted = c.Boolean(nullable: false, defaultValue: false),
						RFCEmpresa = c.String(nullable: false, maxLength: 13),
						RFCCliente = c.String(nullable: false, maxLength: 13),
					})
					.PrimaryKey(t => t.Folio)
					.ForeignKey("dbo.Clientes", t => t.RFCCliente, cascadeDelete: true)
					.ForeignKey("dbo.Empresas", t => t.RFCEmpresa, cascadeDelete: true)
					.Index(t => t.RFCEmpresa)
					.Index(t => t.RFCCliente);

			CreateTable(
					"dbo.Empresas",
					c => new {
						RFC = c.String(nullable: false, maxLength: 13),
						Id = c.Int(nullable: false),
						Nombre = c.String(maxLength: 50),
						IsDeleted = c.Boolean(nullable: false, defaultValue: false),
					})
					.PrimaryKey(t => t.RFC);

			CreateTable(
					"dbo.Partidas",
					c => new {
						Folio = c.Int(nullable: false, identity: true),
						NumerPartida = c.Int(nullable: false),
						Precio = c.Single(nullable: false, defaultValue: 0.0f),
						Piezas = c.Single(nullable: false, defaultValue: 0.0f),
						IVA = c.Single(nullable: false, defaultValue: 0.0f),
						IsDeleted = c.Boolean(nullable: false, defaultValue: false),
						FolioProducto = c.Int(nullable: false),
						FolioFactura = c.Int(nullable: false),
					})
					.PrimaryKey(t => t.Folio)
					.ForeignKey("dbo.Facturas", t => t.FolioFactura, cascadeDelete: true)
					.ForeignKey("dbo.Productos", t => t.FolioProducto, cascadeDelete: true)
					.Index(t => t.FolioProducto)
					.Index(t => t.FolioFactura);

			CreateTable(
					"dbo.Productos",
					c => new {
						Folio = c.Int(nullable: false, identity: true),
						Codigo = c.String(maxLength: 15),
						Descripcion = c.String(maxLength: 50),
						Costo = c.Single(nullable: false, defaultValue: 0.0f),
						Precio = c.Single(nullable: false, defaultValue: 0.0f),
						GravaIVA = c.Boolean(nullable: false, defaultValue: false),
						TasaIVA = c.Single(nullable: false, defaultValue: 0.0f),
						IsDeleted = c.Boolean(nullable: false, defaultValue: false),
					})
					.PrimaryKey(t => t.Folio);

		}

		public override void Down() {
			DropForeignKey("dbo.Partidas", "FolioProducto", "dbo.Productos");
			DropForeignKey("dbo.Partidas", "FolioFactura", "dbo.Facturas");
			DropForeignKey("dbo.Facturas", "RFCEmpresa", "dbo.Empresas");
			DropForeignKey("dbo.Facturas", "RFCCliente", "dbo.Clientes");
			DropIndex("dbo.Partidas", new[] { "FolioFactura" });
			DropIndex("dbo.Partidas", new[] { "FolioProducto" });
			DropIndex("dbo.Facturas", new[] { "RFCCliente" });
			DropIndex("dbo.Facturas", new[] { "RFCEmpresa" });
			DropTable("dbo.Productos");
			DropTable("dbo.Partidas");
			DropTable("dbo.Empresas");
			DropTable("dbo.Facturas");
			DropTable("dbo.Clientes");
		}
	}
}
