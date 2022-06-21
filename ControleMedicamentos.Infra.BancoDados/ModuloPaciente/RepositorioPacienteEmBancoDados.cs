using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDados : 
        RepositorioBase<Paciente, ValidadorPaciente, MapeadorPaciente>
    {
        #region SQL Queries
        protected override string sqlInserir =>
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

        protected override string sqlEditar =>
           @"UPDATE TBPACIENTE
		        SET
			        NOME = @NOME,
                    CARTAOSUS = @CARTAOSUS
		        WHERE
			        ID = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM TBPACIENTE
		        WHERE
			        ID = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
		            ID, 
		            NOME,
                    CARTAOSUS
	            FROM 
		            TBPACIENTE
		        WHERE
                    ID = @ID";

        protected override string sqlSelecionarTodos =>
             @"SELECT 
		            ID, 
		            NOME,
                    CARTAOSUS
	            FROM 
		            TBPACIENTE";

        #endregion
    }
}
