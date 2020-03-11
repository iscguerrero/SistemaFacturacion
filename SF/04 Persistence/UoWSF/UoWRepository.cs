using PersistenceSF;
using RepositorySF;
namespace UoWSF {
	public interface IUoWRepository {
		IClienteRepository Cliente { get; }
		IEmpresaRepository Empresa { get; }
		IFacturaRepository Factura { get; }
		IPartidaRepository Partida { get; }
		IProductoRepository Producto { get; }
	}
	class UoWRepository : IUoWRepository {
		public IClienteRepository Cliente { get; set; }
		public IEmpresaRepository Empresa { get; set; }
		public IFacturaRepository Factura { get; set; }
		public IPartidaRepository Partida { get; set; }
		public IProductoRepository Producto { get; set; }

		public UoWRepository(SFContext ctx) {
			Cliente = new ClienteRepository(ctx);
			Empresa = new EmpresaRepository(ctx);
			Factura = new FacturaRepository(ctx);
			Partida = new PartidaRepository(ctx);
			Producto = new ProductoRepository(ctx);
		}

	}
}
