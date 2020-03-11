using System.Collections.Generic;
namespace RepositorySF.SegregatedInterfaces {
	public interface IRemove<T> where T : class {
		T SoftDelete(string Codigo);
		T CompositePKSoftDelete(params object[] keys);
		bool Delete(T t);
		bool DeleteRange(List<T> tList);
	}
}