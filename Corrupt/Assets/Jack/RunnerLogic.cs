using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerLogic : EnemyBase
{


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        if (other.gameObject == FindPlayer(_detectRange).gameObject)
        {

            Debug.Log("Collided with a Player");
            //currentState = EnemyState.Attack;
            Attack();
            Die();
        }
    }
    public override void Attack()
    {
        Debug.Log("Reached Attack Function");
        var target = FindPlayer(_detectRange);
        try
        {
            Debug.Log("Trying Attack");
            var _targetScript = target.gameObject.GetComponent<PlayerBase>(); //PlayerBase would be whatever the script controlling player health is
            _targetScript.TakeDamage(damage); //calls function to lower player health
        }
        catch (Exception e)
        {
            Debug.Log("No Player Script found on target");
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
