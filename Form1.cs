using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesafioWF_BD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEstoque_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente(txtNome.Text, txtCPF.Text, txtTelefone.Text, txtEmail.Text);
            MessageBox.Show("Cliente cadastrado: "       + c.nome +              " CPF: " + c.cpf      
                          + "Telefone:" + c.telefone               + "email:" + c.email) ;
            txtNome.Text = "";
            txtCPF.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            c.gravarCliente();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var preco = decimal.Parse(txtPreco.Text);
            var estoque = int.Parse(txtEstoque.Text);

            Produto p = new Produto(
                txtNomeProduto.Text, 
                preco,
                estoque
                );
            MessageBox.Show("Produto Cadastrado: " + p.Nome +
                " Preço: " + p.Preco +  " Estoque " + p.Estoque) ;
            txtNome.Text = "";
            txtPreco.Text = "";
            txtEstoque.Text = "";
            p.gravarProduto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'sistemaVendasDataSet.Cliente'. Você pode movê-la ou removê-la conforme necessário.
            this.clienteTableAdapter.Fill(this.sistemaVendasDataSet.Cliente);
            // TODO: esta linha de código carrega dados na tabela 'sistemaVendasDataSet.Produto'. Você pode movê-la ou removê-la conforme necessário.
            this.produtoTableAdapter.Fill(this.sistemaVendasDataSet.Produto);

        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            var linha = dtGridProdutos.CurrentCell.OwningRow;
            var idproduto = linha.Cells[4].Value.ToString();
            var nomeProduto = linha.Cells[1].Value.ToString();
            var valor = linha.Cells[2].Value.ToString();

            dtGridProdutosVenda.Rows.Add(new object[] { idproduto, nomeProduto, txtQuantidade.Text, decimal.Parse(valor) });
        }

        private void btnEfetuarVenda_Click(object sender, EventArgs e)
        {
            decimal valorTotal = 0;
            int idCliente = int.Parse(cmbCliente.SelectedValue.ToString());

            for (int i = 0; i < dtGridProdutosVenda.Rows.Count; i++)
            {
                valorTotal += Convert.ToDecimal(dtGridProdutosVenda.Rows[i].Cells[3].Value) * Convert.ToInt32(dtGridProdutosVenda.Rows[i].Cells[2].Value);
            }

            Venda v = new Venda(idCliente, valorTotal);
            //MessageBox.Show("Produto Cadastrado: " + p.Nome +
            //    " Preço: " + p.Preco + " Estoque " + p.Estoque);
            txtNome.Text = "";
            txtPreco.Text = "";
            txtEstoque.Text = "";
            var idVenda = v.gravarVenda();

            for (int i = 0; i < dtGridProdutosVenda.Rows.Count; i++)
            {
                var valorUnitario = Convert.ToDecimal(dtGridProdutosVenda.Rows[i].Cells[3].Value);
                ProdutoVenda pv =
                    new ProdutoVenda(
                        idVenda,
                        Convert.ToInt32(dtGridProdutosVenda.Rows[i].Cells[0].Value),
                        valorUnitario,
                        Convert.ToInt32(dtGridProdutosVenda.Rows[i].Cells[2].Value),
                        valorTotal);

                pv.gravarProdutoVenda();
            }

            MessageBox.Show("Venda realizada para o cliente "
                + cmbCliente.SelectedText 
                + ".\n Total da venda: " + valorTotal);
            dtGridProdutosVenda.Rows.Clear();
        }

        private void txtCPF_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
