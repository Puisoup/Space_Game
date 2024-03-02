using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Dealer : MonoBehaviour
{
    [SerializeField] int damage = 10;


    // Gibt den Schadenwert f�r externen Zugriff zur�ck
    public int GetDamage()
    {
        return damage;
    }


    // Zerst�rt das GameObject, wenn aufgerufen. 
    public void Hit()
    {
        Destroy(gameObject);
    }
}
