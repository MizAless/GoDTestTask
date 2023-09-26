using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private PlayerController playerController;

    private void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Take damage");
        currentHealth -= damageAmount;

        // �������� �� ������ ���������
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ��� ��� ������ ���������
        // ��������, ����� ������� �������� ������ � ���������� �������� ���������
        playerController.Die();
    }
}
