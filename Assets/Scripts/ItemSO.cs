using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChange;

    public void UseItem()
    {
        if(statToChange == StatToChange.hunger)
        {
            GameObject.Find("HungerManager").GetComponent<PlayerHunger>().ChangeHunger(amountToChange);
        }
    }

    public enum StatToChange{
        none,
        health,
        hunger,
        stamina
    };
}
