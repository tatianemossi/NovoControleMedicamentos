using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento>
    {
        #region SQL Queries
        protected override string sqlInserir =>
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

        protected override string sqlEditar =>
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

        protected override string sqlExcluir=>
            @"DELETE FROM TBMEDICAMENTO
		        WHERE
			        ID = @ID";

        protected override string sqlSelecionarPorId =>
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

        protected override string sqlSelecionarTodos =>
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
    }
}
