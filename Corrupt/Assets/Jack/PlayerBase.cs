using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int health { private set; get; } = 100;

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;
        Debug.Log("Player Health: "+ health);
    }

}
