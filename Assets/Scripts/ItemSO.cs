using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemSO", menuName = "")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public float amountToChangeStat;

    public bool UseItem()
    {
        if(statToChange == StatToChange.hunger)
        {
            HungerPlayer hungerPlayer = GameObject.Find("Player").GetComponent<HungerPlayer>();
            if (hungerPlayer.Hunger >= hungerPlayer.maxHunger)
            {
                return false;
            }
            else
            {
                hungerPlayer.Consuming(amountToChangeStat);
                return true;    
            }
            
        }
        return false;
    }


    public enum StatToChange
    {
        none,
        hunger
    };

    public enum AttributeToChange
    {
        none,
        stuffed,
    };
}
