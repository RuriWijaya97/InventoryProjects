using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HungerPlayer : MonoBehaviour
{
    public GameObject DeadPanel;
    [Header ("Hunger Information")]
    public Slider hungerBarSlider;
    [Range(0, 100)] public float Hunger;
    [Range(0, 100)] public float maxHunger;
    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        Hunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBarSlider.maxValue = maxHunger;
        hungerBarSlider.value = Hunger;
        GetHunger();
    }

    void Dead()
    {
        isDead = true;
        DeadPanel.SetActive(true);
    }

    public void Consuming(float amount)
    {
        if (Hunger < maxHunger)
        {
            Hunger += amount;
        }
        else
        {
            Hunger = maxHunger;
        }
    }

    void GetHunger()
    {
        Hunger -= .5f * Time.deltaTime;
        if (Hunger <= 0)
        {
            if (isDead)
            {
                return;
            }
            Dead();    
        }
    }
}
