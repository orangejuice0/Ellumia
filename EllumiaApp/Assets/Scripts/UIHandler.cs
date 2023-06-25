using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField]private GameObject AccountsPage;
    [SerializeField]private GameObject CharacterStatsPage;

    public void LoggedOn(){
        CharacterStatsPage.SetActive(true);
        AccountsPage.SetActive(false);
    }
}
