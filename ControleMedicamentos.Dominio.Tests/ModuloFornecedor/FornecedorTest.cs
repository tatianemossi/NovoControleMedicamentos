using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {
        public FornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
            Assert.AreEqual("'Nome' deve ser informado.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Email_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Email' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
            Assert.AreEqual("'Email' deve ser informado.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Telefone' não pode ser nulo.", resultado.Errors[4].ErrorMessage);
            Assert.AreEqual("'Telefone' deve ser informado.", resultado.Errors[5].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Cidade' não pode ser nulo.", resultado.Errors[6].ErrorMessage);
            Assert.AreEqual("'Cidade' deve ser informado.", resultado.Errors[7].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Estado' não pode ser nulo.", resultado.Errors[8].ErrorMessage);
            Assert.AreEqual("'Estado' deve ser informado.", resultado.Errors[9].ErrorMessage);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_fornecedor_estiver_valido()
        {
            //arrange
            var fornecedor = new Fornecedor();

            var validador = new ValidadorFornecedor();

            fornecedor.Nome = "Tatiane Mossi";
            fornecedor.Email = "tati@email.com";
            fornecedor.Telefone = "991846942";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";

            //action
            var resultado = validador.Validate(fornecedor);

            //assert
            Assert.IsTrue(resultado.IsValid);
        }
    }
}
