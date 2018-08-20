using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Jazztastic3ASPXWebForms
{
    public class Ticket
    {
        //fields

        //constructor

        //methods
        public static short[] GetTicketDays(string ticketType)
        {
            short[] ticketDays = new short[3];
            if (ticketType[0] == 'T')
                ticketDays[0] = 1;
            else
                ticketDays[0] = 0;
            if (ticketType[1] == 'T')
                ticketDays[1] = 2;
            else
                ticketDays[1] = 0;
            if (ticketType[2] == 'T')
                ticketDays[2] = 3;
            else
                ticketDays[2] = 0;

            return ticketDays;
        }

        public static string GetTicketDaysText(string ticketType) {
            string temp = "";
            if (ticketType[0] == 'T')
                temp += "01/07/2018,";
            if (ticketType[1] == 'T')
                temp += "02/07/2018,";
            if (ticketType[2] == 'T')
                temp += "03/07/2018";

            return temp;
        }
        public static string GetTicketType(string ticketDates)
        {
            string ticketType = "";

            if (ticketDates.Contains("01/07/2018"))
                ticketType += "T";
            else
                ticketType += "F";
            if (ticketDates.Contains("02/07/2018"))
                ticketType += "T";
            else
                ticketType += "F";
            if (ticketDates.Contains("03/07/2018"))
                ticketType += "T";
            else
                ticketType += "F";

            return ticketType;
        }

        public static bool ReduceTicketQuantityBy1(string ticketType)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            short[] ticketDays = GetTicketDays(ticketType);

            string sql = "UPDATE ticket_quantity " +
                         "SET quantity_left = quantity_left - 1 ";
            bool firstAdded = false;
            for (int i = 0; i < ticketDays.Length; i++)
            {
                if (ticketDays[i] != 0 && !firstAdded)
                {
                    sql += $"WHERE day = {ticketDays[i]} ";
                    firstAdded = true;
                }
                else if (ticketDays[i] != 0)
                    sql += $"OR day = {ticketDays[i]} ";
            }

            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            connection.Open();
            myTrans = connection.BeginTransaction();
            myCommand.Connection = connection;
            try
            {
                myCommand.Transaction = myTrans;
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                myTrans.Commit();
                return true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
                return false;
            }
            finally
            {
                myCommand.Dispose();
                connection.Close();
            }

        }

        public static bool TicketAvailable(string ticketType, out string errorMessage)
        {
            errorMessage = "";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            short[] ticketDates = GetTicketDays(ticketType);

            string sql = "SELECT quantity_left " +
                         "FROM ticket_quantity ";
            bool firstAdded = false;
            for (int i = 0; i < ticketDates.Length; i++)
            {
                if (ticketDates[i] != 0 && !firstAdded)
                {
                    sql += $"WHERE day = {ticketDates[i]} ";
                    firstAdded = true;
                }
                else if (ticketDates[i] != 0)
                    sql += $"OR day = {ticketDates[i]} ";
            }

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                int line = 0;
                errorMessage = "No tickets left for ";
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader[0]) <= 0)
                    {
                        if (line == 0)
                            errorMessage += "01/07/2018, ";
                        else if (line == 1)
                            errorMessage += "02/07/2018, ";
                        else if (line == 2)
                            errorMessage += "03/07/2018 ";
                    }
                    line++;
                }
                if (errorMessage.EndsWith(", "))
                    errorMessage = errorMessage.Remove(errorMessage.Length - 2);
                if (errorMessage != "No tickets left for ")
                    return false;
                else
                    return true;
            }
            catch (MySqlException)
            {
                errorMessage = "Error!";
                return false;
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}