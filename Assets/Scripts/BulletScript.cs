using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float bulletSpeed;
    private int damage;
    private Vector3 direction;
    private GameObject target;


    private void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void Moveing(GameObject target, Vector3 direction, float bulletSpeed, int damage)
    {
        this.direction = direction;
        this.bulletSpeed = bulletSpeed;
        this.damage = damage;
        this.target = target;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            var enemyObject = GameObject.FindGameObjectWithTag("Enemy");

            var zombieHealth =  target.GetComponent<ZombieHealth>();
            zombieHealth.TakeDamage(damage);
            Destroy(gameObject);
        



        }
    }
}
