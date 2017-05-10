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
    public class ClienteDao
    {
        private static Cliente getCliente(object[] dados)
        {
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt64(dados.GetValue(0));
            cliente.Nome = Convert.ToString(dados.GetValue(1));
            cliente.Cpf = Convert.ToString(dados.GetValue(2));

            return cliente;
        }


        public static List<Cliente> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Cliente> Lista = new List<Cliente>();

            try
            {
                //      String sql = "SELECT id, login, senha, nome, cpf, id_supermercado FROM usuario";
                String sql = "SELECT id, nome, cpf FROM cliente";
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
                    Lista.Add(getCliente(dt.Rows[i].ItemArray));
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

        public static Boolean Persistir(Cliente cliente)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (cliente.Id > 0)//update
                {
                    String sql = "UPDATE cliente SET nome = @nome, cpf = @cpf  WHERE id = @id";
                    cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    cliente.Id = DBUtil.getNextId("cliente");
                    String sql = "INSERT INTO cliente (id,nome,cpf) VALUES (@id,@nome,@cpf)";

                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf);
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


        public static Cliente BuscarPorId(long id)
        {
            Cliente cliente = null;
            try
            {
                SqlCommand cmd;
                String sql = "SELECT id, nome, cpf FROM cliente where id=@id";

                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                cliente = getCliente(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id o Usuário " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return cliente;
        }

        public static long getLastId()
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                String sql = "select max(id) as total from cliente";
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

        public static Boolean Excluir(Cliente cliente)
        {
            return Excluir(cliente.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from cliente where id=@id", conexao);
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