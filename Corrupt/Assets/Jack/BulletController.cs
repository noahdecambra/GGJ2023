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
            other.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
            
        }

        if (other.gameObject.CompareTag("LaunchPoint")) //need to change once models are implemented- bandaid fix for testing
        {
            return;
        }
        Destroy(gameObject);
    }
}
