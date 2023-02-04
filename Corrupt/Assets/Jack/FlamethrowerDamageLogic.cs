using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlamethrowerDamageLogic : MonoBehaviour
{
    public float damage = .00001f;
    private float _damageTally = 0;
    private float _damageTallyMax = 100;
    private int _sentDamage;
    public ParticleSystem part;
    private Transform _tip;
    
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        _tip = part.transform.parent;
        part.transform.parent = null;
        part.transform.localScale = new Vector3(1, 1, 1);
        
       
    }

    void Update()
    {
        part.transform.position = _tip.position;
        part.transform.rotation = _tip.rotation;
        
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
