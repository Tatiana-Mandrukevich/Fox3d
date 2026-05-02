using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildberries : MonoBehaviour
{
    private Collectable _collectable;

    private void Start()
    {
        _collectable = GetComponent<Collectable>();
        _collectable.SetCanCollect(true);
    }
}