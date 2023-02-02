using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryProjectileLogic : EnemyBase
{

    private Transform _target;

    [Header("Shooting")] 
    [SerializeField] private float _fireRate=1f;
    private float _fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float _range = 15f;
    private float _shotPower = 100;


    [Header("Rotation")] 
    public Transform rotPoint;
    private Quaternion _startRot;
    [SerializeField] private float _rotSpeed = 10f;


    void Awake()
    {
        if (stats.detectionRange>0)
        {
            _range = stats.detectionRange;
        }

        if (stats.fireRate>0)
        {
            _fireRate = stats.fireRate;
        }
        if (stats.rotSpeed > 0)
        {
            _rotSpeed = stats.rotSpeed;
        }

        if (stats.fireSpeed > 0)
        {
            _shotPower = stats.fireSpeed;
        }

        _startRot = rotPoint.rotation;
      

    }
    // Update is called once per frame
    void Update()
    {
        _target = FindPlayer(_detectRange);
        if (_target != null)
        {
            AlertAllies(_detectRange);
            if (_fireCountdown<=0f)
            {
                Attack();
                _fireCountdown = 1f / _fireRate;
            }

            _fireCountdown -= Time.deltaTime;
        }
        RotationController();
    }

    void RotationController()
    {
       if (_target == null && rotPoint.rotation != _startRot)
       {
           rotPoint.rotation = Quaternion.Lerp(rotPoint.rotation, _startRot, Time.deltaTime);
           return;
       }

       
       Vector3 dir = _target.position - transform.position;
       //dir.y = dir.y * -1;
       Quaternion lookRot = Quaternion.LookRotation(dir); //Y is inverted for some reason
       Vector3 rot = Quaternion.Lerp(rotPoint.rotation, lookRot, Time.deltaTime * _rotSpeed).eulerAngles;
       rotPoint.rotation = Quaternion.Euler(0f, rot.y, 0f);
       
       Debug.Log("target: "+_target.name + "\ndir: "+dir +"\nLookRot: "+lookRot);

    }

    public override void Attack()
    {
        GameObject bullet= (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*_shotPower);
        bullet.transform.parent = firePoint.transform;
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetDamage(damage);

        base.Attack();
    }
}
