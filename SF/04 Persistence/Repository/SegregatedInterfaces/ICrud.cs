using System.Collections.Generic;
using System.Linq;
namespace RepositorySF.SegregatedInterfaces {
	public interface ICrud<T> where T: class {
		IQueryable<T> Set();
		T Add(T t);
		IEnumerable<T> AddRange(IEnumerable<T> tList);
		T NoTrackingUpdate(T updated, string key);
		bool TrackingUpdate(T t);
		T SoftDelete(string Codigo);
		T CompositePKSoftDelete(params object[] keys);
		bool Delete(T t);
		bool DeleteRange(List<T> tList);
	}
}
