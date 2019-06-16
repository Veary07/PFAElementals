using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] Timer spawnTimer;
    [SerializeField] GameObject[] bonusObjects;
    [SerializeField] Transform[] bonusPositions;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer.SetDuration(Random.Range(20f, 50f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Update())
        {
            Instantiate(bonusObjects[Random.Range(0, bonusObjects.Length)], bonusPositions[Random.Range(0, bonusPositions.Length)].position, Quaternion.identity);
            spawnTimer.SetDuration(Random.Range(20f, 50f), 1);
        }
    }
}
