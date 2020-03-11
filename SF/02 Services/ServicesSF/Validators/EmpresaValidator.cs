using FluentValidation;
using ModelSF;
using System.Configuration;
namespace ServicesSF.Validators {
	public class EmpresaValidator : AbstractValidator<Empresa> {
		protected string RFC_VALIDATOR = ConfigurationManager.AppSettings["RFC_VALIDATOR"].ToString();
		public EmpresaValidator() {
			RuleFor(x => x.RFC)
				.NotNull().WithMessage("El RFC de la empresa no puede ser nulo")
				.MaximumLength(13).WithMessage("Longitud de cadena excedida en el RFC de la empresa")
				.Matches(RFC_VALIDATOR).WithMessage("El rfc de la empresa no cumple con los estándares establecidos");

			RuleFor(x => x.Nombre)
				.NotNull().NotEmpty().WithMessage("El nomnbre de la empresa no puede estar vacío");
		}
	}
}
