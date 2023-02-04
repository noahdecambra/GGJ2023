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
        Destroy(gameObject,1);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
            
        }

        else if (other.gameObject.CompareTag("Enemy")) 
        {
            return;
        }
        Destroy(gameObject);
    }
}
