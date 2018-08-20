using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jazztastic3ASPXWebForms
{
    public partial class AspDotNetAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod()]
        public static bool AvailableSpot(int spotNo, string areaNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
                string sql = "SELECT spots_taken " +
                             "FROM camping_spot " +
                             $"WHERE spotNo = {spotNo} " +
                             $"AND area_letter = '{areaNo}';";
                MySqlCommand command = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader[0]) == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}