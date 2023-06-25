using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountsButtons : MonoBehaviour
{
    [SerializeField]private SqlHandler Sqlhandler;
    [SerializeField]private GameObject PlayerNameInput;
    
    private string PlayerName=" ";

    public void OnPlayerNameInputChange(string Input){
        PlayerName=Input;
    }

    public void OnNewAccountButton(){
        if (PlayerName!=" "){
            Sqlhandler.NewAccount(PlayerName);
        }
        else{
            Sqlhandler.SendAlert("Name cant be empty");
        }
    }

    public void OnLogin(){
                if (PlayerName!=" "){
            Sqlhandler.login(PlayerName);
        }
        else{
            Sqlhandler.SendAlert("Name cant be empty");
        }
    }
}
