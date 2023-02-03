using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerLogic : EnemyBase
{

    void Update()
    {
        if (!_foundPlayer)
        {
            currentState = EnemyState.Roam;
            Debug.Log("Time to roam");
            //Roam();
            //FindPlayer(_detectRange);
        }
        else
        {
            currentState = EnemyState.Pursue;
            //MoveToPlayer(_detectRange, _speedMultiplier);
        }

        switch (currentState)
        {
            case EnemyState.Attack:
                Debug.Log("Attack State");
                Attack();
                break;
            case EnemyState.Roam:
                Roam();
                FindPlayer(_detectRange);
                break;
            case EnemyState.Pursue:
                MoveToPlayer(_detectRange, _speedMultiplier);
                break;
            case EnemyState.Die:
                Die();
                break;
        }



    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        if (other.gameObject != FindPlayer(_detectRange).gameObject)
        {
            Debug.Log("Not a player");
            return;
           
        }
        
            Debug.Log("Collided with a Player");
            
            Attack();
            Die();
        
    }
    public override void Attack()
    {
        Debug.Log("Reached Attack Function");
        var target = FindPlayer(_detectRange);
        try
        {
            Debug.Log("Trying Attack");
            var _targetScript = target.gameObject.GetComponent<PlayerController>(); //PlayerBase would be whatever the script controlling player health is
            _targetScript.TakeDamage(damage); //calls function to lower player health
        }
        catch (Exception e)
        {
            Debug.Log("No Player Script found on target");
            //target.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
            throw;
        }
        base.Attack();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject,.25f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, _detectRange);
    }
}
