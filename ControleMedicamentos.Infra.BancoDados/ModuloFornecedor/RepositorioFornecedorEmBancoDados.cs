using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados : 
        RepositorioBase<Fornecedor, ValidadorFornecedor, MapeadorFornecedor>
    {
        #region SQL Queries
        protected override string sqlInserir
        {
            get =>
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
        }

        protected override string sqlEditar
        {
            get =>
           @"UPDATE TBFORNECEDOR
		        SET
			        NOME = @NOME,
                    TELEFONE = @TELEFONE,
                    EMAIL = @EMAIL,
                    CIDADE = @CIDADE,
                    ESTADO = @ESTADO
		        WHERE
			        ID = @ID";
        }

        protected override string sqlExcluir
        {
            get =>
            @"DELETE FROM TBFORNECEDOR
		        WHERE
			        ID = @ID";
        }

        protected override string sqlSelecionarPorId
        {
            get =>
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
        }

        protected override string sqlSelecionarTodos
        {
            get =>
             @"SELECT 
		            ID, 
		            NOME,
                    TELEFONE,
                    EMAIL,
                    CIDADE,
                    ESTADO
	            FROM 
		            TBFORNECEDOR";
        }

        #endregion
    }
}
