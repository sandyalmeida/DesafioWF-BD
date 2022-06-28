using SimplesWindowsFormsBanco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioWF_BD
{

    class Venda
    {
        public int IdCliente;
        public decimal ValorTotalVenda;

        public Venda(int idCliente, decimal valortotal)
        {
            this.IdCliente = idCliente;
            this.ValorTotalVenda = valortotal;
        }

        public int gravarVenda()
        {
            int idVenda=0;
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "set nocount on; insert into Venda values (@codigoCliente, @valorTotal); select SCOPE_IDENTITY();";
            command.Parameters.Add("@codigoCliente", SqlDbType.Int);
            command.Parameters.Add("@valorTotal", SqlDbType.Decimal);
            command.Parameters[0].Value = this.IdCliente;
            command.Parameters[1].Value = this.ValorTotalVenda;

            try
            {
                var result = command.ExecuteScalar();
                tran.Commit();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return 0;
            }

            finally
            {
                banco.fecharConexao();
            }


        }

    }
}