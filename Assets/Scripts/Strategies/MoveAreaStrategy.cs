using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MoveAreaStrategy : IStrategy
{
    private NavMeshAgent _movableAgent;
    private Transform _moveTarget;
    private float _radius;
    private Vector3 _currentPosition;
    [Inject] private RabbitConfig _config;
    
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
        //_movableAgent.speed = _config.speedInPatrol;
        _currentPosition = _moveTarget.position + new Vector3(Random.Range(-_radius/2f, _radius/2f), _movableAgent.transform.position.y, 
            Random.Range(-_radius/2f, _radius/2f));
        _movableAgent.enabled = true;
    }

    public void EndStrategy()
    {
        _movableAgent.enabled = false;
    }
}