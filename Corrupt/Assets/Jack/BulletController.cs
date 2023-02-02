using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage;
    // Start is called before the first frame update

    public void SetDamage(int val)
    {
        _damage = val;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBase>().TakeDamage(_damage);
            
        }

        if (other.gameObject.name == "Cube (1)") 
        {
            return;
        }
        Destroy(gameObject);
    }
}
