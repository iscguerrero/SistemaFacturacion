using ModelSF;
using PersistenceSF;
using ServicesSF;
using System;
using UoWSF;

namespace ConsoleAppSF {
	class Program {
		static void Main(string[] args) {
			using (var ctx = new SFContext()) {

				IUoW uow = new UoWContainer(ctx);
				IProductoService productoService = new ProductoService(uow);

				var producto = new Producto{
					Codigo = "SF002",
					Descripcion = "Producto número Dos",
					GravaIVA = true,
					TasaIVA = 16,
					Costo = 70,
					Precio = 108.5f
				};

				var response = productoService.Add(producto);

				uow.SaveChanges();
			}
		}
	}
}
