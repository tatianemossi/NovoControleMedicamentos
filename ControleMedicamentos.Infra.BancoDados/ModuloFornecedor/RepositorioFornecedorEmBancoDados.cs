﻿using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados : IRepositorioFornecedor
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
            @"INSERT INTO TBFORNECEDOR
            (
                    NOME,
                    TELEFONE,
                    EMAIL,
                    CIDADE,
                    ESTADO
            )
            VALUES
            (
                    @NOME,
                    @TELEFONE,
                    @EMAIL,
                    @CIDADE,
                    @ESTADO
            );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE TBFORNECEDOR
		        SET
			        NOME = @NOME,
                    TELEFONE = @TELEFONE,
                    EMAIL = @EMAIL,
                    CIDADE = @CIDADE,
                    ESTADO = @ESTADO
		        WHERE
			        ID = @ID";

        private const string sqlExcluir =
            @"DELETE FROM TBFORNECEDOR
		        WHERE
			        ID = @ID";

        private const string sqlSelecionarPorId =
            @"SELECT 
		            ID, 
		            NOME,
                    TELEFONE,
                    EMAIL,
                    CIDADE,
                    ESTADO
	            FROM 
		            TBFORNECEDOR
		        WHERE
                    ID = @ID";

        private const string sqlSelecionarTodos =
             @"SELECT 
		            ID, 
		            NOME,
                    TELEFONE,
                    EMAIL,
                    CIDADE,
                    ESTADO
	            FROM 
		            TBFORNECEDOR";

        #endregion

        public ValidationResult Inserir(Fornecedor novoFornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(novoFornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFornecedor(novoFornecedor, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoFornecedor.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Fornecedor fornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(fornecedor, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Fornecedor SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;
            if (leitorFornecedor.Read())
                fornecedor = ConverterParaFornecedor(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaFornecedor(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;
        }

        private static Fornecedor ConverterParaFornecedor(SqlDataReader leitorFornecedor)
        {

            int id = Convert.ToInt32(leitorFornecedor["ID"]);
            string nome = Convert.ToString(leitorFornecedor["NOME"]);
            string telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            string email = Convert.ToString(leitorFornecedor["EMAIL"]);
            string cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            string estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            var fornecedor = new Fornecedor()
            {
                Id = id,
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Cidade = cidade,
                Estado = estado
            };

            return fornecedor;
        }

        private static void ConfigurarParametrosFornecedor(Fornecedor novoFornecedor, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoFornecedor.Id);
            comando.Parameters.AddWithValue("NOME", novoFornecedor.Nome);
            comando.Parameters.AddWithValue("TELEFONE", novoFornecedor.Telefone);
            comando.Parameters.AddWithValue("EMAIL", novoFornecedor.Email);
            comando.Parameters.AddWithValue("CIDADE", novoFornecedor.Cidade);
            comando.Parameters.AddWithValue("ESTADO", novoFornecedor.Estado);
        }
    }
}
