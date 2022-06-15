using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();

            RuleFor(x => x.Descricao).NotNull().NotEmpty();

            RuleFor(x => x.QuantidadeDisponivel).NotNull().NotEmpty()
                .WithMessage("'Quantidade Disponivel' não pode ser nula ou vazia.");

            RuleFor(x => x.Validade).NotNull().NotEmpty()
                .WithMessage("'Validade' não pode ser nula ou vazia.");

            RuleFor(x => x.IdFornecedor).NotNull().NotEmpty()
                .WithMessage("'IdFornecedor' não pode ser nulo ou vazio.");

            RuleFor(x => x.Lote).NotNull().NotEmpty();
        }
    }
}
