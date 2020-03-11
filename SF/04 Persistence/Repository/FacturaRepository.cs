using ModelSF;
using PersistenceSF;
using RepositorySF.SegregatedInterfaces;
namespace RepositorySF {
	public interface IFacturaRepository : IRead<Factura>, ICreate<Factura>, IUpdate<Factura>, IRemove<Factura> {

	}
	public class FacturaRepository: Repository<Factura>, IFacturaRepository {
		public FacturaRepository(SFContext _ctx) {
			ctx = _ctx;
		}
	}
}
