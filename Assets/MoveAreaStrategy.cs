using UnityEngine;
using UnityEngine.AI;

public class MoveAreaStrategy : IStrategy
{
    private NavMeshAgent _movableAgent;
    private Transform _moveTarget;
    private float _radius;
    private Vector3 _currentPosition;
    
    public MoveAreaStrategy(NavMeshAgent movableAgent, Transform moveTarget, float radius)
    {
        _movableAgent = movableAgent;
        _moveTarget = moveTarget;
        _radius = radius;
    }
    
    public void Tick()
    {
        if (Vector3.Distance(_currentPosition, _movableAgent.transform.position) < 2)
        {
            _currentPosition = _moveTarget.position + new Vector3(Random.Range(-_radius/2f, _radius/2f), _movableAgent.transform.position.y, 
                Random.Range(-_radius/2f, _radius/2f));
        }
        _movableAgent.SetDestination(_currentPosition);
    }
    
    public void StartStrategy()
    {
        _currentPosition = _moveTarget.position + new Vector3(Random.Range(-_radius/2f, _radius/2f), _movableAgent.transform.position.y, 
            Random.Range(-_radius/2f, _radius/2f));
        _movableAgent.enabled = true;
    }

    public void EndStrategy()
    {
        _movableAgent.enabled = false;
    }
}