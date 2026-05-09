using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PickUpMechanics : MonoBehaviour
{
    public Transform searchCenter;
    public Transform attachPoint;
    public float radius;
    public Transform GetCollectableItem => _collectableItem;

    [Inject]
    private InputSystem InputSystem;
    private Transform _collectableItem;

    private void OnDrawGizmosSelected()
    {
        if(searchCenter == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(searchCenter.position, radius);
    }

    private void Update()
    {
        if (InputSystem.IsSelectableButtonClicked && _collectableItem == null)
        {
            Collect();
        }
    }

    public void Collect()
    {
        Collider[] colliders = Physics.OverlapSphere(searchCenter.position, radius);
        foreach (Collider collider in colliders)
        {
            ICollectable collectable = collider.GetComponent<ICollectable>();
            if (collectable != null && collectable.IsCanCollect)
            {
                _collectableItem = collider.transform;
                collectable.Collect(attachPoint);
                return;
            }
        }
    }

    public void DeCollect()
    {
        _collectableItem = null;
    }
}