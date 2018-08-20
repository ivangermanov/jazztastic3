using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class FeedBack_manager : MonoBehaviour {
    MySqlConnection Connection;
    private bool openconnection;
    public InputField comment_section;
    public static byte[] result_image_to_insert;
  
    public void writeFeedbackToDatabase()
    {
        StartConnection();

        if (openconnection)
        {
            // NULL, '"+comment_section.text+"', '', '"+GlobalVariables.idVisitor+"'

            string query = "INSERT INTO `feedback` (`feedbackNo`, `comment`, `image`, `visitorNo`) VALUES (NULL,@comm,@img,@visitId);";
        
                MySqlCommand command = new MySqlCommand(query, Connection);



                /*
                command.Parameters.Add("@comm", MySqlDbType.VarChar);
        
                command.Parameters.Add("@img", MySqlDbType.LongBlob);

                command.Parameters.Add("@visitId", MySqlDbType.VarChar);

            */
                command.Parameters.Add(new MySqlParameter("comm",comment_section.text));


                //command.Parameters["@comm"].Value = comment_section.text;

                if (result_image_to_insert != null)
                    command.Parameters.Add(new MySqlParameter("img", result_image_to_insert));
                else
                    command.Parameters.Add(new MySqlParameter("img", null));

                command.Parameters.Add(new MySqlParameter("visitId", GlobalVariables.idVisitor));

       
      
                    Debug.Log("connected");
                    //create mysql command
           
                    //Assign the connection using Connection
           
                    //Execute query
                    command.ExecuteNonQuery();
                    comment_section.text = "";
                    result_image_to_insert = null;
           

        }
        else
        {
            Debug.Log("no db connection");
        }
        Connection.Close();
    }
    private void StartConnection()
    {
        string server = "studmysql01.fhict.local";
        string database = "dbi397773";
        string uid = "dbi397773";
        string password = "eventimate";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";"+ "Allow User Variables=True";
        Connection = new MySqlConnection(connectionString);
        try
        {
            Connection.Open();
            openconnection = true;
        }
        catch (Exception e)
        {
            openconnection = false;
        }
    }

    public void go_back_home()
    {

        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

}
