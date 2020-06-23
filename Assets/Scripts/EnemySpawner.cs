using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnRadius = 100f;
    public float timeToSpawn = 1f;
    [SerializeField] private float timeNow = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = new Vector3(spawnRadius,transform.lossyScale.y, spawnRadius);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnLocation;
        if (timeNow < 0)
        {
            //print("spawn");
            spawnLocation = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
            Instantiate(spawnObject, spawnLocation+transform.position, transform.rotation);
            timeNow = timeToSpawn;
        }
        timeNow -= Time.deltaTime;
    }
}
