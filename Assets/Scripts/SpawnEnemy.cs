using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private int wave;
    private float count;
    [SerializeField] private float spawnTime;
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;
    [SerializeField] private Transform MonsterContainer;
    // Start is called before the first frame update
    void Start()
    {
        wave = 4;
        spawnTime = 4f;
        count = spawnTime;
        Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (wave > 0)
        {
            spawnEnemy();
        }
    }

    private void spawnEnemy()
    {
        if (count <= 0)
        {
            Instantiate(testEnemyPrefab, MonsterContainer).GetComponent<MoveEnemy>().waypoints = waypoints;
            wave--;
            count = spawnTime;
        }
        else
            count -= Time.deltaTime;

    }
}
