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
    public class UsuarioDao
    {
        private static Usuario getUsuario(object[] dados)
        {
            Usuario Usuario = new Usuario();
            Usuario.Id = Convert.ToInt64(dados.GetValue(0));
            Usuario.Login = Convert.ToString(dados.GetValue(1));
            Usuario.Senha = Convert.ToString(dados.GetValue(2));
            Usuario.Nome = Convert.ToString(dados.GetValue(3));
            Usuario.Cpf = Convert.ToString(dados.GetValue(4));
        //    DBUtil.closeConnection();
         //   Usuario.Supermercado = SupermercadoDao.BuscarPorId(Convert.ToInt64(dados.GetValue(5)));
            
            return Usuario;
        }


        public static List<Usuario> BuscarTodos()
        {
            // Cria a Lista que ira retornar os Objetos da Tabela Passado Por Paramêtro, do banco de da dados

            List<Usuario> Lista = new List<Usuario>();

            try
            {
                //      String sql = "SELECT id, login, senha, nome, cpf, id_supermercado FROM usuario";
                     String sql = "SELECT id, login, senha, nome, cpf FROM usuario";
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
                    Lista.Add(getUsuario(dt.Rows[i].ItemArray));
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

        public static Boolean Persistir(Usuario Usuario)
        {
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;

                if (Usuario.Id > 0)//update
                {
                    //OBs: Testando Gravar Usuario sem Supermercado depois Modficar !!!!!!

                //    String sql = "update usuario set login = @login, senha = @senha, nome = @nome, cpf = @cpf, id_supermercado = @id_supermercado WHERE id = @id";
                    String sql = "update usuario set login = @login, senha = @senha, nome = @nome, cpf = @cpf  WHERE id = @id";

                    cmd = new SqlCommand(sql, conexao);
                }
                else //insert
                {
                    //Calcular proximo ID - Função da Classe DbUtil
                    Usuario.Id = DBUtil.getNextId("usuario");
                    //  String sql = "insert into usuario(id,login,senha,nome,cpf,id_supermercado) values (@id,@login,@senha,@nome,@cpf,@id_supermercado)";
                        String sql = "insert into usuario(id,login,senha,nome,cpf) values (@id,@login,@senha,@nome,@cpf)";

                    cmd = new SqlCommand(sql, conexao);
                }

                conexao.Open();
                cmd.Parameters.AddWithValue("@id", Usuario.Id);
                cmd.Parameters.AddWithValue("@login", Usuario.Login);
                cmd.Parameters.AddWithValue("@senha", Usuario.Senha);
                cmd.Parameters.AddWithValue("@nome", Usuario.Nome);
                cmd.Parameters.AddWithValue("@cpf", Usuario.Cpf);
           //     cmd.Parameters.AddWithValue("@id_supermercado", Usuario.Supermercado.Id);
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


        public static Usuario BuscarPorId(long id)
        {
            Usuario Usuario = null;
            try
            {
                SqlCommand cmd;
            //    String sql = "SELECT id, login, senha, nome, cpf, id_supermercado FROM usuario where id=@id";
                String sql = "SELECT id, login, senha, nome, cpf FROM usuario where id=@id";

                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Usuario = getUsuario(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por id o Usuário " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Usuario;
        }

        public static Usuario BuscarPorLogin(String login)
        {
            Usuario Usuario = null;
            try
            {
                SqlCommand cmd;
             //   String sql = "SELECT id, login, senha, nome, cpf, id_supermercado FROM usuario where login = @login ";
                String sql = "SELECT id, login, senha, nome, cpf FROM usuario where login = @login ";

                cmd = new SqlCommand(sql, DBUtil.getConnection());
                DBUtil.getConnection().Open();
                cmd.Parameters.AddWithValue("@login", login);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);

                Usuario = getUsuario(dt.Rows[0].ItemArray);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro ao buscar por login o Usuário " + e.Message);
            }
            finally
            {
                DBUtil.closeConnection();
            }

            return Usuario;
        }

        public static long getLastId()
        {
            long retorno = 0;
            try
            {
                SqlCommand cmd;
                String sql = "select max(id) as total from usuario ";
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

        public static Boolean Excluir(Usuario Usuario)
        {
            return Excluir(Usuario.Id);
        }


        public static Boolean Excluir(long id)
        {
            int total = 0;
            SqlConnection conexao = DBUtil.getConnection();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("delete from usuario where id=@id", conexao);
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
