using ModelSF;
using PersistenceSF;
using RepositorySF.SegregatedInterfaces;
namespace RepositorySF {
	public interface IEmpresaRepository : IRead<Empresa>, ICreate<Empresa>, IUpdate<Empresa>, IRemove<Empresa> {

	}
	public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository {
		public EmpresaRepository(SFContext _ctx) {
			ctx = _ctx;
		}
	}
}
