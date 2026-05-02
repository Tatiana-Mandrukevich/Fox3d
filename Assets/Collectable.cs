using System;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public event Action OnCollectedEvent;
    public event Action OnStartCollectedEvent;
    public bool IsCanCollect { get; private set; }

    public void Collect(Transform attachPoint)
    {
        OnStartCollectedEvent?.Invoke();
        IsCanCollect = false;
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        OnCollectedEvent?.Invoke();
    }
    
    public void SetCanCollect(bool value)
    {
        IsCanCollect = value;
    }
}