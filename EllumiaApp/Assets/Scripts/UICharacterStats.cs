using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICharacterStats : MonoBehaviour
{
    [SerializeField] private SqlHandler sqlhandler;
    [SerializeField] private GameObject PlayerName;
    [SerializeField] private GameObject Level;
    [SerializeField] private GameObject PlayerRace;
    [SerializeField] private GameObject PlayerBG;
    private int LocalPlayerID, level;
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
}
