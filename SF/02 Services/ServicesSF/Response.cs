using System.Collections.Generic;
namespace ServicesSF {
	public class Response<T> where T : class {
		public bool status { get; set; } // False en caso de error / True en caso de éxito
		public List<string> ErrorMessages { get; set; } // Contiene la lista de validaciones
		public T item { get; set; } // Contener T
		public List<T> list { get; set; } // contener una lista T
	}
}
