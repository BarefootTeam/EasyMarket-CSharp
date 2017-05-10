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
    public class CarrinhoDao
    {
        private static Carrinho getCarrinho(object[] dados, bool selecionarItem)
        {
            Carrinho carrinho = new Carrinho();
            carrinho.Id = Convert.ToInt64(dados.GetValue(0));
            carrinho.Status = Convert.ToBoolean(dados.GetValue(1));
            carrinho.Data = Convert.ToDateTime(dados.GetValue(2));
            DBUtil.closeConnection();

            if (selecionarItem)
            {
                carrinho.Itens = ItemCarrinhoDao.BuscarPorCarrinho(carrinho.Id);
            }
            carrinho.Usuario = UsuarioDao.BuscarPorId(Convert.ToInt64(dados.GetValue(3)));
            return carrinho;
        }


        public static List<Carrinho> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Carrinho> Lista = new List<Carrinho>();

            try
            {
                String sql = "SELECT id, status, data, id_usuario FROM carrinho";
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
                    Lista.Add(getCarrinho(dt.Rows[i].ItemArray, true));
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

        public static List<Carrinho> buscarPorStatus(Boolean status)
        {
            List<Carrinho> Carrinho = new List<Carrinho>();

            try
            {
                String sql = "SELECT id, status, data, id_usuario FROM carrinho WHERE status = @status";
                SqlCommand cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Carrinho.Add(getCarrinho(ds.Tables[0].Rows[i].ItemArray, true));
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
            return Carrinho;
        }



        public static Boolean Persistir(Carrinho carrinho)
        {
            return PersistirRetorno(carrinho) != null;
        }

        public static Carrinho PersistirRetorno(Carrinho carrinho)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (carrinho.Id > 0)//update
                {
                      String sql = "UPDATE carrinho SET status = @status, data = @data, id_usuario = @id_usuario WHERE id = @id";
                      cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    carrinho.Id = DBUtil.getNextId("carrinho");
                    String sql = "INSERT INTO carrinho(id, status, data, id_usuario) values (@id, @status, @data, @id_usuario)";
                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", carrinho.Id);
                cmd.Parameters.AddWithValue("@status", carrinho.Status);
                cmd.Parameters.AddWithValue("@data", carrinho.Data);
                cmd.Parameters.AddWithValue("@id_usuario", carrinho.Usuario.Id);
                cmd.ExecuteNonQuery();

                return carrinho;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Persistencia - Erro ao conectar ao banco de dados " + e.Message);
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }

        
        public static Carrinho BuscarPorId(long id, bool selecionarItem)
        {
            Carrinho Carrinho = null;
            try
            {
                SqlCommand cmd;
                String sql = "SELECT id, status, data, id_usuario FROM carrinho WHERE id = @id";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Carrinho = getCarrinho(dt.Rows[0].ItemArray, selecionarItem);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id do Carrinho" + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Carrinho;
        }
        
                public static long getLastId()
                {
                    long retorno = 0;
                    try
                    {
                        SqlCommand cmd;
                        String sql = "SELECT max(id) AS total FROM carrinho ";
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
        
                public static Boolean Excluir(Carrinho Carrinho)
                {
                    return Excluir(Carrinho.Id);
                }


                public static Boolean Excluir(long id)
                {
                    int total = 0;
                    SqlConnection conexao = DBUtil.getConnection();
                    try
                    {
                        SqlCommand cmd;
                        cmd = new SqlCommand("DELETE FROM carrinho WHERE id=@id", conexao);
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
