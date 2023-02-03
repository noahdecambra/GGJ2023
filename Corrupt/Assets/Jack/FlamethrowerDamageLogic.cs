using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlamethrowerDamageLogic : MonoBehaviour
{
    public float damage = .00001f;
    private float _damageTally = 0;
    private float _damageTallyMax = 200;
    private int _sentDamage;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    // Start is called before the first frame update
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Fire!");
        if (other.CompareTag("Enemy"))
        {
            _damageTally += damage;
            Debug.Log(_damageTally);
            if (_damageTally >=_damageTallyMax)
            {
                _sentDamage = 1;
                _damageTally = 0;
                Debug.Log(_damageTally);
            }
            if (_sentDamage==1)
            {
                other.GetComponent<EnemyBase>().TakeDamage(_sentDamage);
                
                _sentDamage = 0;
            }
            
           
        }
    }
}
