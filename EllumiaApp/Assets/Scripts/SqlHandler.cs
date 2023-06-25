using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using System;

public class SqlHandler : MonoBehaviour
{
    private string dbName = "URI=file:information.db";
    private int LocalPlayerID;
    private float AlertTimerMaxTime = 15f;
    private float AlertTimerCurrentTime = 0f;
    private bool CountDownAlertTimer = false;
    private bool RanAlertAnimation = false;

    [SerializeField] private GameObject AlertObject;
    [SerializeField]private UIHandler UIhandler;

    void Update()
    {
        if (CountDownAlertTimer)
        {
            if (AlertTimerCurrentTime > 0)
            {
                if (AlertTimerCurrentTime < 1 && !RanAlertAnimation)
                {
                    AlertObject.transform.LeanMoveLocalX(700, 1).setEaseOutQuart();
                    RanAlertAnimation = true;
                }
                AlertTimerCurrentTime -= Time.deltaTime;
            }
            else
            {
                CountDownAlertTimer = false;
                RanAlertAnimation = false;
                AlertObject.SetActive(false);
            }
        }

    }

    public void NewAccount(string PlayerName)
    {
        int pid = getnextplayerid();
        LocalPlayerID = pid + 1;
        string query = "INSERT INTO BaseStats (PlayerID,PlayerName) VALUES(" + LocalPlayerID + ",'" + PlayerName + "')";
        InputSql(query);
        UIhandler.LoggedOn();
    }

    private int getnextplayerid()
    {
        string query = "SELECT PlayerID FROM BaseStats ORDER BY PlayerID DESC";
        int nid = SearchSqlInt(query);
        return nid;
    }

    public void login(string PlayerName)
    {
        string query = "SELECT PlayerID FROM BaseStats WHERE PlayerName='" + PlayerName + "'";
        try
        {
            LocalPlayerID = SearchSqlInt(query);
        }
        catch (Exception e)
        {
            SendAlert("Can't fing Player: " + PlayerName);
        }
        if(LocalPlayerID!=0){
            UIhandler.LoggedOn();
        }
    }

    private void InputSql(string ExecutingString)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = ExecutingString;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    private int SearchSqlInt(string ExecutingString)
    {
        int output = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = ExecutingString;
                using (IDataReader reader = command.ExecuteReader())
                {
                    output = (int)(reader[0]);
                }
            }
            connection.Close();
        }
        return output;
    }

    public void SendAlert(string alertmessage)
    {
        if (!AlertObject.activeSelf)
        {
            AlertObject.SetActive(true);
            AlertObject.transform.LeanMoveLocalX(350, 1).setEaseOutQuart();
            AlertObject.GetComponentInChildren<TMP_Text>().text = alertmessage;
            AlertTimerCurrentTime = AlertTimerMaxTime;
            CountDownAlertTimer = true;
        }
        else
        {
            AlertObject.GetComponentInChildren<TMP_Text>().text = alertmessage;
            AlertTimerCurrentTime = AlertTimerMaxTime;
            CountDownAlertTimer = true;
        }
    }
}
