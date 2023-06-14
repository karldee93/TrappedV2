using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int waveNum = 1;
    public int enemyNum = 0;
    public int currentSpawnedEnemies;
    public int enemyKilled = 0;
    GameObject gameManager;
    public GameObject[] spawners;
    public GameObject enemy;
    float spawnTimer = 5f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        spawners = new GameObject[3];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject; // get the child of the spawner object relative to the i value and set it as that game object
        }
        gameManager.GetComponent<Score>().roundNum = waveNum;
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKilled >= enemyNum)
        {
            NextWave();
            spawn = true;
        }
        if (spawn)
        {
            for (int i = 0; i < enemyNum; i++) // if i is less then enemy number then spawn a new enemy until it is == enemy num
            {
                spawnTimer -= 1 * Time.deltaTime;
                if (spawnTimer <= 0)
                {
                    SpawnEnemy();
                    currentSpawnedEnemies += 1;
                    spawnTimer = 5f;
                    Debug.Log(enemyNum);
                }
            }
            if (currentSpawnedEnemies == enemyNum)
            {
                spawn = false;
                currentSpawnedEnemies = 0;
            }
        }

    }

    void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        // this will instantiate an enemy prefab at a random spawn point of the array and at the same rotation
        Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
    }

    void StartWave()
    {
        waveNum = 1;
        enemyNum = 2;
        enemyKilled = 0;

        for (int i = 0; i < enemyNum; i++) // if i is less then enemy number then spawn a new enemy until it is == enemy num
        {
            SpawnEnemy();
        }
    }

    public void NextWave()
    {
        waveNum++; // add 1 to wave num
        enemyNum *= 2; // this will increased the num of enemies at the start of each wave by 2
        enemyKilled = 0;
        gameManager.GetComponent<Score>().roundNum = waveNum;

    }

    public void SpawnControl()
    {

    }
}
