using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDados : 
        RepositorioBase<Funcionario, ValidadorFuncionario, MapeadorFuncionario>
    {
        #region SQL Queries
        protected override string sqlInserir
        {
            get =>
             @"INSERT INTO TBFUNCIONARIO
             (
                    NOME,
                    LOGIN,
                    SENHA
             )
            VALUES
            (
                    @NOME,
                    @LOGIN,
                    @SENHA
            );SELECT SCOPE_IDENTITY();";
        }

        protected override string sqlEditar
        {
            get =>
           @"UPDATE TBFUNCIONARIO
		        SET
			        NOME = @NOME,
                    LOGIN = @LOGIN,
                    SENHA = @SENHA
		        WHERE
			        ID = @ID";
        }

        protected override string sqlExcluir
        {
            get =>
            @"DELETE FROM TBFUNCIONARIO
		        WHERE
			        ID = @ID";
        }

        protected override string sqlSelecionarPorId
        {
            get =>
            @"SELECT 
		            ID, 
		            NOME,
                    LOGIN,
                    SENHA
	            FROM 
		            TBFUNCIONARIO
		        WHERE
                    ID = @ID";
        }

        protected override string sqlSelecionarTodos
        {
            get =>
             @"SELECT 
		            ID, 
		            NOME,
                    LOGIN,
                    SENHA
	            FROM 
		            TBFUNCIONARIO";
        }
        #endregion
    }
}
