using UnityEngine;

public interface ICollectable
{
    bool IsCanCollect { get; }
    void Collect(Transform attachPoint);
}