using UnityEngine;
using Zenject;

public class InteractMechanics : MonoBehaviour
{
    public Transform searchCenter;
    public float radius;
    
    [Inject]
    private InputSystem InputSystem;

    private void OnDrawGizmosSelected()
    {
        if(searchCenter == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(searchCenter.position, radius);
    }

    private void Update()
    {
        if (InputSystem.IsInteractButtonClicked)
        {
            FindInteractObjects();
        }
    }

    private void FindInteractObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(searchCenter.position, radius);
        foreach (Collider collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }
    }
}