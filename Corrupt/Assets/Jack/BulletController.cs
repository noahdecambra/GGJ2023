using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage;

    private bool _canDamage = true;
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
            if (_canDamage)
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
                _canDamage = false;
            }
            
           
        }

        else if (other.gameObject.CompareTag("Enemy")) 
        {
            return;
        }
        Destroy(gameObject);
    }
}
