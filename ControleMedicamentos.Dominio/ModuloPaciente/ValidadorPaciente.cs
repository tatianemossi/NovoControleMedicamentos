using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();

            RuleFor(x => x.CartaoSUS).NotNull().NotEmpty();

            RuleFor(x => x.CartaoSUS).MinimumLength(5)
                .WithMessage("'Cartao SUS' deve ter no mínimo 5 caracteres.");
        }
    }
}
