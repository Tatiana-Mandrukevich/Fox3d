using System;
using UnityEngine;
using UnityEngine.AI;
 
public class PickUpAndEatStrategy : IStrategy
{ 
    private Transform _collectableItem;
    private NavMeshAgent _movableAgent;
    private float _radius;
    private Vector3 _currentPosition;
    private Collider _targetFood;
    private Wildberries _wildberries;
    private Action _onEatFinished;
    private PickUpMechanics _pickUpMechanics;
    private Transform _attachPoint;
    private float _eatTime = 3f; // Время поедания
    private float _currentTime = 0f;
    private bool _isReducing; // Флаг, указывающий, уменьшается ли ягода

    public PickUpAndEatStrategy(NavMeshAgent movableAgent, float radius, Action onEatFinished, PickUpMechanics pickUpMechanics, Transform attachPoint)
    {
        _movableAgent = movableAgent;
        _radius = radius;
        _onEatFinished = onEatFinished;
        _pickUpMechanics = pickUpMechanics;
        _attachPoint = attachPoint;
    }
    
    public void Tick()
    {
        _currentTime += Time.deltaTime;
        
        // Продолжаем уменьшать ягоду во время поедания
        if (_isReducing && _collectableItem != null)
        {
            // Переполучаем компонент на случай переиспользования из пула
            Wildberries wildberries = _collectableItem.GetComponent<Wildberries>();
            if (wildberries != null && wildberries.gameObject.activeSelf)
            {
                wildberries.Reduction();
            }
            else
            {
                // Ягода была уничтожена или деактивирована
                _isReducing = false;
            }
        }
        
        if (_currentTime >= _eatTime)
        {
            _onEatFinished?.Invoke();
        }
    }
    
    public void StartStrategy()
    {
        // Взятие ягоды
        var foundColliders = Physics.OverlapSphere(_movableAgent.transform.position, _radius);
        _targetFood = null;
        _isReducing = false;
        _wildberries = null;
        
        // Найдем ближайшую ягоду
        Collider closestCollider = null;
        float closestDistance = float.MaxValue;
        
        foreach (var foundCollider in foundColliders)
        {
            if (foundCollider.CompareTag("Wildberry"))
            {
                ICollectable collectable = foundCollider.GetComponent<ICollectable>();
                if (collectable != null && collectable.IsCanCollect)
                {
                    float distance = Vector3.Distance(_movableAgent.transform.position, foundCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = foundCollider;
                    }
                }
            }
        }
        
        // Подбираем ближайшую найденную ягоду
        if (closestCollider != null)
        {
            ICollectable collectable = closestCollider.GetComponent<ICollectable>();
            _collectableItem = closestCollider.transform;
            // Используем attachPoint кролика, если он установлен, иначе используем позицию кролика
            Transform targetAttachPoint = _attachPoint != null ? _attachPoint : _movableAgent.transform;
            collectable.Collect(targetAttachPoint);
            Debug.Log("Picked up food: " + closestCollider.name);
            
            // Уменьшение и исчезание ягоды
            _wildberries = closestCollider.GetComponent<Wildberries>();
            if (_wildberries != null)
            {
                _wildberries.StartReduction(); // Сбрасываем счетчик перед началом уменьшения
                _isReducing = true;
                Debug.Log("Reducing food: " + closestCollider.name);
            }
        }
        
        _currentTime = 0f;
        _movableAgent.enabled = false; // Останавливаемся для поедания
        Debug.Log("Starting PickUpAndEatStrategy");
    }

    public void EndStrategy()
    {
        _isReducing = false;
        _wildberries = null;
        Debug.Log("Ending PickUpAndEatStrategy");
        _movableAgent.enabled = false;
    }
}