using ModelSF;
using ServicesSF.Validators;
using System.Collections.Generic;
using System.Linq;
using UoWSF;
namespace ServicesSF {
	public interface IProductoService {
		IQueryable<Producto> List();
		Response<Producto> Add(Producto t);
	}
	public class ProductoService : IProductoService {
		private readonly IUoW _uow;
		private Response<Producto> response;
		private List<string> errors;

		public ProductoService(IUoW unitOfWork) {
			_uow = unitOfWork;
			response = new Response<Producto>();
			errors = new List<string>();
		}

		public IQueryable<Producto> List() {
			return _uow.Repository.Producto.Set();
		}

		public Response<Producto> Add(Producto t) {
			// Ejecutar el validator
			var validator = new ProductoValidator();
			var results = validator.Validate(t);

			if (!results.IsValid) {
				foreach (var failure in results.Errors) {
					errors.Add("Falló " + failure.PropertyName + ". Error: " + failure.ErrorMessage);
				}
				response.ErrorMessages = errors;
			} else {
				var existing = _uow.Repository.Producto.Set().Where(p => p.Codigo == t.Codigo).SingleOrDefault();
				if (existing != null) {
					errors.Add("El código ingresado ya se encuentra registrado en el sistema");
					response.ErrorMessages = errors;
				}
				else {
					response.item = _uow.Repository.Producto.Add(t);
					response.status = true;
				}
			}
			return response;
		}

	}
}