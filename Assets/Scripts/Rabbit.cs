using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Rabbit : MonoPooled
{
    public Transform areaTransform;
    public float size;
    public NavMeshAgent rabbitAgent;
    public Transform attachPoint; // Точка для прикрепления подобранной еды
    
    private Collectable _collectable;
    private IStrategy _moveAreaStrategy;
    private IStrategy _findFoodStrategy;
    private IStrategy _currentStrategy;
    private float _timeSinceLastFoodSearch = 0f;
    private const float FOOD_SEARCH_INTERVAL = 20f;

    [Inject] private RabbitConfig config;
    [Inject] private IDamageable _damageable;
    [Inject] private DiContainer _container;
     
    [Inject]
    private PickUpMechanics _pickUpMechanics;

    private void Start()
    {
        rabbitAgent.speed = config.speed;
        _collectable = GetComponent<Collectable>();
        _collectable.SetCanCollect(true);
        _moveAreaStrategy = new MoveAreaStrategy(rabbitAgent, areaTransform.transform, size);
        //_moveAreaStrategy = _container.Instantiate<MoveAreaStrategy>(new Object[] { rabbitAgent, areaTransform.transform, size });
        StartStrategy(_moveAreaStrategy);
    }

    private void Update()
    {
        _timeSinceLastFoodSearch += Time.deltaTime;
        
        // Каждые 20 секунд начинаем искать ягоду
        if (_timeSinceLastFoodSearch >= FOOD_SEARCH_INTERVAL)
        {
            _timeSinceLastFoodSearch = 0f;
            var findFoodStrategy = new FindFoodStrategy(rabbitAgent, areaTransform, size, OnFoodReached);
            StartStrategy(findFoodStrategy);
        }
        
        if (_currentStrategy != null)
        {
            _currentStrategy.Tick();
        }
    }
    
    private void OnFoodReached()
    {
        // Переход в стратегию поедания ягоды
        var pickUpAndEatStrategy = new PickUpAndEatStrategy(rabbitAgent, size, OnEatFinished, _pickUpMechanics, attachPoint);
        StartStrategy(pickUpAndEatStrategy);
    }

    private void OnEatFinished()
    {
        // Возвращаемся к стратегии движения по области
        StartStrategy(_moveAreaStrategy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(areaTransform.position, new Vector3(size, 1, size));
    }

    private void StartStrategy(IStrategy strategy)
    {
        if (_currentStrategy != null)
        {
            _currentStrategy.EndStrategy();
        }
        _currentStrategy = strategy;
        _currentStrategy.StartStrategy();
    }

    public void TakeDamage(int damage)
    {
        _damageable.TakeDamage(damage);
    }
}