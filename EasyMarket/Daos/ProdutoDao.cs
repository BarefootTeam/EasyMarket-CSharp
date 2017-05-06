using EasyMarket.Models;
using EasyMarket.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EasyMarket.Daos
{
    public class ProdutoDao
    {
        private static Produto getProduto(object[] dados)
        {
            Produto p = new Produto();
            p.Supermercado = new Supermercado(); 
            p.Id = Convert.ToInt64(dados.GetValue(0));
            p.Nome = Convert.ToString(dados.GetValue(1));
            p.Cod = Convert.ToString(dados.GetValue(2));
            p.Descricao = Convert.ToString(dados.GetValue(3));
            p.PrecoCusto = Convert.ToDecimal(dados.GetValue(4));
            p.Foto = Convert.ToString(dados.GetValue(5));
            DBUtil.closeConnection();
            p.Supermercado = SupermercadoDao.BuscarPorId(Convert.ToInt64(dados.GetValue(6)));
            return p;
        }


        public static List<Produto> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Produto> Lista = new List<Produto>();

            try
            {
                String sql = "select id ,nome ,cod ,descricao ,preco_custo , foto, id_supermercado from produto";
                // Cira o Comando que sera executado no bancp de dados e indica qual conexao
                SqlCommand cmd = new SqlCommand(sql, DBUtil.getConnection());
                //Abre a Conexao com obanco de dados
                DBUtil.getConnection().Open();
                //Execute query
                cmd.ExecuteNonQuery();
                //Criar um Data Set para armazenar o retorno da query
                DataTable dt = new DataTable();
                //Crie um Sql Data Adapter para pegar o retorno da quer7y e preencher um Data table
                SqlDataAdapter da = new SqlDataAdapter();
                //recuperar o retorno da query
                da.SelectCommand = cmd;
                // Preencher o Data Table
                da.Fill(dt);
                // Percorre a as Linhas do Data Table
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Lista.Add(getProduto(dt.Rows[i].ItemArray));
                }


            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);

            }
            finally
            {
                DBUtil.closeConnection();
            }
            return Lista;

        }

        public static List<Produto> BuscarPorSupermercado(long id)
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Produto> Lista = new List<Produto>();

            try
            {
                String sql = "select id ,nome ,cod ,descricao ,preco_custo , foto, id_supermercado from produto where id_supermercado = " + id;
                // Cira o Comando que sera executado no bancp de dados e indica qual conexao
                SqlCommand cmd = new SqlCommand(sql, DBUtil.getConnection());
                //Abre a Conexao com obanco de dados
                DBUtil.getConnection().Open();
                //Execute query
                cmd.ExecuteNonQuery();
                //Criar um Data Set para armazenar o retorno da query
                DataTable dt = new DataTable();
                //Crie um Sql Data Adapter para pegar o retorno da quer7y e preencher um Data table
                SqlDataAdapter da = new SqlDataAdapter();
                //recuperar o retorno da query
                da.SelectCommand = cmd;
                // Preencher o Data Table
                da.Fill(dt);
                // Percorre a as Linhas do Data Table
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Lista.Add(getProduto(dt.Rows[i].ItemArray));
                }


            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);

            }
            finally
            {
                DBUtil.closeConnection();
            }
            return Lista;

        }

        public static Produto BuscarPorId(long id)
        {
            Produto Produto = null;
            try
            {
                SqlCommand cmd;
                String sql = "select id ,nome ,cod ,descricao ,preco_custo , foto, id_supermercado from produto where id = @id ";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Produto = getProduto(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do Produto " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Produto;
        }

        public static Produto BuscarPorCodigo(String Cod)
        {
            Produto Produto = null;
            try
            {
                SqlCommand cmd;
                String sql = "select id ,nome ,cod ,descricao ,preco_custo , foto, id_supermercado from produto where cod like @cod ";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@cod", "%" + Cod + "%");
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Produto = getProduto(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por Codigo " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Produto;
        }

        public static Boolean Persistir(Produto Produto)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (Produto.Id > 0)//update
                {
                    String sql = "update produto set nome = @nome ,cod = @cod ,descricao = @descricao ,preco_custo = @preco_custo, foto = @foto, id_supermercado = @id_supermercado where id = @id ";
                    cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    Produto.Id = DBUtil.getNextId("produto");
                    String sql = "insert into produto(id,nome,cod,descricao,preco_custo,foto,id_supermercado) values (@id,@nome,@cod,@descricao,@preco_custo,@foto,@id_supermercado)";
                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", Produto.Id);
                cmd.Parameters.AddWithValue("@nome", Produto.Nome);
                cmd.Parameters.AddWithValue("@cod", Produto.Cod);
                cmd.Parameters.AddWithValue("@descricao", Produto.Descricao);
                cmd.Parameters.AddWithValue("@preco_custo", Produto.PrecoCusto);
                cmd.Parameters.AddWithValue("@foto", Produto.Foto);
                cmd.Parameters.AddWithValue("@id_supermercado", Produto.Supermercado.Id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Persistencia - Erro ao conectar ao banco de dados" + e.Message);
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public static long getLastId()
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                String sql = "select max(id) as total from produto ";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                retorno = Convert.ToInt64(dt.Rows[0].ItemArray[0]);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return retorno;
        }

        public static Boolean Excluir(Produto Produto)
        {
            return Excluir(Produto.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from produto where id=@id", conexao);
                conexao.Open();
                cmd.Parameters.AddWithValue("@id", id);
                total = cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exclusão - Erro ao conectar ao banco de dados" + e.Message);
            }
            finally
            {
                conexao.Close();
            }

            return total > 0;
        }

    }
}