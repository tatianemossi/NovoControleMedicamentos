using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Medicamento _medicamento;
        private Fornecedor _fornecedor;
        private RepositorioMedicamentoEmBancoDados _repositorioMedicamento;
        private RepositorioFornecedorEmBancoDados _repositorioFornecedor;

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            _fornecedor = new Fornecedor("Tatiane Mossi", "991846942", "tati@email.com", "Lages", "SC");
            _medicamento = new Medicamento("Dipirona", "Analgésico", "123", new DateTime(2025, 05, 05), 76);
            _repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            _repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_novo_medicamento()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            //action
            _repositorioMedicamento.Inserir(_medicamento);

            //assert
            var medicamentoEncontrado = _repositorioMedicamento.SelecionarPorId(_medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(_medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_medicamento()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;
            _repositorioMedicamento.Inserir(_medicamento);

            //action
            _medicamento.Nome = "Neosaodina";
            _medicamento.Descricao = "Analgésico";
            _medicamento.Lote = "13579";
            _medicamento.Validade = new DateTime(2024, 07, 07);
            _medicamento.QuantidadeDisponivel = 45;
            _medicamento.IdFornecedor = _fornecedor.Id;
            _repositorioMedicamento.Editar(_medicamento);

            //assert
            var medicamentoEncontrado = _repositorioMedicamento.SelecionarPorId(_medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(_medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            //arrange
            _repositorioMedicamento.Inserir(_medicamento);

            //action
            _repositorioMedicamento.Excluir(_medicamento);

            //assert
            var medicamentoEncontrado = _repositorioMedicamento.SelecionarPorId(_medicamento.Id);

            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_medicamento()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;
            _repositorioMedicamento.Inserir(_medicamento);

            //action
            var medicamentoEncontrado = _repositorioMedicamento.SelecionarPorId(_medicamento.Id);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(_medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);

            var medicamento1 = new Medicamento("Miorelax", "Relaxante Muscular", "234", new DateTime(2026, 06, 06), 61);
            medicamento1.IdFornecedor = _fornecedor.Id;

            var medicamento2 = new Medicamento("Cataflan", "Analgésico", "456", new DateTime(2027, 07, 07), 72);
            medicamento2.IdFornecedor = _fornecedor.Id;

            var medicamento3 = new Medicamento("Loratadina", "Antialérgico", "678", new DateTime(2028, 08, 08), 83);
            medicamento3.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(medicamento1);
            _repositorioMedicamento.Inserir(medicamento2);
            _repositorioMedicamento.Inserir(medicamento3);

            //action
            var medicamentos = _repositorioMedicamento.SelecionarTodos();

            //assert
            Assert.AreEqual(3, medicamentos.Count);
            Assert.AreEqual(medicamento1, medicamentos[0]);
            Assert.AreEqual(medicamento2, medicamentos[1]);
            Assert.AreEqual(medicamento3, medicamentos[2]);
        }
    }
}
