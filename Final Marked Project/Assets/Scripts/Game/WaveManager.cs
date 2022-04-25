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

    public GameObject Bouncer1;
    public GameObject Chaser1;
    public GameObject Defender1;
    public GameObject Bigger1;
    public GameObject DropPoder1;
    public GameObject Heavyer1;
    public GameObject Teleporter1;
    public GameObject Healer1;
    public GameObject Flyer1;

    public GameObject Bouncer2;
    public GameObject Chaser2;
    public GameObject Defender2;
    public GameObject Bigger2;
    public GameObject DropPoder2;
    public GameObject Heavyer2;
    public GameObject Teleporter2;
    public GameObject Healer2;
    public GameObject Flyer2;

    public GameObject Bouncer3;
    public GameObject Chaser3;
    public GameObject Defender3;
    public GameObject Bigger3;
    public GameObject DropPoder3;
    public GameObject Heavyer3;
    public GameObject Teleporter3;
    public GameObject Healer3;
    public GameObject Flyer3;
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
                whichenemy = Random.Range(0, 9);
                GameObject enemy = null;
                if (whichenemy == 0)
                {
                    enemy = Bouncer1;
                }
                if (whichenemy == 1)
                {
                    enemy = Chaser1;
                }
                if (whichenemy == 2)
                {
                    enemy = Defender1;
                }
                if (whichenemy == 3)
                {
                    enemy = Bigger1;
                }
                if (whichenemy == 4)
                {
                    enemy = DropPoder1;
                }
                if (whichenemy == 5)
                {
                    enemy = Heavyer1;
                }
                if (whichenemy == 6)
                {
                    enemy = Teleporter1;
                }
                if (whichenemy == 7)
                {
                    enemy = Healer1;
                }
                if (whichenemy == 8)
                {
                    enemy = Flyer1;
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
            this.GetComponent<LightingManager>().TimeOfDay += 1;
        }
    }
}

