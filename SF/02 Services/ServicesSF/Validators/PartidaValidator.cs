using FluentValidation;
using ModelSF;
using System.Configuration;
namespace ServicesSF.Validators {
	public class PartidaValidator : AbstractValidator<Partida> {
		protected float MINIMAL_PIECES = float.Parse(ConfigurationManager.AppSettings["MINIMAL_PIECES"]);

		public PartidaValidator() {
			RuleFor(x => x.NumerPartida).NotNull().WithMessage("Debes definir el número de partida");
			RuleFor(x => x.Precio).NotNull().GreaterThanOrEqualTo(x => x.Producto.Precio).WithMessage("Debes definir el número de partida");
			RuleFor(x => x.Piezas).NotNull().GreaterThanOrEqualTo(MINIMAL_PIECES).WithMessage(x => "El producto" + x.Producto.Descripcion + " no cumple con la cantidad mínima de piezas requeridas para la venta");
		}
	}
}