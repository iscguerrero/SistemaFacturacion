using ModelSF;
using PersistenceSF;
using RepositorySF.SegregatedInterfaces;
namespace RepositorySF {
	public interface IProductoRepository: IRead<Producto>, ICreate<Producto>, IUpdate<Producto>, IRemove<Producto> {
	}
	public class ProductoRepository : Repository<Producto>, IProductoRepository {
		public ProductoRepository(SFContext _ctx) {
			ctx = _ctx;
		}
	}
}
