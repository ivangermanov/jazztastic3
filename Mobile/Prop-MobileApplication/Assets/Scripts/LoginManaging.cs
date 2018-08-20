
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;

public class LoginManaging : MonoBehaviour
{
    public Animator erro_animator;
    public InputField id, passentry;
    private string server;
    private string database;
    private string uid;
    private string password;
    private bool openconnection;
    public Animator fading_out;
    private MySqlConnection Connection;
   

    public bool OpenConnection
    {
        get
        {
            return openconnection;
        }

        set
        {
            openconnection = value;
        }
    }

    private bool login(string email, string password_container)
    {
        string query = "SELECT COUNT(*), visitorNo FROM event_account WHERE email = '"+ email + "' AND password='"+password_container+"'";

        //Open connection
        if (this.OpenConnection == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = Connection;

            //Execute query
            MySqlDataReader reader= cmd.ExecuteReader();

            if (reader != null)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) > 0)
                {
                    GlobalVariables.idVisitor = Convert.ToInt32(reader[1].ToString());
                    return true;
                }
            }

            //close connection
            this.CloseConnection();

           
        }
        return false;
    }
    //Initialize values
    private void StartConnection()
    {
        server = "studmysql01.fhict.local";
        database = "dbi397773";
        uid = "dbi397773";
        password = "eventimate";
        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
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
    private void CloseConnection()
    {
        Connection.Close();
    }




   
    
    public void LogIN ()
    {
        StartConnection();
        //Debug.Log(id.text);
        if (login(id.text, passentry.text))
        {

            fading_out.gameObject.SetActive(true);
            fading_out.SetBool("fadeOut", true);


        }
        else
        {
            erro_animator.SetBool("GoOut", false);
            erro_animator.SetBool("GoIn", true);
        }

    }
    
	
}
