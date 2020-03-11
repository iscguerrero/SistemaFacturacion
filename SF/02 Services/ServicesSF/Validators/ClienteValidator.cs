using FluentValidation;
using ModelSF;
using System.Configuration;
namespace ServicesSF.Validators {
	public class ClienteValidator : AbstractValidator<Cliente> {
		protected string RFC_VALIDATOR = ConfigurationManager.AppSettings["RFC_VALIDATOR"].ToString();
		private int MINIMAL_LENGTH_RFC = int.Parse(ConfigurationManager.AppSettings["MINIMAL_LENGTH_RFC"]);
		private int MAX_LENGTH_RFC = int.Parse(ConfigurationManager.AppSettings["MAX_LENGTH_RFC"]);

		public ClienteValidator() {
			RuleFor(x => x.RFC)
				.NotNull().NotEmpty().WithMessage("El RFC de la empresa no puede ser nulo ni vacío")
				.MinimumLength(MINIMAL_LENGTH_RFC).MaximumLength(MAX_LENGTH_RFC).WithMessage("Longitud de cadena excedida en el RFC de la empresa")
				.Matches(RFC_VALIDATOR).WithMessage("El RFC no comple con el estándar establecido");

			RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Debes proporcionar el nombre o razón social del cliente");
		}
	}
}
