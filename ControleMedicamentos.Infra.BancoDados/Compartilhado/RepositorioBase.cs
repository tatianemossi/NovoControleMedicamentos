using ControleMedicamentos.Dominio.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public abstract class RepositorioBase<T, TValidador, TMapeador> 
        where T : EntidadeBase<T>
        where TValidador : AbstractValidator<T>, new()
        where TMapeador : MapeadorBase<T>, new ()
    {
        #region Endereço do Banco de Dados
        protected const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=controleMedicamentosDb;" +
               "Integrated Security=True;" +
               "Pooling=False";
        #endregion

        protected abstract string sqlInserir { get; }

        protected abstract string sqlEditar { get; }

        protected abstract string sqlExcluir { get; }

        protected abstract string sqlSelecionarPorId { get; }

        protected abstract string sqlSelecionarTodos { get; }

        public ValidationResult Inserir(T registro)
        {
            var validador = new TValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            var mapeador = new TMapeador();
            mapeador.ConfigurarParametros(registro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            registro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(T registro)
        {
            var validador = new TValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            var mapeador = new TMapeador();
            mapeador.ConfigurarParametros(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public void Excluir(T registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        public T SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRegistro = comandoSelecao.ExecuteReader();

            var mapeador = new TMapeador();

            T registro = null;
            if (leitorRegistro.Read())
                registro = mapeador.ConverterRegistro(leitorRegistro);

            conexaoComBanco.Close();

            return registro;
        }

        public List<T> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorRegistro = comandoSelecao.ExecuteReader();

            var mapeador = new TMapeador();

            List<T> registros = new List<T>();

            while (leitorRegistro.Read())
                registros.Add(mapeador.ConverterRegistro(leitorRegistro));

            conexaoComBanco.Close();

            return registros;
        }
    }
}
