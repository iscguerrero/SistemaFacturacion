using FluentValidation;
using ModelSF;
using System.Configuration;
namespace ServicesSF.Validators {
	class ProductoValidator : AbstractValidator<Producto> {
		// Hay que agregar los ensamblados System.Configuration y System.Messaging
		protected float MAX_COST = float.Parse(ConfigurationManager.AppSettings["MAX_COST"]);
		protected float MINIMAL_MONEY_MAKING_PERCENT = float.Parse(ConfigurationManager.AppSettings["MINIMAL_MONEY_MAKING_PERCENT"]);
		protected float IVA_PERCENT = float.Parse(ConfigurationManager.AppSettings["IVA_PERCENT"]);

		public ProductoValidator() {
			RuleFor(x => x.Codigo).MaximumLength(15).WithMessage("La longitud máxima del código de barras es de 15 caracteres");
			RuleFor(x => x.Descripcion).MaximumLength(50).WithMessage("La longitud máxima de la descripción del producto debe ser de 50 caracteres");
			RuleFor(x => x.Costo).NotNull().NotEqual(0).LessThanOrEqualTo(MAX_COST).WithMessage("El costo del producto excede los límites establecidos");

			RuleFor(x => x.Precio).NotNull().GreaterThanOrEqualTo(x => x.Costo + x.Costo * MINIMAL_MONEY_MAKING_PERCENT).WithMessage("Debes seguir las reglas para establecer el precio de los productos");

			RuleFor(x => x.TasaIVA).NotNull().LessThanOrEqualTo(IVA_PERCENT).WithMessage("La tasa mínima de impuestos gravables es del 16%");
		}

	}
}
