using UnityEngine;
using System.Collections.Generic;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects;
    public float spawnInterval = 5f;

    private float nextSpawnTime = 0f;
    private List<GameObject> shuffledList;
    private int currentIndex = 0;

    void Start()
    {
        ShuffleList();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnNextObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void ShuffleList()
    {
        shuffledList = new List<GameObject>(myObjects);

        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = shuffledList[i];
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }

        currentIndex = 0;
        Debug.Log("List shuffled! New order ready.");
    }

    void SpawnNextObject()
    {
        if (shuffledList == null || shuffledList.Count == 0)
        {
            Debug.LogWarning("No objects to spawn!");
            return;
        }

        GameObject objectToSpawn = shuffledList[currentIndex];

        if (objectToSpawn == null)
        {
            Debug.LogWarning("Object at index " + currentIndex + " is null!");
            currentIndex++;
            if (currentIndex >= shuffledList.Count)
            {
                ShuffleList();
            }
            return;
        }

        Vector3 randomSpawnPosition = new Vector3(Random.Range(-5, 5), 31, Random.Range(-8, 8));
        Instantiate(objectToSpawn, randomSpawnPosition, objectToSpawn.transform.rotation);

        currentIndex++;

        if (currentIndex >= shuffledList.Count)
        {
            ShuffleList();
        }
    }
}