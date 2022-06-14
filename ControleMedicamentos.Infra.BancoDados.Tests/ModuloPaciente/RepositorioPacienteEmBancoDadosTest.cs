using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        private Paciente _paciente;
        private RepositorioPacienteEmBancoDados _repositorio;

        public RepositorioPacienteEmBancoDadosTest()
        {
            _paciente = new Paciente("Tatiane Mossi", "1234567890");
            _repositorio = new RepositorioPacienteEmBancoDados();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_novo_paciente()
        {
            //action
            _repositorio.Inserir(_paciente);

            //assert
            var pacienteEncontrado = _repositorio.SelecionarPorId(_paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(_paciente, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_paciente()
        {
            //arrange
            _repositorio.Inserir(_paciente);

            //action
            _paciente.Nome = "João de Morais";
            _paciente.CartaoSUS = "0987654321";
            _repositorio.Editar(_paciente);

            //assert
            var pacienteEncontrado = _repositorio.SelecionarPorId(_paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(_paciente, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            //arrange
            _repositorio.Inserir(_paciente);

            //action
            _repositorio.Excluir(_paciente);

            //assert
            var pacienteEncontrado = _repositorio.SelecionarPorId(_paciente.Id);

            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_paciente()
        {
            //arrange
            _repositorio.Inserir(_paciente);

            //action
            var pacienteEncontrado = _repositorio.SelecionarPorId(_paciente.Id);

            //assert
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(_paciente, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_paciente()
        {
            //arrange
            var paciente1 = new Paciente("Thiago Souza", "12345673234");
            var paciente2 = new Paciente("Rosimeri Morais", "12345673234");
            var paciente3 = new Paciente("Ademir Jacó Mossi", "12345673234");

            var repositorio = new RepositorioPacienteEmBancoDados();

            repositorio.Inserir(paciente1);
            repositorio.Inserir(paciente2);
            repositorio.Inserir(paciente3);

            //action
            var pacientes = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, pacientes.Count);
            Assert.AreEqual(paciente1, pacientes[0]);
            Assert.AreEqual(paciente2, pacientes[1]);
            Assert.AreEqual(paciente3, pacientes[2]);
        }
    }
}
