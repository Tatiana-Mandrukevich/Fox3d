using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildberries : MonoPooled
{
    private float growingDelay = 3;
    private float reductionDelay = 2;
    private Collectable _collectable;
    private float _currentTime;
    private IStrategy _growthDueFoxStrategy;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        _collectable = GetComponent<Collectable>();
        _collectable.SetCanCollect(false);
        _currentTime = transform.localScale.x * growingDelay;
        _growthDueFoxStrategy = new GrowthDueFoxStrategy(this, 2f);
    }

    private void Update()
    {
        _growthDueFoxStrategy.Tick();
    }

    public void Grow()
    {
        if (_currentTime >= growingDelay) return;
        
        _currentTime += Time.deltaTime;
        
        float scale = Mathf.Min(_currentTime / growingDelay, 1f);
        transform.localScale = new Vector3(scale, scale, scale);
        
        if (_currentTime >= growingDelay)
        {
            _collectable.SetCanCollect(true);
        }
    }

    public void Reduction()
    {
        if (_currentTime >= reductionDelay) return;

        _currentTime += Time.deltaTime;

        float scale = 1f - Mathf.Clamp01(_currentTime / reductionDelay);
        transform.localScale = new Vector3(scale, scale, scale);

        if (_currentTime >= reductionDelay)
        {
            _collectable.SetCanCollect(false);
            Destroy(gameObject); 
        }
    }

    public void StartReduction()
    {
        _currentTime = 0f; // Сбрасываем счетчик перед началом уменьшения
    }
}