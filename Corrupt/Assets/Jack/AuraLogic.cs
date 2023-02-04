using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AuraLogic : EnemyBase
{
    [SerializeField] private float _fireRate = 1f;
    private float _fireCountdown = 0f;
    private float _range = 15f;

    private Transform _target;
    // Start is called before the first frame update
    void Awake()
    {
        if (stats.detectionRange > 0)
        {
            _range = stats.detectionRange/2;
        }

        if (stats.fireRate > 0)
        {
            _fireRate = stats.fireRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Die:
                Die();
                break;
        }

        _target = FindPlayer(_detectRange);
        if (_target != null)
        {
            Debug.Log(_target.name);
            if (Physics.CheckSphere(gameObject.transform.position, _range, player))
            {
                Debug.Log("Players in range = true");
                if (_fireCountdown <= 0f)
                {
                    Attack();
                    _fireCountdown = 1f / _fireRate;
                }
            }
            AlertAllies(_detectRange);
            

            _fireCountdown -= Time.deltaTime;
        }
    }

    public override void Attack()
    {
        var targetList = Physics.OverlapSphere(gameObject.transform.position, _range, player);
        if (targetList.Length>0)
        {
            foreach (var target in targetList)
            {
                target.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }
       
        
        base.Attack();
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject, .25f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, _detectRange);
    }
}

