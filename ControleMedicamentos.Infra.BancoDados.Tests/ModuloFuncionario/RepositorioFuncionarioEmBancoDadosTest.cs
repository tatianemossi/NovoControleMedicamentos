using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        private Funcionario _funcionario;
        private RepositorioFuncionarioEmBancoDados _repositorio;

        public RepositorioFuncionarioEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
            _funcionario = new Funcionario("Tatiane Mossi", "tatimossi", "12345");
            _repositorio = new RepositorioFuncionarioEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_funcionario()
        {
            //action
            _repositorio.Inserir(_funcionario);

            //assert
            var funcionarioEncontrado = _repositorio.SelecionarPorId(_funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(_funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_funcionario()
        {
            //arrange
            _repositorio.Inserir(_funcionario);

            //action
            _funcionario.Nome = "Thiago Souza";
            _funcionario.Login = "thiagosouza";
            _funcionario.Senha = "13579";
            _repositorio.Editar(_funcionario);

            //assert
            var funcionarioEncontrado = _repositorio.SelecionarPorId(_funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(_funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            //arrange
            _repositorio.Inserir(_funcionario);

            //action
            _repositorio.Excluir(_funcionario);

            //assert
            var funcionarioEncontrado = _repositorio.SelecionarPorId(_funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_funcionario()
        {
            //arrange
            _repositorio.Inserir(_funcionario);

            //action
            var funcionarioEncontrado = _repositorio.SelecionarPorId(_funcionario.Id);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(_funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {
            //arrange
            var funcionario1 = new Funcionario("Thiago Souza", "thiagosouza", "12345");
            var funcionario2 = new Funcionario("Rosimeri Morais", "merimorais", "13579");
            var funcionario3 = new Funcionario("Ademir Jacó Mossi", "milamossi", "24680");

            var repositorio = new RepositorioFuncionarioEmBancoDados();

            repositorio.Inserir(funcionario1);
            repositorio.Inserir(funcionario2);
            repositorio.Inserir(funcionario3);

            //action
            var funcionarios = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, funcionarios.Count);
            Assert.AreEqual(funcionario1, funcionarios[0]);
            Assert.AreEqual(funcionario2, funcionarios[1]);
            Assert.AreEqual(funcionario3, funcionarios[2]);
        }
    }
}
