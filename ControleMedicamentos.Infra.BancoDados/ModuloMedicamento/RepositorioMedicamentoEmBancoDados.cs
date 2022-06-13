using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : IRepositorioMedicamento
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
             @"INSERT INTO TBMEDICAMENTO
             (
                    NOME,
                    DESCRICAO,
                    LOTE,
                    VALIDADE,
                    QUANTIDADEDISPONIVEL,
                    FORNECEDOR_ID
             )
            VALUES
            (
                    @NOME,
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID
            );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE TBMEDICAMENTO
		        SET
			        NOME = @NOME,
                    DESCRICAO = @DESCRICAO,
                    LOTE = @LOTE,
                    VALIDADE = @VALIDADE,
                    QUANTIDADEDISPONIVEL = @QUANTIDADEDISPONIVEL,
                    FORNECEDOR_ID = @FORNECEDOR_ID
		        WHERE
			        ID = @ID";

        private const string sqlExcluir =
            @"DELETE FROM TBMEDICAMENTO
		        WHERE
			        ID = @ID";

        private const string sqlSelecionarPorId =
            @"SELECT 
		            M.ID, 
		            M.NOME,
                    M.DESCRICAO,
                    M.LOTE,
                    M.VALIDADE,
                    M.QUANTIDADEDISPONIVEL,
                    F.ID AS FORNECEDOR_ID,
                    F.NOME AS FORNECEDOR_NOME
	            FROM 
		            TBMEDICAMENTO M 
                INNER JOIN 
                    TBFORNECEDOR F ON M.FORNECEDOR_ID = F.ID
                WHERE
                    M.ID = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
		            M.ID, 
		            M.NOME,
                    M.DESCRICAO,
                    M.LOTE,
                    M.VALIDADE,
                    M.QUANTIDADEDISPONIVEL,
                    F.ID AS FORNECEDOR_ID,
                    F.NOME AS FORNECEDOR_NOME
	            FROM 
		            TBMEDICAMENTO M 
                INNER JOIN
                    TBFORNECEDOR F ON M.FORNECEDOR_ID = F.ID";

        #endregion

        public ValidationResult Inserir(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            medicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return new ValidationResult();
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            comandoEdicao.Parameters.AddWithValue("ID", medicamento.Id);

            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Medicamento SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;
            if (leitorMedicamento.Read())
                medicamento = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamento;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                var medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;
        }

        private static Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            var id = Convert.ToInt32(leitorMedicamento["ID"]);
            var nome = Convert.ToString(leitorMedicamento["NOME"]);
            var descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            var lote = Convert.ToString(leitorMedicamento["LOTE"]);
            var validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            var quantidadeDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);

            var idFornecedor = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);
            var nomeFornecedor = Convert.ToString(leitorMedicamento["FORNECEDOR_NOME"]);

            var medicamento = new Medicamento
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = quantidadeDisponivel,
                IdFornecedor = idFornecedor,

                Fornecedor = new Fornecedor
                {
                    Id = idFornecedor,
                    Nome = nomeFornecedor
                }

            };

            return medicamento;
        }

        private void ConfigurarParametrosMedicamento(Medicamento medicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", medicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", medicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", medicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", medicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", medicamento.IdFornecedor);
        }
    }
}
