using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : RepositorioBase<Requisicao, ValidadorRequisicao, MapeadorRequisicao>
    {
        #region SQL Queries
        protected override string sqlInserir =>
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

        protected override string sqlEditar =>
          @"UPDATE TBREQUISICAO
		        SET
			        FUNCIONARIO_ID = @FUNCIONARIO_ID,
                    PACIENTE_ID = @PACIENTE_ID,
                    MEDICAMENTO_ID = @MEDICAMENTO_ID,
                    QUANTIDADEMEDICAMENTO = @QUANTIDADEMEDICAMENTO,
                    DATA = @DATA
		        WHERE
			        ID = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM TBREQUISICAO
		        WHERE
			        ID = @ID";

        protected override string sqlSelecionarPorId =>
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
                    TBFUNCIONARIO FUNC ON R.FUNCIONARIO_ID = FUNC.ID
                INNER JOIN
                    TBPACIENTE P ON R.PACIENTE_ID = P.ID
                INNER JOIN
                    TBMEDICAMENTO M ON R.MEDICAMENTO_ID = M.ID
                WHERE
                    R.ID = @ID";

        protected override string sqlSelecionarTodos =>
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
                    TBFUNCIONARIO FUNC ON R.FUNCIONARIO_ID = FUNC.ID
                INNER JOIN
                    TBPACIENTE P ON R.PACIENTE_ID = P.ID
                INNER JOIN
                    TBMEDICAMENTO M ON R.MEDICAMENTO_ID = M.ID";

        #endregion
    }
}
