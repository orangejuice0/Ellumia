using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UICharacterStats : MonoBehaviour
{
    [SerializeField] private SqlHandler sqlhandler;
    [SerializeField] private GameObject PlayerName;
    [SerializeField] private GameObject Level;
    [SerializeField] private GameObject PlayerRace;
    [SerializeField] private GameObject PlayerBG;
    [SerializeField] private GameObject PlayerCP;
    [SerializeField] private GameObject PlayerSpeed;
    [SerializeField] private GameObject PlayerAC;
    private int LocalPlayerID, level, playercp, playerspeed, playerac;
    private string playername, playerrace, playerbg;

    private void Awake()
    {
        LocalPlayerID = sqlhandler.GetLocalPlayerID();
        ImportStats();
    }

    private void ImportStats()
    {
        playername = sqlhandler.SearchSqlString("SELECT PlayerName FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        PlayerName.GetComponent<TMP_Text>().text = playername;
        SetLevel();
        playerrace = sqlhandler.SearchSqlString("SELECT PlayerRace FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        if (playerrace == "")
        {
            PlayerRace.GetComponentInChildren<TMP_Text>().text = "Race:";
        }
        else
        {
            PlayerRace.GetComponentInChildren<TMP_Text>().text = playerrace;
        }

        playerbg = sqlhandler.SearchSqlString("SELECT PlayerBackground FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        if (playerbg == "")
        {
            PlayerBG.GetComponentInChildren<TMP_Text>().text = "Background:";
        }
        else
        {
            PlayerBG.GetComponentInChildren<TMP_Text>().text = playerbg;
        }
        playercp = sqlhandler.SearchSqlInt("SELECT PlayerCP FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        PlayerCP.GetComponentInChildren<TMP_InputField>().text = "" + playercp;
        playerspeed = sqlhandler.SearchSqlInt("SELECT PlayerSpeed FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        PlayerSpeed.GetComponentInChildren<TMP_InputField>().text = "" + playerspeed+"ft";
        playerac = sqlhandler.SearchSqlInt("SELECT PlayerAC FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        PlayerAC.GetComponentInChildren<TMP_InputField>().text = "" + playerac;
    }

    private void SetLevel()
    {
        level = sqlhandler.SearchSqlInt("SELECT PlayerLevel FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Level.GetComponent<TMP_Text>().text = ("Level: " + level);
    }

    public void UpdateRace(string Stat)
    {
        if (Stat != null)
        {
            string query = ("UPDATE BaseStats SET PlayerRace ='" + Stat + "' WHERE PlayerID = " + LocalPlayerID);
            sqlhandler.InputSql(query);
        }

    }

    public void UpdateBG(string Stat)
    {
        if (Stat != null)
        {
            string query = ("UPDATE BaseStats SET PlayerBackground ='" + Stat + "' WHERE PlayerID = " + LocalPlayerID);
            sqlhandler.InputSql(query);
        }
    }

    public void OnChangeCP(string input)
    {
        try
        {
            playercp = int.Parse(input);
            PlayerCP.GetComponentInChildren<TMP_InputField>().text = "" + playercp;
            string query = ("UPDATE BaseStats SET PlayerCP =" + playercp + " WHERE PlayerID = " + LocalPlayerID);
            sqlhandler.InputSql(query);
        }
        catch (Exception e)
        {
            sqlhandler.SendAlert("CP must be an integer");
        }
    }
    public void OnChangeSpeed(string input)
    {
        try
        {
            playerspeed = int.Parse(input);
            PlayerSpeed.GetComponentInChildren<TMP_InputField>().text = "" + playerspeed+"ft";
            string query = ("UPDATE BaseStats SET PlayerSpeed =" + playerspeed + " WHERE PlayerID = " + LocalPlayerID);
            sqlhandler.InputSql(query);
        }
        catch (Exception e)
        {
            sqlhandler.SendAlert("Speed must be an integer");
        }
    }
    public void OnChangeAC(string input)
    {
        try
        {
            playerac = int.Parse(input);
            PlayerAC.GetComponentInChildren<TMP_InputField>().text = "" + playerac;
            string query = ("UPDATE BaseStats SET PlayerAC =" + playerac + " WHERE PlayerID = " + LocalPlayerID);
            sqlhandler.InputSql(query);
        }
        catch (Exception e)
        {
            sqlhandler.SendAlert("AC must be an integer");
        }
    }
}
