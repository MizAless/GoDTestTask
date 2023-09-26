using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int damage = 30;
    public LayerMask enemyLayer;
    public Button shotButton;

    private InventoryScript inventory;
    private DataBase db;
    private Transform target;

    private void Start()
    {
        shotButton.onClick.AddListener(Shoot);
        inventory = FindObjectOfType<InventoryScript>();
        db = FindObjectOfType<DataBase>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    bool searchInventoryAmmo()
    {
        bool isFound = false;
        for (int i = 0; i < inventory.maxCount && !isFound; i++)
        {
            if (inventory.items[i].id == db.items[1].id)
            {
                isFound = true;
                inventory.items[i].count--;
                if (inventory.items[i].count < 1)
                {
                    inventory.RemoveItem(i);
                }
                inventory.UpdateInventory();
            }
        }
        return isFound;
    }

    void Shoot()
    {
        var enemy = FindNearestObjectWithTag();

        if (target != null)
        {
            if (searchInventoryAmmo())
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                Vector3 direction = (target.position - transform.position + new Vector3(0f, -0.5f, 0f)).normalized;
                var b = bullet.GetComponent<BulletScript>();
                b.Moveing(enemy, direction, bulletSpeed, damage);
            }
        }
    }

    void FindNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = collider.transform;
            }
        }

        target = closestEnemy;
    }

    private GameObject FindNearestObjectWithTag()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(obj.transform.position, currentPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestObject = obj;
            }
        }

        target = nearestObject.transform;
        return nearestObject;
    }
}
