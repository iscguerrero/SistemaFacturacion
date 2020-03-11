using FluentValidation;
using ModelSF;
using System.Configuration;
namespace ServicesSF.Validators {
	public class FacturaValidator : AbstractValidator<Factura> {
		private string RFC_VALIDATOR = ConfigurationManager.AppSettings["RFC_VALIDATOR"].ToString();
		private int MINIMAL_LENGTH_RFC = int.Parse(ConfigurationManager.AppSettings["MINIMAL_LENGTH_RFC"]);
		private int MAX_LENGTH_RFC = int.Parse(ConfigurationManager.AppSettings["MAX_LENGTH_RFC"]);

		public FacturaValidator() {
			RuleFor(x => x.RFCCliente)
				.NotNull().NotEmpty().WithMessage("Debes proporcionar el RFC del cliente")
				.MinimumLength(MINIMAL_LENGTH_RFC).MaximumLength(MAX_LENGTH_RFC).WithMessage("La longitud del RFC no es correcta")
				.Matches(RFC_VALIDATOR).WithMessage("El RFC no cumple con el estándar");

			RuleFor(x => x.RFCEmpresa)
				.NotNull().NotEmpty().WithMessage("Debes proporcionar el RFC del cliente")
				.MinimumLength(MINIMAL_LENGTH_RFC).MaximumLength(MAX_LENGTH_RFC).WithMessage("La longitud del RFC no es correcta")
				.Matches(RFC_VALIDATOR).WithMessage("El RFC no cumple con el estándar");

		}
	}
}
