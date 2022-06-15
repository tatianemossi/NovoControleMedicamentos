using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.IdMedicamento).NotEmpty();

            RuleFor(x => x.QtdMedicamento).NotEmpty();

            RuleFor(x => x.IdPaciente).NotEmpty();

            RuleFor(x => x.IdFuncionario).NotEmpty();

            RuleFor(x => x.Data).NotEmpty();
        }
    }
}
