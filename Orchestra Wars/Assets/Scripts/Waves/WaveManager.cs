﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    private float elapsedTime = 0f;
    private EnemyWave activeWave;
    private float spawnCounter = 0f;
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    void Awake() {
        Instance = this;
    }

    void Start(){

    }

    void Update() {
        if (GameManager.instance.isPaused != true){
        elapsedTime += Time.deltaTime;
        SearchForWave();
        UpdateActiveWave();
        //stops spawning when boss has arrived
        if (GameManager.instance.bossSpawned){
            StopSpawning();
        }
        }
    }
    private void SearchForWave() {
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            if (!activatedWaves.Contains(enemyWave) && enemyWave.startSpawnTimeInSeconds <= elapsedTime) {
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;
                break;
            }
        }
    }

    private void UpdateActiveWave() {
        
        if (activeWave != null) {
        spawnCounter += Time.deltaTime;

            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds) {
            spawnCounter = 0f;

                if (activeWave.listOfEnemies.Count != 0) {

                GameObject enemy = (GameObject)Instantiate(
                activeWave.listOfEnemies[0], WayPointManager.Instance.
                GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);

                enemy.GetComponent<EnemyScript>().pathIndex = activeWave.pathIndex;

                activeWave.listOfEnemies.RemoveAt(0);
                } else {

                activeWave = null;

                if (activatedWaves.Count == enemyWaves.Count) {

                }
            }
        }
    }
    }
    public void StopSpawning() {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}
