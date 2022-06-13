using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();

            RuleFor(x => x.CartaoSUS).NotNull().NotEmpty();
        }
    }
}
