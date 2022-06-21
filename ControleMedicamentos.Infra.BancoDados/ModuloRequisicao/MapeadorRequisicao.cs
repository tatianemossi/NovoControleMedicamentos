using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class MapeadorRequisicao : MapeadorBase<Requisicao>
    {

        public override Requisicao ConverterRegistro(SqlDataReader leitorRequisicao)
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
                IdFuncionario = idFuncionario,
                IdMedicamento = idMedicamento,
                IdPaciente = idPaciente,

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

        public override void ConfigurarParametros(Requisicao requisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", requisicao.Id);
            comando.Parameters.AddWithValue("DATA", requisicao.Data);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.IdFuncionario);
            comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.IdPaciente);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.IdMedicamento);
        }
    }
}
