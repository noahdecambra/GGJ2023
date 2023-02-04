using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    private Slider _healthBar;

    private Image _fill;

    private int _maxHealth;

    private int _currentHealth;

    public string objectTag;
    

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = GetComponent<Slider>();
        var fillArea = gameObject.transform.GetChild(1);
        _fill = fillArea.GetChild(0).GetComponent<Image>();
        _maxHealth = GameObject.FindGameObjectWithTag(objectTag).GetComponent<PlayerController>().health;
        //Debug.Log(_maxHealth);
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _currentHealth = GameObject.FindGameObjectWithTag(objectTag).GetComponent<PlayerController>().health;
        _healthBar.value = _currentHealth;
    }

    
}
