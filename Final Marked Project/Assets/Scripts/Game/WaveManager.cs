using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public int WaveNum;
    public int WaveCount;
    public int WaveChapter;
    public bool EnemiesCleared;
    Scene CurrentScene;
    string SceneName;
    private float SpawnRangeXmin;
    private float SpawnRangeXmax;
    private float SpawnRangeY;
    private float SpawnRangeZmin;
    private float SpawnRangeZmax;
    private int multiaxis = 1; // mechanism for complicated enviroments

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


    public GameObject BossBouncer;
    public GameObject BossTeleporter;
    public GameObject BossDefender;
    public GameObject BossHeavy;
    public GameObject BossDropPoder;

    public void Awake()
    {
        whichenemy = Random.Range(0, 9);
    }

    void FixedUpdate()
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
        
        if (EnemiesCleared)
        {
            if ((GameObject.FindGameObjectsWithTag("Enemy").Length <= WaveNum) && !(WaveNum % 10 == 0)) //&& !(WaveNum % 10 == 0)
            {                
                GameObject enemy = null;
                if (whichenemy == 0 && WaveNum <= 10)
                {
                    enemy = Bouncer1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 1 && WaveNum <= 10)
                {
                    enemy = Chaser1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 2 && WaveNum <= 10)
                {
                    enemy = Defender1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 3 && WaveNum <= 10)
                {
                    enemy = Bigger1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 4 && WaveNum <= 10)
                {
                    enemy = DropPoder1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 5 && WaveNum <= 10)
                {
                    enemy = Heavyer1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 6 && WaveNum <= 10)
                {
                    enemy = Teleporter1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 7 && WaveNum <= 10)
                {
                    enemy = Healer1;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 8 && WaveNum <= 10)
                {
                    enemy = Flyer1;
                    whichenemy = Random.Range(0, 9);
                }

                if (whichenemy == 0 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Bouncer2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 1 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Chaser2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 2 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Defender2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 3 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Bigger2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 4 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = DropPoder2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 5 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Heavyer2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 6 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Teleporter2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 7 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Healer2;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 8 && WaveNum >= 11 && WaveNum <= 20)
                {
                    enemy = Flyer2;
                    whichenemy = Random.Range(0, 9);
                }

                if (whichenemy == 0 && WaveNum >= 21)
                {
                    enemy = Bouncer3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 1 && WaveNum >= 21)
                {
                    enemy = Chaser3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 2 && WaveNum >= 21)
                {
                    enemy = Defender3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 3 && WaveNum >= 21)
                {
                    enemy = Bigger3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 4 && WaveNum >= 21)
                {
                    enemy = DropPoder3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 5 && WaveNum >= 21)
                {
                    enemy = Heavyer3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 6 && WaveNum >= 21)
                {
                    enemy = Teleporter3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 7 && WaveNum >= 21)
                {
                    enemy = Healer3;
                    whichenemy = Random.Range(0, 9);
                }
                if (whichenemy == 8 && WaveNum >= 21)
                {
                    enemy = Flyer3;
                    whichenemy = Random.Range(0, 9);
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


            if (WaveNum % 10 == 0 && (GameObject.FindGameObjectsWithTag("Enemy").Length <= 2))
            {
                whichenemy = Random.Range(1, 6);
                GameObject enemy = null;
                //BossWave
                Debug.Log("10th wave");

                if (whichenemy == 1)
                {
                    enemy = BossBouncer;
                }
                if (whichenemy == 2)
                {
                    enemy = BossTeleporter;
                }
                if (whichenemy == 3)
                {
                    enemy = BossDefender;
                }
                if (whichenemy == 4)
                {
                    enemy = BossHeavy;
                }
                if (whichenemy == 5)
                {
                    enemy = BossDropPoder;
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
        }

        if ((GameObject.FindGameObjectsWithTag("Enemy").Length >= WaveNum) || (WaveNum % 10 == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length >=1))
        {
            EnemiesCleared = false;
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            EnemiesCleared = true;
            WaveNum += 1;
            WaveCount += 1;
            this.GetComponent<LightingManager>().TimeOfDay += 1;
        }
    }
}

