namespace Persistence.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class AddBitacora : DbMigration {
		public override void Up() {
			CreateTable(
					"dbo.Bitacora",
					c => new {
						Id = c.Int(nullable: false, identity: true),
						BDOrigen = c.String(),
						EntidadOrigen = c.String(),
						PKName = c.String(),
						PKValue = c.String(),
						Accion = c.String(),
						Propiedad = c.String(),
						TipoDato = c.String(),
						ValorAntes = c.String(),
						ValorDespues = c.String(),
						Correo = c.String(),
						Fecha = c.DateTime(nullable: false),
					})
					.PrimaryKey(t => t.Id);

		}

		public override void Down() {
			DropTable("dbo.Bitacora");
		}
	}
}
