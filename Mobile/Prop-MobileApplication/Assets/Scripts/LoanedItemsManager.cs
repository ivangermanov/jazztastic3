using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoanedItemsManager : MonoBehaviour {

    MySqlConnection Connection;
    bool openconnection;
    public RectTransform loaned_Prefab;
    public Transform list;
    // Update is called once per frame

    public void goToHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }
    void Start()
    {
        
        StartConnection();
        string query = "SELECT standNo,return_datetime,quantity,name FROM `account_buy_loan` JOIN item ON item.itemNo=account_buy_loan.itemNo WHERE item_type='LOAN' AND type='loan'";

        if (openconnection)
        {
            Debug.Log("connected");
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = Connection;

            RectTransform g;

            //Execute query
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                g = Instantiate(loaned_Prefab);
                g.transform.SetParent(list);
                g.transform.localScale = Vector3.one;
                (loaned_Prefab.GetChild(0).GetComponent("Text") as Text).text=("Stand no: "+reader[0].ToString() +" on "+ reader[1].ToString());
                (loaned_Prefab.GetChild(1).GetComponent("Text") as Text).text = (reader[2].ToString() +" "+ reader[3].ToString());

            }
            

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
}

