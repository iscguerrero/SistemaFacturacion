namespace RepositorySF.SegregatedInterfaces {
	public interface IUpdate<T> where T : class {
		T NoTrackingUpdate(T updated, string key);
		bool TrackingUpdate(T t);
	}
}