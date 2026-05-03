using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildberries : MonoPooled
{
    public float growingDelay;
    
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
}