using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class MapeadorPaciente : MapeadorBase<Paciente>
    {
        public override Paciente ConverterRegistro(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaoSus = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            var paciente = new Paciente
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaoSus
            };

            return paciente;
        }

        public override void ConfigurarParametros(Paciente novoPaciente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoPaciente.Id);
            comando.Parameters.AddWithValue("NOME", novoPaciente.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", novoPaciente.CartaoSUS);
        }
    }
}
