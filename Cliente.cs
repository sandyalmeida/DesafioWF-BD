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

    class Cliente
    {
        public string nome;
        public string cpf;
        public string telefone;
        public string email;

        public Cliente()
        {

        }
        public Cliente(string nome, string cpf, string telefone, string email)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.email = email;

        }

        public bool gravarCliente()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into cliente values (@cpf, @nome, @telefone, @email );";
            command.Parameters.Add("@cpf", SqlDbType.VarChar);
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@telefone", SqlDbType.VarChar);
            command.Parameters.Add("@email", SqlDbType.VarChar);
            command.Parameters[0].Value = this.cpf;
            command.Parameters[1].Value = this.nome;
            command.Parameters[2].Value = this.telefone;
            command.Parameters[3].Value = this.email;


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