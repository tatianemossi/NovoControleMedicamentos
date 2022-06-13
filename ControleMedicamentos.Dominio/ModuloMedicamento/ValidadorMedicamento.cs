using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();

            RuleFor(x => x.Descricao).NotNull().NotEmpty();

            RuleFor(x => x.QuantidadeDisponivel).NotNull().NotEmpty();

            RuleFor(x => x.Validade).NotNull().NotEmpty();

            RuleFor(x => x.IdFornecedor).NotNull().NotEmpty();

            RuleFor(x => x.Lote).NotNull().NotEmpty();
        }
    }
}
