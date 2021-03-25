using AtividadeAdoNet.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AtividadeAdoNet
{
    class Program
    {
        private static SqlConnection SqlConnection;
        
        static void Main(string[] args)
        {
            IniciarConexao();
            // GravarNovoCliente();
            // ExcluirCliente();
            //ListarCLientes();
            //GravarNovoProduto();
            ListarProdutos();

            Console.ReadKey();
        }

        //INICIAR A CONEXÃO DO BD NOS MÉTODOS
        private static void IniciarConexao()
        {
            var connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = dbLoja; Integrated Security = True; Connect Timeout = 30;";

            SqlConnection = new SqlConnection();
            SqlConnection.ConnectionString = connectionString;
        }

        private static void GravarNovoCliente(){

            SqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "insert into Cliente values(@id, @nome,@email, @senha)";

            var cliente = new Cliente("anailson", "anailson@gmail.com", "436363");
            
                     
            sqlCommand.Parameters.Add(new SqlParameter("@id", cliente.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@nome", cliente.Nome));
            sqlCommand.Parameters.Add(new SqlParameter("@email", cliente.Email));
            sqlCommand.Parameters.Add(new SqlParameter("@senha", cliente.Senha));

            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Cliente Cadastrado com sucesso!");
            }
            SqlConnection.Close();
            SqlConnection.Dispose();
            
            


        }


        private static void ExcluirCliente()
        {


            IniciarConexao();
            SqlConnection.Open();
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "delete from Cliente where id=@id";

            var clienteId = "901CE422-91E9-4A6A-A23E-EC06083585B0";
            sqlCommand.Parameters.Add(new SqlParameter("@id", clienteId));

            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Cliente excluído com sucesso");
            }

            SqlConnection.Close();
            SqlConnection.Dispose();

           
        }

        public static void ListarCLientes()
        {
            IniciarConexao();
            SqlConnection.Open();
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "select Id,Nome,Email from Cliente";

            var sqlDataReader = sqlCommand.ExecuteReader();

            List<Cliente> listaClientes = new List<Cliente>();

            while(sqlDataReader.Read())
            {
                Guid id = Guid.Parse(sqlDataReader[0].ToString());
                var cliente = new Cliente(id);
                cliente.Atualizar(sqlDataReader[1].ToString(), sqlDataReader[2].ToString());
                listaClientes.Add(cliente);
            }

            sqlDataReader.Close();
            SqlConnection.Close();
            SqlConnection.Dispose();


            foreach (var item in listaClientes)
            {
                Console.WriteLine($"Nome: {item.Nome} - Email:{item.Email}");

            }
                        

        }



        /*GRAVAR PRODUTOS*/
        private static void GravarNovoProduto() {

            IniciarConexao();

            SqlConnection.Open();

            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "insert into Produto values(@id, @nome,@precounitario, @quantidadeestoque)";

            var produto = new Produto("Notebook Dell", Convert.ToDecimal("5.400"), Convert.ToInt32("5"));
                       

            sqlCommand.Parameters.Add(new SqlParameter("@id", produto.Id));
            sqlCommand.Parameters.Add(new SqlParameter("@nome", produto.Nome));
            sqlCommand.Parameters.Add(new SqlParameter("@precounitario", produto.PrecoUnitario));
            sqlCommand.Parameters.Add(new SqlParameter("@quantidadeestoque", produto.QuantidadeEstoque));


            var qtdRows = sqlCommand.ExecuteNonQuery();

            if (qtdRows > 0)
            {
                Console.WriteLine("Produto Cadastrado com sucesso!");
            }


        }

        //LISTAR PRODUTOS
        public static void ListarProdutos()
        {

            IniciarConexao();
            SqlConnection.Open();
            
            var sqlCommand = new SqlCommand();
            sqlCommand.Connection = SqlConnection;
            sqlCommand.CommandText = "select Id, Nome, PrecoUnitario, QuantidadeEstoque from Produto";

            var sqlDataReader = sqlCommand.ExecuteReader();

            List<Produto> listaProdutos = new List<Produto>();

            while (sqlDataReader.Read())
            {
                Guid id = Guid.Parse(sqlDataReader[0].ToString());

                var produto = new Produto(id);

                // produto.Atualizar(sqlDataReader[1].ToString(), decimal.Parse(sqlDataReader[2], (int)sqlDataReader[3]);
                //https://docs.microsoft.com/pt-br/dotnet/api/system.decimal.parse?view=net-5.0

                produto.Atualizar(sqlDataReader[1].ToString(), decimal.Parse(sqlDataReader[2].ToString()), int.Parse(sqlDataReader[3].ToString()));


                listaProdutos.Add(produto);

            }
                
                
                sqlDataReader.Close();
                SqlConnection.Close();
                SqlConnection.Dispose();

                foreach (var item in listaProdutos)
                {
                    Console.WriteLine($"Nome: {item.Nome} - Preço Unitário $: {item.PrecoUnitario} - Quantidade Estoque: {item.QuantidadeEstoque}");
                }




            }


        }
    }
