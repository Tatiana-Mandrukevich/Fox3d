using UnityEngine;
using UnityEngine.AI;
using System;

public class FindFoodStrategy : IStrategy
{
    private NavMeshAgent _movableAgent;
    private Transform _areaTransform;
    private float _radius;
    private Vector3 _currentPosition;
    private Collider _targetFood;
    private Action _onFoodReached;
    private const float ArrivalDistance = 1.5f;
    
    public FindFoodStrategy(NavMeshAgent movableAgent, Transform areaTransform, float radius, Action onFoodReached)
    {
        _movableAgent = movableAgent;
        _areaTransform = areaTransform;
        _radius = radius;
        _onFoodReached = onFoodReached;
    }
    
    public void Tick()
    {
        // Если нашли ягоду и добрались до неё
        if (_targetFood != null && Vector3.Distance(_currentPosition, _movableAgent.transform.position) < ArrivalDistance)
        {
            _onFoodReached?.Invoke();
            return;
        }
        
        _movableAgent.SetDestination(_currentPosition);
    }
    
    public void StartStrategy()
    {
        // Ищем ягоду в радиусе
        Debug.Log("Starting FindFoodStrategy");
        var foundColliders = Physics.OverlapSphere(_movableAgent.transform.position, _radius);
        _targetFood = null;
        
        foreach (var foundCollider in foundColliders)
        {
            if (foundCollider.CompareTag("Food"))
            {
                Debug.Log("Found Food");
                _targetFood = foundCollider;
                _currentPosition = foundCollider.transform.position;
                break;
            }
        }
        
        // Если не нашли ягоду, ищем в области
        if (_targetFood == null)
        {
            Debug.Log("No Food");
            _currentPosition = _areaTransform.position + new Vector3(
                UnityEngine.Random.Range(-_radius/2f, _radius/2f), 
                _movableAgent.transform.position.y, 
                UnityEngine.Random.Range(-_radius/2f, _radius/2f)
            );
        }
        
        _movableAgent.enabled = true;
    }

    public void EndStrategy()
    {
        Debug.Log("Ending FindFoodStrategy");
        _movableAgent.enabled = false;
    }
}