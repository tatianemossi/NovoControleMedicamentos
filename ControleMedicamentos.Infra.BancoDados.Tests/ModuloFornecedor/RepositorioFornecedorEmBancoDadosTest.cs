using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        private Fornecedor _fornecedor;
        private RepositorioFornecedorEmBancoDados _repositorio;

        public RepositorioFornecedorEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
            _fornecedor = new Fornecedor("Tatiane Mossi", "991846942", "tati@email.com", "Lages", "SC");
            _repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_fornecedor()
        {
            //action
            _repositorio.Inserir(_fornecedor);

            //assert
            var fornecedorEncontrado = _repositorio.SelecionarPorId(_fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(_fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_fornecedor()
        {
            //arrange
            _repositorio.Inserir(_fornecedor);

            //action
            _fornecedor.Nome = "Thiago Souza";
            _fornecedor.Telefone = "999537888";
            _fornecedor.Email = "thiago@email.com";
            _fornecedor.Cidade = "Foz do Iguaçu";
            _fornecedor.Estado = "PR";
            _repositorio.Editar(_fornecedor);

            //assert
            var fornecedorEncontrado = _repositorio.SelecionarPorId(_fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(_fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            //arrange
            _repositorio.Inserir(_fornecedor);

            //action
            _repositorio.Excluir(_fornecedor);

            //assert
            var fornecedorEncontrado = _repositorio.SelecionarPorId(_fornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_fornecedor()
        {
            //arrange
            _repositorio.Inserir(_fornecedor);

            //action
            var fornecedorEncontrado = _repositorio.SelecionarPorId(_fornecedor.Id);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(_fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            //arrange
            var fornecedor1 = new Fornecedor("Thiago Souza", "999537888", "th@email.com", "Lages", "SC");
            var fornecedor2 = new Fornecedor("Rosimeri Morais", "991589905", "meri@email.com", "São Joaquim", "SC");
            var fornecedor3 = new Fornecedor("Ademir Jacó Mossi", "991942101", "mila@email.com", "São Joaquim", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDados();

            repositorio.Inserir(fornecedor1);
            repositorio.Inserir(fornecedor2);
            repositorio.Inserir(fornecedor3);

            //action
            var fornecedores = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, fornecedores.Count);
            Assert.AreEqual(fornecedor1, fornecedores[0]);
            Assert.AreEqual(fornecedor2, fornecedores[1]);
            Assert.AreEqual(fornecedor3, fornecedores[2]);
        }
    }
}
