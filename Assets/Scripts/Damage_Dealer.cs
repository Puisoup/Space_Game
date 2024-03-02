using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Dealer : MonoBehaviour
{
    [SerializeField] int damage = 10;


    // Gibt den Schadenwert für externen Zugriff zurück
    public int GetDamage()
    {
        return damage;
    }


    // Zerstört das GameObject, wenn aufgerufen. 
    public void Hit()
    {
        Destroy(gameObject);
    }
}
