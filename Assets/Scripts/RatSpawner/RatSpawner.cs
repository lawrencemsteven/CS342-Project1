using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public List<Transform> spawnLocations = new List<Transform>();
    public GameObject rat;

    public float spawnTime = 30.0f;
    private float spawnTimeCounter;

    void Start() {
        spawnTimeCounter = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimeCounter <= 0) {
            spawnTimeCounter = spawnTime;
            int location = Random.Range(0, spawnLocations.Count);
            Instantiate(rat, spawnLocations[location].position, Quaternion.identity);
        }
        spawnTimeCounter -= Time.deltaTime;
    }
}
