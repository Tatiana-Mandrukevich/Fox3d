using UnityEngine;

public class RabbitDamageDealer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rabbit rabbit))
        {
            rabbit.TakeDamage(50);
        }
    }
}