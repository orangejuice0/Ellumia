using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private GameObject TempHealth;
    [SerializeField] private GameObject CurrentHealth;
    [SerializeField] private GameObject MaxHealth;
    [SerializeField] private GameObject DamageInput;
    [SerializeField] private SqlHandler sqlhandler;
    private int temphealth, currenthealth, maxhealth, damageinput = 0, LocalPlayerID;

    private void Awake()
    {
        LocalPlayerID = sqlhandler.GetLocalPlayerID();
        ImportHealth();
    }

    private void ImportHealth()
    {
        temphealth = sqlhandler.SearchSqlInt("SELECT PlayerTempHealth FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        currenthealth = sqlhandler.SearchSqlInt("SELECT PlayerCurrentHealth FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        maxhealth = sqlhandler.SearchSqlInt("SELECT PlayerMaxHealth FROM BaseStats WHERE PlayerID =" + LocalPlayerID);
        UpdateUI();
    }

    public void OnDamageInputDeSelect(string damage)
    {
        try
        {
            damageinput=int.Parse(damage);
        }
        catch (Exception e)
        {
            sqlhandler.SendAlert("Damage/Healing must be an integer");
            damageinput = 0;
        }
    }

    public void DoDamage()
    {
        if (temphealth > damageinput)
        {
            temphealth -= damageinput;
        }
        else
        {
            temphealth -= damageinput;
            currenthealth += temphealth;
            temphealth=0;
        }
        UpdateUI();
        ExportHealthStats();
    }

    public void DoHealing()
    {
        if (currenthealth + damageinput >= maxhealth)
        {
            currenthealth = maxhealth;
        }
        else
        {
            currenthealth += damageinput;
        }
        UpdateUI();
        ExportHealthStats();
    }

    public void ChangeMaxHealth(string NewMaxHealth)
    {
        int tempmaxhealth = 0;
        try
        {
            //int.TryParse(NewMaxHealth, out tempmaxhealth);
            tempmaxhealth = int.Parse(NewMaxHealth);
            if (tempmaxhealth < currenthealth)
            {
                sqlhandler.SendAlert("Max health can't be lower than current health");
            }
            else
            {
                maxhealth = tempmaxhealth;

                ExportHealthStats();
                UpdateUI();
            }
        }
        catch (Exception e)
        {
            sqlhandler.SendAlert("Max health must be an integer");
        }
    }

    private void ExportHealthStats()
    {
        string query = ("UPDATE BaseStats SET PlayerTempHealth =" + temphealth + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
        query = ("UPDATE BaseStats SET PlayerCurrentHealth =" + currenthealth + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
        query = ("UPDATE BaseStats SET PlayerMaxHealth =" + maxhealth + " WHERE PlayerID = " + LocalPlayerID);
        sqlhandler.InputSql(query);
    }

    private void UpdateUI()
    {
        MaxHealth.GetComponentInChildren<TMP_InputField>().text = "Max Health: " + maxhealth;
        CurrentHealth.GetComponent<TMP_Text>().text = "Current Health: " + currenthealth;
        TempHealth.GetComponent<TMP_Text>().text = "Temp Health: " + temphealth;
    }
}
