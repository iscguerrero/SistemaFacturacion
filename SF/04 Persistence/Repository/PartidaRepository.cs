using ModelSF;
using PersistenceSF;
using RepositorySF.SegregatedInterfaces;
namespace RepositorySF {
	public interface IPartidaRepository : IRead<Partida>, ICreate<Partida>, IUpdate<Partida>, IRemove<Partida> {

	}
	public class PartidaRepository : Repository<Partida>, IPartidaRepository {
		public PartidaRepository(SFContext _ctx) {
			ctx = _ctx;
		}
	}
}
