using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Health : IDamageable, IInitializable
{
    private float maxHealth = 100f;
    private float currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Rabbit dead");
        }
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
    }
}