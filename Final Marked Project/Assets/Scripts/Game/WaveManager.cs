using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int WaveNum;
    public int WaveChapter;
    public bool EnemiesCleared;
    Scene CurrentScene;
    string SceneName;
    private float SpawnRangeX;
    private float SpawnRangeY;
    private float SpawnRangeZ;

    public GameObject enemy;

    void Update()
    {
        CurrentScene = SceneManager.GetActiveScene();
        if (CurrentScene.name == "Park")
        {
            // set size of arena and spawn locations
        }

        if (CurrentScene.name == "SuperStore")
        {
            // set size of arena and spawn locations
        }

        if (CurrentScene.name == "City_Centre")
        {
            //these are not accurate spawn ranges
            SpawnRangeX = 6;
            SpawnRangeY = 20;
            SpawnRangeZ = 6;
        }

        if (CurrentScene.name == "Main")
        {
            SpawnRangeX = 12;
            SpawnRangeY = 20;
            SpawnRangeZ = 12;
        }
        //Instantiate(enemy, new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), 20, Random.Range(-SpawnRangeZ, SpawnRangeZ)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        if (EnemiesCleared)
        {
            WaveNum += 1;
            EnemiesCleared = false;
            while (GameObject.FindGameObjectsWithTag("Enemy").Length <= WaveNum-1) // 0.1
            {
                Instantiate(enemy, new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), 20, Random.Range(-SpawnRangeZ, SpawnRangeZ)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            }
            //if (GameObject.FindGameObjectsWithTag("Enemy").Length >= 0.1)
            //{
            //    WaveNum += 1;
            //    EnemiesCleared = false;
            //}

        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            EnemiesCleared = true;
        }
    }
}

