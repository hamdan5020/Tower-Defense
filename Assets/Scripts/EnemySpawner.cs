using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy[] enemies;

    [SerializeField] float spawnInterval = 1f;

    [SerializeField] GameObject spawnPoint;

    [SerializeField] Waypoint enemyWaypoint;

    [SerializeField] Waypoint lastWaypoint;

    [SerializeField] int spawnCount = 10;

    [SerializeField] Button startWaveBtn;

    [SerializeField] int enemyTypeID = 0;

    [SerializeField] float enemyHealthMultiplier = 0.01f;

    

    private float enemyHealthAddition = 0;

    private int waveCount = 0;

     LevelManager levelManager;


    [SerializeField] List<int> nextWavesIDs = new List<int>();


    Enemy[] enemiesInField;

    int crntSpawnCount;

    Coroutine spawnLoop;

    bool inWave = false;

    bool gameEnded = false;


    // Start is called before the first frame update
    void Start()
    {
        crntSpawnCount = 0;
        levelManager = FindObjectOfType<LevelManager>();
        gameEnded = false;



        for (int i = 0; i < 3; i++)
        {
            enemyTypeID = UnityEngine.Random.Range(0, enemies.Length);
            nextWavesIDs.Add(enemyTypeID);
        }
    }


    // Update is called once per frame
    void Update()
    {
        enemiesInField = FindObjectsOfType<Enemy>();

        if (enemiesInField.Length <= 0 && inWave && !gameEnded)
        {


            inWave = false;
            startWaveBtn.interactable = true;
            
            waveCount++;
            enemyHealthAddition = levelManager.GetScore() * enemyHealthMultiplier;
            Debug.Log(waveCount);

            nextWavesIDs.RemoveAt(0);
            nextWavesIDs.Add(UnityEngine.Random.Range(0, enemies.Length));

            levelManager.UpdateWaveInfo();

            

        }

        
    }

    IEnumerator SpawnLoop()
    {
        while (crntSpawnCount < spawnCount)
        {
            SpawnEnemy();


            yield return new WaitForSeconds(spawnInterval);
            crntSpawnCount++;

            }
        

        StopCoroutine(spawnLoop);
        crntSpawnCount = 0;
        
        
    }

    private void SpawnEnemy()


    {


        Enemy enemy = Instantiate(enemies[nextWavesIDs.ElementAt(0)]);
        enemy.AddEnemyHealth(enemyHealthAddition);
        enemy.transform.position = spawnPoint.transform.position;

        if (enemy.Isflyer())
        {
            enemy.SetEnemyWaypoint(lastWaypoint);
        }else
        {
            enemy.SetEnemyWaypoint(enemyWaypoint);
        }

        

        
        
    }

    

    public void startSpawning()

    {
        

        if (!inWave)
        {
            spawnLoop = StartCoroutine(SpawnLoop());
            inWave = true;
            startWaveBtn.interactable = false;
        }
        
    }


    public List<int> GetNextWaves()
    {
        return nextWavesIDs;
    }

    public string IDtoName(int ID)
    {
        switch (ID)
        {
            case 0: return "Normal";
                
            case 1: return "Slow";

            case 2: return "Fast";

            case 3: return "Flyer";

            default: return "err";
        }

        
    }

    public int GetWaveCount()
    {
        return waveCount;
    }

    public void stopSpawns()
    {
        gameEnded = true;
        StopCoroutine(spawnLoop);
        startWaveBtn.interactable = false;
        
    }
}
