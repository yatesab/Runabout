using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidObjects;
    public int amountAsteroidsToSpawn = 20;
    public float minRandomSpawn = -500;
    public float maxRandomSpawn = 500;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    void SpawnAsteroid()
    {
        for (int i = 0; i < amountAsteroidsToSpawn; i++)
        {
            float randomX = Random.Range(minRandomSpawn, maxRandomSpawn);
            float randomY = Random.Range(minRandomSpawn, maxRandomSpawn);
            float randomZ = Random.Range(minRandomSpawn, maxRandomSpawn);
            
            Vector3 randomSpawnPoint = new Vector3 (transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

            GameObject tempObj = Instantiate(asteroidObjects[Random.Range(0, asteroidObjects.Length - 1)], randomSpawnPoint, Quaternion.identity);
            tempObj.transform.parent = this.transform;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(maxRandomSpawn * 2, maxRandomSpawn * 2, maxRandomSpawn * 2));
    }
}
