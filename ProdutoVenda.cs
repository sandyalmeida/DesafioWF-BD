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

    class ProdutoVenda
    {
        public int IdVenda;
        public int IdProduto;
        public decimal ValorUnitario;
        public int Quantidade;
        public decimal ValorTotal;

        public ProdutoVenda(int idCliente, int idProduto, decimal valorUnitario, int quantidade, decimal valorTotal)
        {
            this.IdVenda = idCliente;
            this.IdProduto = idProduto;
            this.ValorUnitario = valorUnitario;
            this.Quantidade = quantidade;
            this.ValorTotal = valorTotal;
        }

        public bool gravarProdutoVenda()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into ProdutoVenda values (@idVenda, @idProduto, @valorUnitario, @quantidade, @valorTotal);";
            command.Parameters.Add("@idVenda", SqlDbType.Int);
            command.Parameters.Add("@idProduto", SqlDbType.Int);
            command.Parameters.Add("@valorUnitario", SqlDbType.Decimal);
            command.Parameters.Add("@quantidade", SqlDbType.Int);
            command.Parameters.Add("@valorTotal", SqlDbType.Decimal);
            command.Parameters[0].Value = this.IdVenda;
            command.Parameters[1].Value = this.IdProduto;
            command.Parameters[2].Value = this.ValorUnitario;
            command.Parameters[3].Value = this.Quantidade;
            command.Parameters[4].Value = this.ValorTotal;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }

            finally
            {
                banco.fecharConexao();
            }


        }

    }
}