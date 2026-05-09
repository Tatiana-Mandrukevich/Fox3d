using System;
using UnityEngine;

public class GrowthDueFoxStrategy : IStrategy
{
    private Wildberries _wildberries;
    private float _radius;
    
    public GrowthDueFoxStrategy(Wildberries wildberries, float radius)
    {
        _wildberries = wildberries;
        _radius = radius;
    }
    
    public void Tick()
    {
        // Проверка, есть ли лиса рядом
        var foundColliders = Physics.OverlapSphere(_wildberries.transform.position, _radius);
        foreach (var collider in foundColliders)
        {
            if (collider.CompareTag("Fox"))
            {
                _wildberries.Grow();
                break;
            }
        }
    }
    
    public void StartStrategy()
    {
        Debug.Log("Starting GrowthDueFoxStrategy");
    }

    public void EndStrategy()
    {
        Debug.Log("Ending GrowthDueFoxStrategy");
    }
}