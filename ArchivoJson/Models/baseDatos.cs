using archivojson.Models.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace archivojson.Models
{
    public class baseDatos
    {
        public MySqlConnection connection;

        public baseDatos()
        {
            connection = new MySqlConnection("datasource=localhost;port=3306;username=root;database=facturas;SSLMode=none");
        }

        public string ejecutarSQL(string sql)
        {
            string resultado = "";

            connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                int filasResultado = cmd.ExecuteNonQuery();

                if (filasResultado > -1)
                {
                    resultado = "Correcto";
                }
                else
                {
                    resultado = "Incorrecto";
                }

                
            }
            catch(Exception ex)
            {
                resultado = ex.Message;
            }
            connection.Close();

            return resultado;
        }

        public DataTable createTable(string sql)
        {
            DataTable datostabla = new DataTable();

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(datostabla);
                connection.Close();
                adapter.Dispose();
            }
            catch
            {
                datostabla = null;
            }
            return datostabla;
        }

    }
}
