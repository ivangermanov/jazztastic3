/*
 * Author: Chinchila ( joaovitim61@gmail.com )
 * Created in base of other tutorial
 * Support for Android thanks to JSAR (juan_JSAR@hotmail.com)
 * you can use that for all your projects for free
 * if you want you can change that and post for more support and help others
 * enjoy that class !
 * Date created: 13/04/2013
 * Date the Android support: 16/07/2014
 * Plis dont remove the credits and thanks for use !
 * documentation at README HOT TO USE.txt (:
 */

using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using I18N;
using I18N.Other;
using I18N.West;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class MysqlClass : MonoBehaviour {
	private static MySqlConnection con = new MySqlConnection();
	private static MySqlCommand cmd = new MySqlCommand();
	private static MySqlDataReader rdr = null;
	
	public static string status(){
		return con.State.ToString();
	}
	
	public static string Connect(string server,string database,string userid,string password,bool pooling = true){
		string constr = "Server="+server+";Database="+database+";User ID="+userid+";Password="+password+";Pooling="+pooling;
		try{
           	con = new MySqlConnection(constr);
           	con.Open();
			return "Connection is: " + con.State;
       	}
       	catch (Exception ex){
       	    return ex.ToString();
       	}
	}
	public static void Connectv(string server,string database,string userid,string password,bool pooling = true){
		string constr = "Server="+server+";Database="+database+";User ID="+userid+";Password="+password+";Pooling="+pooling;
		try{
           	con = new MySqlConnection(constr);
           	con.Open();
			Debug.Log("Connection is: " + con.State);
       	}
       	catch (Exception ex){
       	    Debug.Log(ex.ToString());
       	}
	}
	public static bool Connectb(string server,string database,string userid,string password,bool pooling = true, bool debuglog = false){
		string constr = "Server="+server+";Database="+database+";User ID="+userid+";Password="+password+";Pooling="+pooling;
		try{
           	con = new MySqlConnection(constr);
           	con.Open();
			if(debuglog)
				Debug.Log ("Connection is: " + con.State);
			return true;
       	}
       	catch (Exception ex){
			if(debuglog)
				Debug.Log (ex.ToString());
       	    return false;
       	}
	}
	public static bool is_Connected(){
		if(con.State.ToString() == "Open"){
			return true;
		}else{
			return false;
		}
	}
	public static void Execute(string query){
        try{
            if (con.State.ToString() != "Open")
                con.Open();
            using (con){
                using (cmd = new MySqlCommand(query, con)){
                        cmd.ExecuteNonQuery();
            	}
            }
			Debug.Log("Executed query: " + query);
        }
        catch (Exception ex){
            Debug.Log(ex.ToString());
        }
	}
	public static string Read(string query,string row,string morequery = null){
		try{
            if (con.State.ToString() != "Open")
                con.Open();
            using(con){
                using(cmd = new MySqlCommand(query, con)){
                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
						while (rdr.Read())
                       		return rdr[row].ToString();
                    rdr.Dispose();
                }
            }
        }
        catch (Exception ex){
            return ex.ToString();
        }
		return "";
	}
	public static void Close(){
		if (con != null){
            if (con.State.ToString() != "Closed")
                con.Close();
            con.Dispose();
        }
	}
}