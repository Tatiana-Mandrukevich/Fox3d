using UnityEngine;

public class Mushrooms : MonoPooled
{
    public float growingDelay;
    private Collectable _collectable;
    private bool _isGrowing;
    private float _currentTime;
    
    private void Start()
    {
        _collectable = GetComponent<Collectable>();
        _collectable.OnStartCollectedEvent += OnStartCollectedHandler;
    }

    private void OnStartCollectedHandler()
    {
        GameObject newMushroom = Instantiate(gameObject);
        newMushroom.transform.position = transform.position;
        newMushroom.transform.rotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToPool();
        }

        if (_isGrowing == false)
        {
            _currentTime += Time.deltaTime;
            transform.localScale = new Vector3(_currentTime / growingDelay, _currentTime / growingDelay, _currentTime / growingDelay);
            if (_currentTime >= growingDelay)
            {
                _isGrowing = true;
                _collectable.SetCanCollect(true);
            }
        }
    }
}