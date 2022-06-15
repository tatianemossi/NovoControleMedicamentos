using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class RequisicaoTest
    {
        public RequisicaoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void IdMedicamento_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.AreEqual("'Id Medicamento' deve ser informado.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_do_medicamento_deve_ser_obrigatoria()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.AreEqual("'Qtd Medicamento' deve ser informado.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void IdPaciente_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.AreEqual("'Id Paciente' deve ser informado.", resultado.Errors[2].ErrorMessage);
        }

        [TestMethod]
        public void IdFuncionario_da_requisicao_deve_ser_obrigatorio()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.AreEqual("'Id Funcionario' deve ser informado.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Data_da_requisicao_deve_ser_obrigatoria()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.AreEqual("'Data' deve ser informado.", resultado.Errors[4].ErrorMessage);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_medicamento_estiver_valido()
        {
            //arrange
            var requisicao = new Requisicao();

            var validador = new ValidadorRequisicao();

            requisicao.IdMedicamento = 1;
            requisicao.QtdMedicamento = 756;
            requisicao.IdPaciente = 1;
            requisicao.IdFuncionario = 1;
            requisicao.Data = new DateTime(2022, 06, 15);

            //action
            var resultado = validador.Validate(requisicao);

            //assert
            Assert.IsTrue(resultado.IsValid);
        }
    }
}
