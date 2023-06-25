using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBaseStats : MonoBehaviour
{
    [SerializeField] private SqlHandler sqlhandler;
    [SerializeField] private GameObject[] Stats;
    private int LocalPlayerID;
    private int[] stats = new int[6];

    private void Awake()
    {
        LocalPlayerID = sqlhandler.GetLocalPlayerID();
        ImportBaseStats();
    }

    private void ImportBaseStats()
    {
        //Cha
        stats[0] = sqlhandler.SearchSqlInt("SELECT Cha FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[0].GetComponentInChildren<TMP_Text>().text = ("" + stats[0]);
        //Con
        stats[1] = sqlhandler.SearchSqlInt("SELECT Con FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[1].GetComponentInChildren<TMP_Text>().text = ("" + stats[1]);
        //Dex
        stats[2] = sqlhandler.SearchSqlInt("SELECT Dex FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[2].GetComponentInChildren<TMP_Text>().text = ("" + stats[2]);
        //Int
        stats[3] = sqlhandler.SearchSqlInt("SELECT Int FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[3].GetComponentInChildren<TMP_Text>().text = ("" + stats[3]);
        //Str
        stats[4] = sqlhandler.SearchSqlInt("SELECT Str FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[4].GetComponentInChildren<TMP_Text>().text = ("" + stats[4]);
        //Wis
        stats[5] = sqlhandler.SearchSqlInt("SELECT Wis FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        Stats[5].GetComponentInChildren<TMP_Text>().text = ("" + stats[5]);
    }

    public void UpdateCha(string Stat)
    {
        string query = ("UPDATE BaseStats SET Cha =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    public void UpdateCon(string Stat)
    {
        string query = ("UPDATE BaseStats SET Con =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    public void UpdateDex(string Stat)
    {
        string query = ("UPDATE BaseStats SET Dex =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    public void UpdateInt(string Stat)
    {
        string query = ("UPDATE BaseStats SET Int =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    public void UpdateStr(string Stat)
    {
        string query = ("UPDATE BaseStats SET Str =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    public void UpdateWis(string Stat)
    {
        string query = ("UPDATE BaseStats SET Wis =" + Stat + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }
}
