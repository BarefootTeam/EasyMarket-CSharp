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
    public class SupermercadoDao
    {
        private static Supermercado getSupermercado(object[] dados)
        {
            Supermercado s = new Supermercado();
            s.Id = Convert.ToInt64(dados.GetValue(0));
            s.Nome = Convert.ToString(dados.GetValue(1));
            s.Cnpj = Convert.ToString(dados.GetValue(2));
            

            return s;
        }


        public static List<Supermercado> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Supermercado> Lista = new List<Supermercado>();

            try
            {
                String sql = "select id, nome, cnpj from supermercado";
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
                    Lista.Add(getSupermercado(dt.Rows[i].ItemArray));
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

        public static Boolean Persistir(Supermercado Supermercado)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (Supermercado.Id > 0)//update
                {
                    String sql = " UPDATE supermercado SET  nome = @nome, cnpj = @cnpj WHERE id = @id ";
                    cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    Supermercado.Id = DBUtil.getNextId("supermercado");
                    String sql = "insert into supermercado (id, nome, cnpj) values (@id, @nome, @cnpj)";
                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", Supermercado.Id);
                cmd.Parameters.AddWithValue("@nome", Supermercado.Nome);
                cmd.Parameters.AddWithValue("@cnpj", Supermercado.Cnpj);
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


        public static Supermercado BuscarPorId(long id)
        {
            Supermercado Supermercado = null;
            try
            {
                SqlCommand cmd;
                String sql = "select id, nome, cnpj from supermercado where id = @id ";
                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Supermercado = getSupermercado(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id o Supermercado " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Supermercado;
        }

        public static long getLastId()
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                String sql = "select max(id) as total from supermercado ";
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

        public static Boolean Excluir(Supermercado Supermercado)
        {
            return Excluir(Supermercado.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from supermercado where id=@id", conexao);
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