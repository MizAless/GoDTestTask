using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            float randomX = Random.Range(-1f, 17f);
            float randomY = Random.Range(-7f, 1f);

            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            enemy.transform.position = randomPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
