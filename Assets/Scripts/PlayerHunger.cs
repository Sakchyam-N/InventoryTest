using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHunger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currHunger ;
    private int currHungerStat = 10;
    [SerializeField] private string maxHunger = "100";
    
    public void ChangeHunger(int hungerAdd)
    {
        currHungerStat += hungerAdd;
        currHunger.text = "Hunger: "+ currHungerStat.ToString() +" / "+ maxHunger;
        

    }
}
