using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyBase : MonoBehaviour
{
    public enum EnemyState
    {
        Roam, Attack, Pursue, Die
    }

    public EnemyState currentState;

    public EnemySO stats;
    protected NavMeshAgent _agent;
    public LayerMask player;
    public LayerMask ground;
    public LayerMask ally;

    public int health { private set; get; }
    public int damage { private set; get; }
    protected float _speedMultiplier;
    protected float _detectRange;

    protected bool _foundPlayer;
    protected bool _playerNear;
    protected bool _dead = false;
    protected Animator _anim;

    protected Vector3 _roamDestination;
    protected bool _hasPointToRoam ;
    protected float _roamRange = 5;
    void Start()
    {
        _foundPlayer = false;
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            _anim = gameObject.AddComponent<Animator>();
        }

        if (stats == null)
        {
            Destroy(gameObject);
            return;
        }
        _agent.speed = stats.enemySpeed;
        health = stats.enemyHealth;
        name = stats.enemyName;
        damage = stats.enemyDamage;
        _anim.SetInteger("Health", health);
        _detectRange = stats.detectionRange;
        _speedMultiplier = stats.speedMultiplier;
    }

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
                break;
        }



    }
    protected void MoveToPlayer(float detectionRange, float speedMultiplier = 1.0f, Transform currentTarget = null)
    {
        if (_dead != false)
        {
            return;
        }
        if (currentTarget==null)
        {
            //Roam();
            currentTarget = FindPlayer(detectionRange);
            
        }

        
        _agent.destination = currentTarget.position;
        _agent.speed = stats.enemySpeed * speedMultiplier;
    }

    protected Transform FindPlayer(float detectionRange)
    {
        Transform playerTarget = null;
        Collider[] players = Physics.OverlapSphere(gameObject.transform.position, detectionRange, player);
        if (players.Length > 0)
        {


            List<float> distances = new List<float>();
            foreach (var player in players)
            {
                float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
                distances.Add(distance);
            }

            float shortestDistance = detectionRange + 10;
            int tempIndex = 0;
            int index = 0;
            foreach (var distance in distances)
            {
                tempIndex++;
                if (distance > shortestDistance)
                {
                    shortestDistance = distance;
                    index = tempIndex;
                }
            }

            playerTarget = players[index].transform;

            if (playerTarget != null && shortestDistance > 0)
            {
                Debug.Log(playerTarget.name + "Target Set");
                _foundPlayer = true;
                return playerTarget;

            }
        }

        _foundPlayer = false;
        Debug.Log("Target not found");
        return null;
       
        
            
        
    }

    protected void FindRoamDestination()
    {
        float zDest = Random.Range(-_roamRange, _roamRange);
        float xDest = Random.Range(-_roamRange, _roamRange);

        _roamDestination =new Vector3(transform.position.x + xDest, transform.position.y,
            transform.position.z + zDest);
        _hasPointToRoam = Physics.Raycast(_roamDestination, -transform.up, ground);



    }
    protected void Roam()
    {
        _agent.speed = stats.enemySpeed;
            if (!_hasPointToRoam)
            {
                FindRoamDestination();
            }

            if (_hasPointToRoam)
            {
                _agent.destination = _roamDestination;

                var distanceRemaining = transform.position - _roamDestination;
                if (distanceRemaining.magnitude<1f)
                {
                    _hasPointToRoam = false;
                }
            }
    }

    public virtual void Attack()
    {
        Debug.Log("BaseAttack");
        //attack logic: Launching projectile, playing a melee animation, etc. 
    }

    public void Alerted(Transform targetPos)
    {
        try
        {
            MoveToPlayer(_detectRange, _speedMultiplier, targetPos);
        }
        catch (Exception e)
        {
            Debug.Log("Can't move but found player!");
        }
    }

    public virtual void Die()
    {
        _dead = true;
        //play death animation, destroy game object
    }

    public void TakeDamage(int damageDone)
    {
        health -= damageDone;
    }

    public void AlertAllies(float alertRange)
    {
        Collider[] allies = Physics.OverlapSphere(gameObject.transform.position, alertRange, ally);

        foreach (var ally in allies)
        {
            ally.SendMessage("Alerted",(FindPlayer(_detectRange)));
        }
    }

    
}

