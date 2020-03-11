using ModelSF;
using System.Data.Entity;
namespace PersistenceSF {
	public class SFContext : DbContext {
		public SFContext()
			: base("name=SFCtx") {
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
		}

		// Definimos el DbSet de las entidades que deseamos mapear
		public virtual DbSet<Producto> Producto { get; set; }
		public virtual DbSet<Partida> Partida { get; set; }
		public virtual DbSet<Factura> Factura { get; set; }
		public virtual DbSet<Empresa> Empresa { get; set; }
		public virtual DbSet<Cliente> Cliente { get; set; }
		public virtual DbSet<Bitacora> Bitacora { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			// Seteamos el nombre de las tablas de las entidades que se mapearan
			modelBuilder.Entity<Producto>().ToTable("Productos");
			modelBuilder.Entity<Partida>().ToTable("Partidas");
			modelBuilder.Entity<Factura>().ToTable("Facturas");
			modelBuilder.Entity<Empresa>().ToTable("Empresas");
			modelBuilder.Entity<Cliente>().ToTable("Clientes");
			modelBuilder.Entity<Bitacora>().ToTable("Bitacora");

			// Configuramos las llaves primarias
			modelBuilder.Properties()
				.Where(p => p.Name == "Folio")
				.Configure(p => p.IsKey());

			modelBuilder.Entity<Empresa>()
				.HasKey(e => e.RFC)
				.Property(e => e.RFC).HasMaxLength(13);

			modelBuilder.Entity<Cliente>()
				.HasKey(e => e.RFC)
				.Property(e => e.RFC).HasMaxLength(13);

			// Configuramos las llaves foraneas
			modelBuilder.Entity<Partida>()
				.HasRequired(p => p.Producto)
				.WithMany(_p => _p.Partidas)
				.HasForeignKey(p => p.FolioProducto);

			modelBuilder.Entity<Partida>()
				.HasRequired(p => p.Factura)
				.WithMany(f => f.Partidas)
				.HasForeignKey(p => p.FolioFactura);

			modelBuilder.Entity<Factura>()
				.HasRequired(f => f.Empresa) // Propiedad de navegacion de referencia
				.WithMany(e => e.Facturas) // Propiedad de navegacion de coleccion
				.HasForeignKey(f => f.RFCEmpresa);

			modelBuilder.Entity<Factura>()
				.HasRequired(f => f.Cliente)
				.WithMany(c => c.Facturas)
				.HasForeignKey(f => f.RFCCliente);

			// Configuracion de propiedades tipo string
			modelBuilder.Entity<Producto>()
				.Property(p => p.Codigo).HasMaxLength(15);
			modelBuilder.Entity<Producto>()
				.Property(p => p.Descripcion).HasMaxLength(50);
			modelBuilder.Properties()
				.Where(x => x.Name == "Nombre")
				.Configure(x => x.HasMaxLength(50));

		}
	}
}
