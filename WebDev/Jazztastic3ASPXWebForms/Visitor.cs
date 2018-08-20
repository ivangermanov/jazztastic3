using System;
using System.Configuration;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Jazztastic3ASPXWebForms
{
    public class Visitor
    {
        //fields
        private long visitorNo;
        private string governmentId;
        private string firstName;
        private string lastName;
        private string email;
        private string dob;
        private string password;
        private string ticketType;
        private string ticketDates;
        private string campingSpot;
        private string spotsTaken;
        private string areaLetter;

        private bool connectionOpen;
        private MySqlConnection connection;

        //properties
        public long VisitorNo
        {
            get { return visitorNo; }
        }
        public string TicketType
        {
            get { return ticketType; }
        }

        //constructor
        public Visitor(string firstName, string lastName, string governmentId, string email, string dob, string password, string ticketDates, string campingSpot, string spotsTaken, string areaLetter)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.governmentId = governmentId;
            this.email = email;
            DateTime dobConversion = DateTime.ParseExact(dob, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            this.dob = dobConversion.ToString("yyyy-MM-dd HH:mm:ss");
            this.password = password;
            this.ticketDates = ticketDates;
            ticketType = Ticket.GetTicketType(ticketDates);
            this.campingSpot = campingSpot;
            this.spotsTaken = spotsTaken;
            this.areaLetter = areaLetter;
        }

        //methods
        private void GetConnection()
        {
            connectionOpen = false;
            connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
            if (OpenLocalConnection())
            {
                connectionOpen = true;
            }
            else
            {
                throw new Exception("Connection cannot be opened");
            }
        }

        private bool OpenLocalConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool AlreadyExistInDB(out string message)
        {
            GetConnection();
            string sql = "SELECT governmentId, email " +
                         "FROM event_account " +
                        $"WHERE governmentId = {governmentId} " +
                        $"OR email = '{email}';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                message = "";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == governmentId)
                    {
                        message += "Government id already in use.";
                    }
                    if (reader[1].ToString() == email)
                    {
                        message += "Email already in use.";
                    }
                }
                if (message == "")
                    return false;
                else
                    return true;
            }
            catch (MySqlException)
            {
                message = "Error!";
                return true;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool InsertIntoDB()
        {
            //if user doesn't exist in DB
            if (!AlreadyExistInDB(out string message))
            {
                if (Ticket.TicketAvailable(ticketType, out string ticketErrorMessage))
                {
                    GetConnection();
                    MySqlCommand myCommand = connection.CreateCommand();
                    MySqlTransaction myTrans;

                    // Start a local transaction
                    myTrans = connection.BeginTransaction();

                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction

                    myCommand.Connection = connection;
                    myCommand.Transaction = myTrans;

                    try
                    {
                        myCommand.CommandText = $"INSERT INTO event_ticket (visitorNo, type, bought_datetime, spotNo) VALUES (DEFAULT,'{ticketType}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', {campingSpot})";
                        myCommand.ExecuteNonQuery();
                        long lastId = myCommand.LastInsertedId;
                        visitorNo = lastId;

                        myCommand.CommandText = $"INSERT INTO event_account (visitorNo, governmentId, first_name, last_name, email, dob, password) VALUES ({lastId}, {governmentId}, '{firstName}', '{lastName}', '{email}', '{dob}', '{password}')";
                        myCommand.ExecuteNonQuery();
                        myTrans.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        myTrans.Rollback();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    message = ticketErrorMessage;
                    return false;
                }
            }
            else
            {
                throw new Exception($"{message}");
            }
        }

        public bool InsertIntoDB(out string message)
        {
            message = "";
            //if user doesn't exist in DB
            if (!AlreadyExistInDB(out string messageError))
            {
                if (Ticket.TicketAvailable(ticketType, out string ticketErrorMessage))
                {
                    GetConnection();
                    MySqlCommand myCommand = connection.CreateCommand();
                    MySqlTransaction myTrans;

                    // Start a local transaction
                    myTrans = connection.BeginTransaction();

                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction

                    myCommand.Connection = connection;
                    myCommand.Transaction = myTrans;

                    try
                    {
                        if (areaLetter != "" && campingSpot != "")
                        {
                            myCommand.CommandText = $"INSERT INTO event_ticket (visitorNo, type, bought_datetime, area_letter, spotNo) VALUES " +
                                                    $"(DEFAULT,'{ticketType}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'" + ", '" + areaLetter + "', " + campingSpot + ")";
                        }
                        else
                        {
                            myCommand.CommandText = $"INSERT INTO event_ticket (visitorNo, type, bought_datetime) VALUES " +
                                                    $"(DEFAULT,'{ticketType}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
                        }
                        myCommand.ExecuteNonQuery();
                        long lastId = myCommand.LastInsertedId;
                        visitorNo = lastId;

                        myCommand.CommandText = $"INSERT INTO event_account (visitorNo, governmentId, first_name, last_name, email, dob, password) VALUES ({lastId}, {governmentId}, '{firstName}', '{lastName}', '{email}', '{dob}', '{password}')";
                        myCommand.ExecuteNonQuery();

                        if (areaLetter != "" && campingSpot != "")
                        {
                            myCommand.CommandText = $"UPDATE camping_spot SET spots_taken={spotsTaken} WHERE spotNo={campingSpot} AND area_letter='{areaLetter}'";
                            myCommand.ExecuteNonQuery();
                        }
                        if (Ticket.ReduceTicketQuantityBy1(ticketType))
                        {
                            myTrans.Commit();
                            return true;
                        }
                        else
                        {
                            myTrans.Rollback();
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        myTrans.Rollback();
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    message = ticketErrorMessage;
                    return false;
                }
            }
            else
            {
                message = messageError;
                return false;
            }
        }
    }
}