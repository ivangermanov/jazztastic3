using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
public class QRcodeManager : MonoBehaviour
{
    MySqlConnection Connection;
    bool openconnection;
    public RawImage place_holder_qrcode;
    public Text ticket, name, surname;
    private void Start()
    {
        retrieveQrCode();
    }
    private void retrieveQrCode()
    {
        connectToDb();
        if (openconnection)
        {
            executeQuery();
        }
        Connection.Close();
    }
    public void goToHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }
    private void connectToDb()
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
            Debug.Log("connected!");
        }
        catch (Exception e)
        {
            openconnection = false;
            Debug.Log("problem with connection!");
        }
    }
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        QRCodeWriter t = new QRCodeWriter();
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private void executeQuery()
    {
        string query = "SELECT governmentId,first_name,governmentId,last_name FROM event_account WHERE visitorNo="+GlobalVariables.idVisitor;

        //Open connection
        if (this.openconnection == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = Connection;

            //Execute query
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string holder_qrCode = reader[0].ToString();
            Debug.Log(holder_qrCode);
            name.text = "Name: "+reader[1].ToString();
            ticket.text = "Ticket Id: "+reader[2].ToString();
            surname.text = "Surname: "+reader[3].ToString();
            Texture2D myQR = generateQR(holder_qrCode);
            place_holder_qrcode.texture = myQR;
        }
    }
	
}
