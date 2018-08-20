using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class bank_manager : MonoBehaviour {

    // Use this for initialization
    MySqlConnection Connection;
    private bool openconnection;
    public RectTransform containerList;
    public Image prefab_item;
    public RectTransform moreDetails;
    public RectTransform prefab_moredetailsItem;
    public RectTransform list_container;
    public Text text_current_status;
    public Text Date, Amount_paid;
    public void populateModeDetailsForm(int clickedReceipt,string totalcost,string datePaid)
    {
        Debug.Log(clickedReceipt.ToString());
        foreach (Transform child in list_container)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartConnection();
        string query = "SELECT name,quantity,price FROM `account_buy_loan` JOIN item ON account_buy_loan.itemNo=item.itemNo GROUP BY receiptNo HAVING receiptNo=" + clickedReceipt;
        Debug.Log(query);
        if (openconnection)
        {
            Debug.Log("connected");
            moreDetails.gameObject.active = true;
            RectTransform t = Instantiate(prefab_moredetailsItem);
            t.SetParent(list_container);
            t.transform.localScale = Vector3.one;
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = Connection;

            //Execute query
            Date.text = datePaid;
            Amount_paid.text = totalcost;
            MySqlDataReader reader = cmd.ExecuteReader();
           
            while (reader.Read())
            { 
                (t.GetChild(0).GetComponent("Text") as Text).text = reader[1].ToString()+" "+reader[0].ToString();
                (t.GetChild(1).GetComponent("Text") as Text).text = reader[2].ToString();

            }

        }
        else
        {
            Debug.Log("no db connection");
        }
        Connection.Close();
    }
    private void instantiatNewItemInList(int id,string datetime, string details, string price)
    {
        Image g=Instantiate(prefab_item);
        g.rectTransform.SetParent(containerList);
        g.rectTransform.localScale = Vector3.one;
        string[] splittedDate = datetime.Split(' ');
        Text t = g.transform.GetChild(0).GetComponent("Text") as Text;
        t.text = splittedDate[0].Split('/')[2];
        t = g.transform.GetChild(1).GetComponent("Text") as Text;
        t.text = splittedDate[1];
        t = g.transform.GetChild(2).GetComponent("Text") as Text;
        t.text = splittedDate[0].Split('/')[0]+"/"+splittedDate[0].Split('/')[1];
        t = g.transform.GetChild(3).GetComponent("Text") as Text;
        t.text = details;
        t = g.transform.GetChild(4).GetComponent("Text") as Text;
        t.text = price+"€";
        (g.GetComponent("Button") as Button).onClick.AddListener(() => { populateModeDetailsForm(id,datetime,price); });


        g.enabled = true;

        Vector2 sizeDelta = list_container.sizeDelta;

        list_container.sizeDelta = new Vector2(list_container.sizeDelta.x, sizeDelta.y + 0.5f);
    }
    void Start ()
    {

        StartConnection();
        string query = "SELECT " +
            "visitorNo," +
            "item.itemNo," +
            "item_type," +
            "standNo," +
            "account_buy_loan.purchase_datetime," +
            "receipt.receiptNo," +
            "price," +
            "quantity," +
            "(price*quantity)," +
            "name " +
            "FROM account_buy_loan JOIN receipt  ON account_buy_loan.receiptNo=receipt.receiptNo JOIN item ON item.type=account_buy_loan.item_type AND item.itemNo=account_buy_loan.itemNo WHERE visitorNo="+GlobalVariables.idVisitor+" GROUP BY account_buy_loan.receiptNo";

        if (openconnection)
        {
            Debug.Log("connected");
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = Connection;

            //Execute query
            MySqlDataReader reader = cmd.ExecuteReader();
            string x = "";
            while (reader.Read())
            {
                Debug.Log(reader[4].ToString()+"------" + reader[6].ToString()+"------" + reader[8].ToString());
                instantiatNewItemInList(Convert.ToInt32(reader[5].ToString()),reader[4].ToString(), "Receipt nr: "+reader[5].ToString(), reader[6].ToString());

            }

        }
        else
        {
            Debug.Log("no db connection");
        }
        Connection.Close();

    }
    public void goHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
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
    public RectTransform details_container_global;
    public void closeDetails()
    {
        details_container_global.gameObject.active = false;

    }
}
