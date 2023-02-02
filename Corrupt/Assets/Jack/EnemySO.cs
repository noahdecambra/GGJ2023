using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int enemyDamage;
    public int enemyHealth;
    public float enemySpeed;
    public float detectionRange;
    public float speedMultiplier;
    public float fireRate;
    public float rotSpeed;
    public float fireSpeed;

}