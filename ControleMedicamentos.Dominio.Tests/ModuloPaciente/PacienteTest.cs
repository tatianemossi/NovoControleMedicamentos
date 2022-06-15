using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class PacienteTest
    {
        public PacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var paciente = new Paciente();

            var validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(paciente);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
            Assert.AreEqual("'Nome' deve ser informado.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var paciente = new Paciente();

            var validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartao SUS' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
            Assert.AreEqual("'Cartao SUS' deve ser informado.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_do_paciente_deve_ter_no_mínimo_10_caracteres()
        {
            //arrange
            var paciente = new Paciente();

            var validador = new ValidadorPaciente();

            paciente.CartaoSUS = "12345";

            //action
            var resultado = validador.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartao SUS' deve ter no mínimo 10 caracteres.", resultado.Errors[2].ErrorMessage);
        }
    }
}