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
    public class CompraDao
    {
        private static Compra getCompra(object[] dados)
        {
            Compra compra = new Compra();
            compra.Id = Convert.ToInt64(dados.GetValue(0));
            compra.Nome = Convert.ToString(dados.GetValue(1));
            compra.Total = Convert.ToDecimal(dados.GetValue(2));
            compra.data = Convert.ToDateTime(dados.GetValue(3));

            return compra;
        }


        public static List<Compra> BuscarTodasCompras()
        {
            List<Compra> Lista = new List<Compra>();

            try
            {
                String sql = "select CarrinhoId , nome,Total,data from carrinho " + 
                             "   inner join cliente on " +
                             "   carrinho.id_cliente = cliente.id " +
                             "   inner join Compra on CarrinhoId = carrinho.id ";
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
                    Lista.Add(getCompra(dt.Rows[i].ItemArray));
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
    }
}