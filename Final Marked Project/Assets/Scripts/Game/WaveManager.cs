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
    private float SpawnRangeXmin;
    private float SpawnRangeXmax;
    private float SpawnRangeY;
    private float SpawnRangeZmin;
    private float SpawnRangeZmax;
    private int multiaxis = 0; // mechanism for complicated enviroments

    public GameObject Bouncer;
    public GameObject Chaser;
    public GameObject Defender;
    public GameObject Bigger;
    public GameObject DropPoder;
    public GameObject Heavyer;
    public int whichenemy = 0;

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
            if (multiaxis == 0)
            {
                SpawnRangeXmin = -3;
                SpawnRangeXmax = 20;
                SpawnRangeY = 20;
                SpawnRangeZmin = -25;
                SpawnRangeZmax = 30;
            }
            if (multiaxis == 1)
            {
                SpawnRangeXmin = -32;
                SpawnRangeXmax = 22;
                SpawnRangeY = 20;
                SpawnRangeZmin = 10;
                SpawnRangeZmax = 30;
            }
        }

        if (CurrentScene.name == "Main")
        {
            SpawnRangeXmin = -12;
            SpawnRangeXmax = 12;
            SpawnRangeY = 20;
            SpawnRangeZmin = -12;
            SpawnRangeZmax = 12;
        }
        //Instantiate(enemy, new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), 20, Random.Range(-SpawnRangeZ, SpawnRangeZ)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        if (EnemiesCleared)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= WaveNum - 1) // 0.1
            {
                whichenemy = Random.Range(0, 6);
                GameObject enemy = null;
                if (whichenemy == 0)
                {
                    enemy = Bouncer;
                }
                if (whichenemy == 1)
                {
                    enemy = Chaser;
                }
                if (whichenemy == 2)
                {
                    enemy = Defender;
                }
                if (whichenemy == 3)
                {
                    enemy = Bigger;
                }
                if (whichenemy == 4)
                {
                    enemy = DropPoder;
                }
                if (whichenemy == 5)
                {
                    enemy = Heavyer;
                }

                if (multiaxis == 1)
                {
                    multiaxis = 0;
                    Instantiate(enemy, new Vector3(Random.Range(SpawnRangeXmin, SpawnRangeXmax), SpawnRangeY, Random.Range(SpawnRangeZmin, SpawnRangeZmax)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
                }
                else
                {
                    multiaxis = 1;
                    Instantiate(enemy, new Vector3(Random.Range(SpawnRangeXmin, SpawnRangeXmax), SpawnRangeY, Random.Range(SpawnRangeZmin, SpawnRangeZmax)), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
                }
            }
            else
            {
                EnemiesCleared = false;
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
            WaveNum += 1;
        }
    }
}

