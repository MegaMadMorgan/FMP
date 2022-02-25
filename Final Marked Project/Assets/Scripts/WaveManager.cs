using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public int WaveNum;
    public int WaveChapter;
    public bool EnemiesCleared;
    Scene CurrentScene;
    string SceneName;
    float SpawnRangeX;
    float SpawnRangeY;
    float SpawnRangeZ;

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

        if (CurrentScene.name == "CityCenter")
        {
            // set size of arena and spawn locations
        }

        if (CurrentScene.name == "Main")
        {
            SpawnRangeX = 12;
            SpawnRangeY = 20;
            SpawnRangeZ = 12;
        }

        if (EnemiesCleared)
        {
            WaveNum += 1;
            EnemiesCleared = false;
            while (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1)
            {
                Instantiate(enemy, new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), 20, Random.Range(-SpawnRangeZ, SpawnRangeZ)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            }
            if (GameObject.FindGameObjectsWithTag("Enemy").Length >= 1)
            {
                WaveNum += 1;
                EnemiesCleared = false;
            }

        }

        if (GameObject.Find("EnemyBase(Clone)") == null)
        {
            EnemiesCleared = true;
        }

    }
}

