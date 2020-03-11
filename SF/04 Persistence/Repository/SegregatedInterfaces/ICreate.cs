using System.Collections.Generic;
namespace RepositorySF.SegregatedInterfaces {
	public interface ICreate<T> where T : class {
		T Add(T t);
		IEnumerable<T> AddRange(IEnumerable<T> tList);
	}
}