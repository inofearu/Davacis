using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int health;
    [SerializeField] int maxMana;
    private int mana;
    public int accessMana
    {
        get{return mana;}
        set
        {
            if(value < 0)
            {value = 0;}
            if(value > maxMana)
            {value = maxMana;}
            mana = value;
        }
    }

    public int accessHealth
    {
        get{return health;}
        set
        {
            if(value < 0)
            {value = 0;}
            if(value > maxHealth)
            {value = maxHealth;}
            health = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mana = maxMana;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
