using System.Linq;
namespace RepositorySF.SegregatedInterfaces {
	public interface IRead<T> where T: class {
		IQueryable<T> Set();
	}
}