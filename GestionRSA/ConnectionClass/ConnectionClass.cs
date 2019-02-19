using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionClass
{
    public class ConnectionClass
    {
        private string _ConnectionString;
        SqlConnection connexxion;
        public void GetConnexionString()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["RepublicSystemConnectionString"].ConnectionString;
        }
        public void ConnectBD()
        {
            GetConnexionString();
            connexxion = new SqlConnection(_ConnectionString);
            connexxion.Open();
        }

        public DataSet PortaTaula(string nomTaula)
        {

            ConnectBD();
            string query = "SELECT * FROM " + nomTaula + "";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _ConnectionString);
            DataSet dtsCli = new DataSet();
            adapter.Fill(dtsCli, nomTaula);
            connexxion.Close();
            return dtsCli;
        }
        public DataSet PortarPerID(string nomTaula, string nomCamp, int valor)
        {
            ConnectBD();
            string query = "SELECT * FROM " + nomTaula + " WHERE " + nomCamp + " = " + valor + "";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _ConnectionString);
            DataSet dtsCli = new DataSet();
            adapter.Fill(dtsCli, nomTaula);
            return dtsCli;

        }
        public DataSet PortarPerID(string nomTaula, int valor)
        {
            ConnectBD();
            string queryTable = "SELECT * FROM " + nomTaula;
            SqlDataAdapter adapterTable = new SqlDataAdapter(queryTable, _ConnectionString);
            DataSet dtsTable = new DataSet();
            adapterTable.Fill(dtsTable, nomTaula);
            DataColumn[] columns;
            columns = dtsTable.Tables[nomTaula].PrimaryKey;

            string NomPK;
            int num = columns.Count();
            NomPK = columns[0].ColumnName;

            string query = "SELECT * FROM " + nomTaula + " WHERE " + NomPK + " = " + valor + "";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _ConnectionString);
            DataSet dtsCli = new DataSet();
            adapter.Fill(dtsCli, nomTaula);
            return dtsCli;

        }
        public void Executa(string consulta)
        {
            try
            {
                ConnectBD();
                SqlCommand comanda = new SqlCommand(consulta, connexxion);
                comanda.ExecuteNonQuery();
            }
            catch (SqlException)
            {
            }
            finally
            {
                connexxion.Close();

            }
        }
        public void Actualitzar(DataSet dts, string consulta)
        {
            try
            {
                ConnectBD();
                SqlDataAdapter adapterDts = new SqlDataAdapter(consulta, _ConnectionString);
                SqlCommandBuilder sqlCommand = new SqlCommandBuilder(adapterDts);

                adapterDts.Update(dts);
            }
            catch (SqlException)
            {

            }
            finally
            {
                connexxion.Close();

            }
        }
        public DataSet PortaPerConsulta(string consultaSql)
        {

            ConnectBD();

            SqlDataAdapter adapter = new SqlDataAdapter(consultaSql, _ConnectionString);
            DataSet dtsCli = new DataSet();
            adapter.Fill(dtsCli);
            return dtsCli;
        }

        public DataSet ComprobarUser(string nombre, string contra)
        {

            ConnectBD();
            string query = "SELECT Users.*, UserCategories.DescCategory,UserCategories.AccessLevel FROM Users INNER JOIN UserCategories ON Users.idUserCategory = UserCategories.idUserCategory WHERE UserName='" + nombre + "' AND Password ='" + contra + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _ConnectionString);
            DataSet dtsCli = new DataSet();
            adapter.Fill(dtsCli);
            connexxion.Close();
            return dtsCli;
        }
    }
}
