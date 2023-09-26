using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public List<GameObject> items;
    public int maxHealth;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject item = items[Random.Range(0, items.Count)];
        Instantiate(item, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Destroy(gameObject);

    }
}
