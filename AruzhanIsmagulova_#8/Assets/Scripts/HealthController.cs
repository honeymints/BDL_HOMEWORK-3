using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Image Image;

    [SerializeField] private float _fullHP=100;
    [SerializeField] private float _currentHP;
    [SerializeField] private float _speedOfFiling;
    private float currentHp;


    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _fullHP;
        
    }

    public void Hit(float damage)
    {
        if (_currentHP > 0)
            _currentHP -= damage;
        else
            _currentHP = 0;

    }

    public void Update()
    {
        
        Image.fillAmount= Mathf.Lerp(Image.fillAmount, _currentHP/_fullHP, Time.deltaTime*_speedOfFiling);
    }
}
