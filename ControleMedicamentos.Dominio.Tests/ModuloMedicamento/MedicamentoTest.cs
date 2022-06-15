using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        public MedicamentoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
            Assert.AreEqual("'Nome' deve ser informado.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'Descricao' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
            Assert.AreEqual("'Descricao' deve ser informado.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void QuantidadeDisponivel_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'Quantidade Disponivel' não pode ser nula ou vazia.", resultado.Errors[4].ErrorMessage);
        }

        [TestMethod]
        public void Validade_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'Validade' não pode ser nula ou vazia.", resultado.Errors[5].ErrorMessage);
        }

        [TestMethod]
        public void IdFornecedor_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'IdFornecedor' não pode ser nulo ou vazio.", resultado.Errors[6].ErrorMessage);
        }

        [TestMethod]
        public void Lote_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.AreEqual("'Lote' não pode ser nulo.", resultado.Errors[7].ErrorMessage);
            Assert.AreEqual("'Lote' deve ser informado.", resultado.Errors[8].ErrorMessage);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_medicamento_estiver_valido()
        {
            //arrange
            var medicamento = new Medicamento();

            var validador = new ValidadorMedicamento();

            medicamento.Nome = "Dipirona";
            medicamento.Descricao = "analgésico";
            medicamento.QuantidadeDisponivel = 178;
            medicamento.Validade = new DateTime(2024,07,01);
            medicamento.IdFornecedor = 1;
            medicamento.Lote = "154-t";

            //action
            var resultado = validador.Validate(medicamento);

            //assert
            Assert.IsTrue(resultado.IsValid);
        }
    }
}
