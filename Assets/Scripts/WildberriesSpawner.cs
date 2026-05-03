using UnityEngine;

public class WildberriesSpawner : MonoBehaviour
{
    public MonoPooled Prefab;
    public Transform[] SpawnPoints;
    
    IPool<MonoPooled> _pool;
    private MonoPooled[] _berries;
    private float _radius = 2f;

    private void Start()
    {
        FactoryMonoObject<MonoPooled> factory = new FactoryMonoObject<MonoPooled>(Prefab, transform);
        _pool = new Pool<MonoPooled>(factory);
        _berries = new MonoPooled[SpawnPoints.Length];
        
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            var berry = _pool.Pull();
            berry.transform.position = SpawnPoints[i].position;
            _berries[i] = berry;
        }
    }

    private void Update()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            // Проверка, есть ли активный berry на этом spawnPoint и нет ли лисы рядом
            if (!HasBerryAtSpawnPoint(SpawnPoints[i]) && !HasFoxAtSpawnPoint(SpawnPoints[i]))
            {
                // Создаем новый berry
                var berry = _pool.Pull();
                berry.transform.position = SpawnPoints[i].position;
                _berries[i] = berry;
            }
        }
    }
    
    private bool HasBerryAtSpawnPoint(Transform spawnPoint)
    {
        // Проверка, есть ли активный berry на позиции spawnPoint
        const float detectionRadius = 0.1f;
        
        foreach (var berry in _berries)
        {
            if (berry != null && berry.gameObject.activeSelf)
            {
                if (Vector3.Distance(berry.transform.position, spawnPoint.position) <= detectionRadius)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private bool HasFoxAtSpawnPoint(Transform spawnPoint)
    {
        var foundColliders = Physics.OverlapSphere(spawnPoint.position, _radius);
        foreach (var collider in foundColliders)
        {
            if (collider.CompareTag("Fox"))
            {
                return true;
            }
        }
        return false;
    }
}