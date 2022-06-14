using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Requisicao _requisicao;
        private RepositorioRequisicaoEmBancoDados _repositorioRequisicao;

        private Medicamento _medicamento;
        private RepositorioMedicamentoEmBancoDados _repositorioMedicamento;

        private Paciente _paciente;
        private RepositorioPacienteEmBancoDados _repositorioPaciente;

        private Funcionario _funcionario;
        private RepositorioFuncionarioEmBancoDados _repositorioFuncionario;

        private Fornecedor _fornecedor;
        private RepositorioFornecedorEmBancoDados _repositorioFornecedor;

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            _paciente = new Paciente("Tatiane Mossi", "12345");
            _repositorioPaciente = new RepositorioPacienteEmBancoDados();

            _funcionario = new Funcionario("Thiago Souza", "thisouza", "12355");
            _repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();

            _fornecedor = new Fornecedor("Mariana", "991171718", "nana@email.com", "SJ", "SC");
            _repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            _medicamento = new Medicamento("Dipirona", "Analgésico", "123", new DateTime(2025, 05, 05), 76);
            _repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            _requisicao = new Requisicao(_medicamento, _paciente, _medicamento.QuantidadeDisponivel, new DateTime(2025, 05, 05), _funcionario);
            _repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
        }

        [TestMethod]
        public void Deve_inserir_nova_requisicao()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(_medicamento);
            _requisicao.IdMedicamento = _medicamento.Id;

            _repositorioPaciente.Inserir(_paciente);
            _requisicao.IdPaciente = _paciente.Id;  

            _repositorioFuncionario.Inserir(_funcionario);
            _requisicao.IdFuncionario = _funcionario.Id;

            //action
            _repositorioRequisicao.Inserir(_requisicao);

            //assert
            var requisicaoEncontrada = _repositorioRequisicao.SelecionarPorId(_requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(_requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_editar_informacoes_requisicao()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(_medicamento);
            _requisicao.IdMedicamento = _medicamento.Id;

            _repositorioPaciente.Inserir(_paciente);
            _requisicao.IdPaciente = _paciente.Id;

            _repositorioFuncionario.Inserir(_funcionario);
            _requisicao.IdFuncionario = _funcionario.Id;

            _repositorioRequisicao.Inserir(_requisicao);

            var paciente2 = new Paciente("Godofredo", "67890");
            _repositorioPaciente.Inserir(paciente2);

            //action
            _requisicao.Data = new DateTime(2022,06,15);
            _requisicao.QtdMedicamento = 65;
            _requisicao.IdPaciente = paciente2.Id;
            _repositorioRequisicao.Editar(_requisicao);

            //assert
            var requisicaoEncontrada = _repositorioRequisicao.SelecionarPorId(_requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(_requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(_medicamento);
            _requisicao.IdMedicamento = _medicamento.Id;

            _repositorioPaciente.Inserir(_paciente);
            _requisicao.IdPaciente = _paciente.Id;

            _repositorioFuncionario.Inserir(_funcionario);
            _requisicao.IdFuncionario = _funcionario.Id;

            _repositorioRequisicao.Inserir(_requisicao);

            //action
            _repositorioRequisicao.Excluir(_requisicao);

            //assert
            var requisicaoEncontrada = _repositorioRequisicao.SelecionarPorId(_requisicao.Id);

            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_uma_requisicao()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(_medicamento);
            _requisicao.IdMedicamento = _medicamento.Id;

            _repositorioPaciente.Inserir(_paciente);
            _requisicao.IdPaciente = _paciente.Id;

            _repositorioFuncionario.Inserir(_funcionario);
            _requisicao.IdFuncionario = _funcionario.Id;

            _repositorioRequisicao.Inserir(_requisicao);

            //action
            var requisicaoEncontrada = _repositorioRequisicao.SelecionarPorId(_requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(_requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {
            //arrange
            _repositorioFornecedor.Inserir(_fornecedor);
            _medicamento.IdFornecedor = _fornecedor.Id;

            _repositorioMedicamento.Inserir(_medicamento);
            _requisicao.IdMedicamento = _medicamento.Id;

            _repositorioPaciente.Inserir(_paciente);
            _requisicao.IdPaciente = _paciente.Id;

            _repositorioFuncionario.Inserir(_funcionario);
            _requisicao.IdFuncionario = _funcionario.Id;

            var requisicao1 = new Requisicao(_medicamento, _paciente, _medicamento.QuantidadeDisponivel, new DateTime(2022,06,14), _funcionario);
            requisicao1.IdFuncionario = _funcionario.Id;
            requisicao1.IdPaciente = _paciente.Id;
            requisicao1.IdMedicamento = _medicamento.Id;

            var requisicao2 = new Requisicao(_medicamento, _paciente, _medicamento.QuantidadeDisponivel, new DateTime(2022, 06, 15), _funcionario);
            requisicao2.IdFuncionario = _funcionario.Id;
            requisicao2.IdPaciente = _paciente.Id;
            requisicao2.IdMedicamento = _medicamento.Id;

            var requisicao3 = new Requisicao(_medicamento, _paciente, _medicamento.QuantidadeDisponivel, new DateTime(2022, 06, 16), _funcionario);
            requisicao3.IdFuncionario = _funcionario.Id;
            requisicao3.IdPaciente = _paciente.Id;
            requisicao3.IdMedicamento = _medicamento.Id;

            _repositorioRequisicao.Inserir(requisicao1);
            _repositorioRequisicao.Inserir(requisicao2);
            _repositorioRequisicao.Inserir(requisicao3);

            //action
            var requisicoes = _repositorioRequisicao.SelecionarTodos();

            //assert
            Assert.AreEqual(3, requisicoes.Count);
            Assert.AreEqual(requisicao1, requisicoes[0]);
            Assert.AreEqual(requisicao2, requisicoes[1]);
            Assert.AreEqual(requisicao3, requisicoes[2]);
        }
    }
}
