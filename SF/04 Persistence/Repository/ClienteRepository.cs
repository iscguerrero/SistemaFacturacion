using ModelSF;
using PersistenceSF;
using RepositorySF.SegregatedInterfaces;
namespace RepositorySF {
	public interface IClienteRepository : IRead<Cliente>, ICreate<Cliente>, IUpdate<Cliente>, IRemove<Cliente> {

	}
	public class ClienteRepository : Repository<Cliente>, IClienteRepository {
		public ClienteRepository(SFContext _ctx) {
			ctx = _ctx;
		}
	}
}
