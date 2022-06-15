using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FuncionarioTest
    {
        public FuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
            Assert.AreEqual("'Nome' deve ser informado.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Login_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("'Login' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
            Assert.AreEqual("'Login' deve ser informado.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("'Senha' não pode ser nulo.", resultado.Errors[4].ErrorMessage);
            Assert.AreEqual("'Senha' deve ser informado.", resultado.Errors[5].ErrorMessage); 
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ter_no_minimo_5_caracteres()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            funcionario.Senha = "123";

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual("'Senha' deve ter no mínimo 5 caracteres.", resultado.Errors[4].ErrorMessage);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_senha_tiver_5_caracteres_ou_mais()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            funcionario.Senha = "12345";

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.AreEqual(4, resultado.Errors.Count);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_funcionario_estiver_valido()
        {
            //arrange
            var funcionario = new Funcionario();

            var validador = new ValidadorFuncionario();

            funcionario.Nome = "Tatiane Mossi";
            funcionario.Login = "tatimossi";
            funcionario.Senha = "12345";

            //action
            var resultado = validador.Validate(funcionario);

            //assert
            Assert.IsTrue(resultado.IsValid);
        }
    }
}
