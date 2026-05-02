using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public MonoPooled Prefab;
    
    IPool<MonoPooled> _pool;

    private void Start()
    {
        FactoryMonoObject<MonoPooled> factory = new FactoryMonoObject<MonoPooled>(Prefab, transform);
        _pool = new Pool<MonoPooled>(factory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < Random.Range(0, 3); i++)
            {
                _pool.Pull().transform.position += new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            }
        }
    }
}