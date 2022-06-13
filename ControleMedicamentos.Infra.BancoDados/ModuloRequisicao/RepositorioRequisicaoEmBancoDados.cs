using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : IRepositorioRequisicao
    {
        #region Endereço do Banco de Dados
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=controleMedicamentosDb;" +
               "Integrated Security=True;" +
               "Pooling=False";
        #endregion

        #region SQL Queries
        private const string sqlInserir =
            @"INSERT INTO TBREQUISICAO
             (
                    FUNCIONARIO_ID,
                    PACIENTE_ID,
                    MEDICAMENTO_ID,
                    QUANTIDADEMEDICAMENTO,
                    DATA
             )
            VALUES
            (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
            );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
          @"UPDATE TBREQUISICAO
		        SET
			        FUNCIONARIO_ID = @FUNCIONARIO_ID
                    PACIENTE_ID = @PACIENTE_ID
                    MEDICAMENTO_ID = @MEDICAMENTO_ID
                    QUANTIDADEMEDICAMENTO = @QUANTIDADEMEDICAMENTO
                    DATA = @DATA
		        WHERE
			        ID = @ID";

        private const string sqlExcluir =
            @"DELETE FROM TBREQUISICAO
		        WHERE
			        ID = @ID";

        private const string sqlSelecionarPorId =
            @"SELECT 
		            R.ID, 
		            R.QUANTIDADEMEDICAMENTO,
                    R.DATA,
                    FUNC.ID AS FUNCIONARIO_ID,
                    FUNC.NOME AS FUNCIONARIO_NOME,
                    P.ID AS PACIENTE_ID,
                    P.NOME AS PACIENTE_NOME,
                    M.ID AS MEDICAMENTO_ID,
                    M.NOME AS MEDICAMENTO_NOME
	            FROM 
		            TBREQUISICAO R 
                INNER JOIN
                    TBFUNCIONARIO FUNC ON
                    R.FUNCIONARIO_ID = FUNC.ID
                INNER JOIN
                    TBPACIENTE P ON
                    R.PACIENTE_ID = P.ID
                INNER JOIN
                    TBMEDICAMENTO M ON
                    R.MEDICAMENTO_ID = M.ID
                WHERE
                    R.ID = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
		            R.ID, 
		            R.QUANTIDADEMEDICAMENTO,
                    R.DATA,
                    FUNC.ID AS FUNCIONARIO_ID,
                    FUNC.NOME AS FUNCIONARIO_NOME,
                    P.ID AS PACIENTE_ID,
                    P.NOME AS PACIENTE_NOME,
                    M.ID AS MEDICAMENTO_ID,
                    M.NOME AS MEDICAMENTO_NOME
	            FROM 
		            TBREQUISICAO R 
                INNER JOIN
                    TBFUNCIONARIO FUNC ON
                    R.FUNCIONARIO_ID = FUNC.ID
                INNER JOIN
                    TBPACIENTE P ON
                    R.PACIENTE_ID = P.ID
                INNER JOIN
                    TBMEDICAMENTO M ON
                    R.MEDICAMENTO_ID = M.ID";

        #endregion

        public ValidationResult Inserir(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            requisicao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            comandoEdicao.Parameters.AddWithValue("ID", requisicao.Id);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao requisicao) 
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Requisicao SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;
            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                var requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }

        private static Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            var id = Convert.ToInt32(leitorRequisicao["ID"]);
            var data = Convert.ToDateTime(leitorRequisicao["DATA"]);
            var quantidadeMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);

            var idFuncionario = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            var nomeFuncionario = Convert.ToString(leitorRequisicao["FUNCIONARIO_NOME"]);

            var idPaciente = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            var nomePaciente = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);

            var idMedicamento = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            var nomeMedicamento = Convert.ToString(leitorRequisicao["MEDICAMENTO_NOME"]);

            var requisicao = new Requisicao
            {
                Id = id,
                Data = data,
                QtdMedicamento = quantidadeMedicamento,

                Funcionario = new Funcionario
                {
                    Id = idFuncionario,
                    Nome = nomeFuncionario
                },

                Paciente = new Paciente
                {
                    Id = idPaciente,
                    Nome = nomePaciente
                },

                Medicamento = new Medicamento
                {
                    Id = idMedicamento,
                    Nome = nomeMedicamento
                }
            };

            return requisicao;
        }

        private void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("DATA", requisicao.Data);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
        }
    }
}
