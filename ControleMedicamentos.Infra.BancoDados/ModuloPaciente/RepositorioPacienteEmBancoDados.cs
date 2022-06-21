using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDados : 
        RepositorioBase<Paciente, ValidadorPaciente, MapeadorPaciente>
    {
        #region SQL Queries
        protected override string sqlInserir
        {
            get =>
             @"INSERT INTO TBPACIENTE
             (
                    NOME,
                    CARTAOSUS
             )
            VALUES
            (
                    @NOME,
                    @CARTAOSUS
            );SELECT SCOPE_IDENTITY();";
        }

        protected override string sqlEditar
        {
            get =>
           @"UPDATE TBPACIENTE
		        SET
			        NOME = @NOME,
                    CARTAOSUS = @CARTAOSUS
		        WHERE
			        ID = @ID";
        }

        protected override string sqlExcluir
        {
            get =>
            @"DELETE FROM TBPACIENTE
		        WHERE
			        ID = @ID";
        }

        protected override string sqlSelecionarPorId
        {
            get =>
            @"SELECT 
		            ID, 
		            NOME,
                    CARTAOSUS
	            FROM 
		            TBPACIENTE
		        WHERE
                    ID = @ID";
        }

        protected override string sqlSelecionarTodos
        {
            get =>
             @"SELECT 
		            ID, 
		            NOME,
                    CARTAOSUS
	            FROM 
		            TBPACIENTE";
        }

        #endregion
    }
}
