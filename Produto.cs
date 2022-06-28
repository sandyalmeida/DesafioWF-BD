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

    class Produto
    {
        public int IdProduto;
        public string Nome;
        public decimal Preco;
        public int Estoque;
        public Produto(string nome, decimal preco, int estoque)
        {
            this.Nome = nome;
            this.Preco =preco;
            this.Estoque = estoque;
        }

        public bool gravarProduto()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into produto values (@nome, @preco, @estoque);";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@preco", SqlDbType.Decimal);
            command.Parameters.Add("@estoque", SqlDbType.Int);
            command.Parameters[0].Value = this.Nome;
            command.Parameters[1].Value = this.Preco;
            command.Parameters[2].Value = this.Estoque;

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