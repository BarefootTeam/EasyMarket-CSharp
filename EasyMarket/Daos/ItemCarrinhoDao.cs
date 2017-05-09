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
    public class ItemCarrinhoDao
    {
        private static ItemCarrinho getItemCarrinho(object[] dados)
        {
            ItemCarrinho Itens = new ItemCarrinho();
            Itens.Id = Convert.ToInt64(dados.GetValue(0));
            Itens.Valor = Convert.ToDecimal(dados.GetValue(1));
            Itens.Quantidade = Convert.ToInt32(dados.GetValue(2));
            DBUtil.closeConnection();
            Itens.Carrinho = CarrinhoDao.BuscarPorId(Convert.ToInt64(dados.GetValue(3)));
            DBUtil.closeConnection();
            Itens.Produto = ProdutoDao.BuscarPorId(Convert.ToInt64(dados.GetValue(4)));
            
            return Itens;
        }
        
        public static List<ItemCarrinho> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<ItemCarrinho> Lista = new List<ItemCarrinho>();

            try
            {
                String sql = "select id,valor,quantidade,id_carrinho,id_produto from itens_carrinho";
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
                    Lista.Add(getItemCarrinho(dt.Rows[i].ItemArray));
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


        public static List<ItemCarrinho> BuscarPorProduto(long Id)
        {
            List<ItemCarrinho> itens = new List<ItemCarrinho>();

            try
            {
                String sql = "select id,valor,quantidade,id_carrinho,id_produto from itens_carrinho where id_produto = @id_produto ";
                SqlCommand cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id_produto", Id);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itens.Add(getItemCarrinho(ds.Tables[0].Rows[i].ItemArray));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
            finally
            {
                DBUtil.closeConnection();
            }
            return itens;
        }

        public static List<ItemCarrinho> BuscarPorCarrinho(Carrinho carrinho)
        {
            return BuscarPorCarrinho(carrinho.Id);
        }

        public static List<ItemCarrinho> BuscarPorCarrinho(long Id)
        {
            Debug.Write(Id);

            List<ItemCarrinho> itens = new List<ItemCarrinho>();

            try
            {
                String sql = "select id,valor,quantidade,id_carrinho,id_produto from itens_carrinho where id_carrinho = @id_carrinho ";
                SqlCommand cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id_carrinho", Id);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itens.Add(getItemCarrinho(ds.Tables[0].Rows[i].ItemArray));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
            finally
            {
                DBUtil.closeConnection();
            }
            return itens;
        }

        public static Boolean Persistir(ItemCarrinho ItemCarrinho)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (ItemCarrinho.Id > 0)//update
                {
                    String sql = "update itens_carrinho set id_carrinho = @id_carrinho, id_produto = @id_produto, valor = @valor, quantidade = @quantidade where id = @id ";
                    cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    ItemCarrinho.Id = DBUtil.getNextId("itens_carrinho");
                    String sql = "insert into itens_carrinho (id, id_carrinho, id_produto, valor, quantidade) values (@id, @id_carrinho, @id_produto, @valor, @quantidade) ";
                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", ItemCarrinho.Id);
                cmd.Parameters.AddWithValue("@id_carrinho", ItemCarrinho.Carrinho.Id);
                cmd.Parameters.AddWithValue("@id_produto", ItemCarrinho.Produto.Id);
                cmd.Parameters.AddWithValue("@valor", ItemCarrinho.Valor);
                cmd.Parameters.AddWithValue("@quantidade", ItemCarrinho.Quantidade);
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
                DBUtil.closeConnection();
            }
        }


        public static ItemCarrinho BuscarPorId(long id)
        {
            ItemCarrinho ItemCarrinho = null;
            try
            {
                SqlCommand cmd;
                String sql = "select id, valor, quantidade, id_carrinho, id_produto from itens_carrinho where id = @id ";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                ItemCarrinho = getItemCarrinho(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id os Itens do Carrinho" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return ItemCarrinho;
        }

        public static long getLastId()
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                String sql = "select max(id) as total from itens_carrinho ";
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

        public static Boolean Excluir(ItemCarrinho ItemCarrinho)
        {
            return Excluir(ItemCarrinho.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from itens_carrinho where id=@id", conexao);
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
                DBUtil.closeConnection();
            }

            return total > 0;
        }

        
    }
}