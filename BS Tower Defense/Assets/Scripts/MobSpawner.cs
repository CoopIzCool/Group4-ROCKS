using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//Developed by Ryan Cooper 2021
public class MobSpawner : MonoBehaviour
{
    #region Fields
    public int finalWave;
    public List<string> tags;
    GameManager gameManager;
    public Transform spawnPoint;
    public Transform endPoint;
    public Button spawnWaveButton;
    public float timeBuffer = 5f;
    private int waveNumber = 1;
    private float spacing = 0.7f;
    public int incrementer = 0;
    [SerializeField]
    private int remainders = 0;
    public int maxCost;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Text waveText;
    private List<int> waveOrder = new List<int>();
    public int weightedValue = 30;
    ObjectPooler objPool;
    private Transform spawnSpot;
    [SerializeField]
    private float scale;
    #endregion

    #region Properties
    public Transform SpawnSpot
    {
        get { return spawnSpot; }
        set { spawnSpot = value; }
    }

    public int WaveNumber
    {
        get { return waveNumber; }
    }
    #endregion

    private void Start()
    {
        objPool = ObjectPooler.Instance;
        spawnSpot = transform;
    }

    public void Update()
    {
        
        if(remainders <=0 )
        {
            spawnWaveButton.gameObject.SetActive(true);
        }
        else
        {
            spawnWaveButton.gameObject.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            activateSpawner();
        }
    }

    public virtual IEnumerator spawnWave()
    {
        CreateWave();
        
        for (int i = 0; i < waveOrder.Count; i++)
        {
            //create new zombie
            GameObject enemy = objPool.SpawnFromPool(tags[waveOrder[i]], spawnSpot.position, spawnSpot.rotation);
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemy.gameObject.GetComponent<PathMovement>().Target = endPoint;
            enemy.GetComponent<EnemyBehavior>().GM = gm;
            enemy.GetComponent<EnemyBehavior>().scale = (1.0f +  ((waveNumber/(float)finalWave) * scale));
            enemy.transform.parent = gameObject.transform;
            Enemies._enemies.Add(enemy);
            yield return new WaitForSeconds(spacing * enemy.GetComponent<EnemyBehavior>().bufferTime);
        }

        waveOrder.Clear();
        waveNumber++;
        incrementer++;
        //as waves increase, decrease the spawn time
        if(incrementer >= 3)
        {
            incrementer = 0;
            spacing -= 0.05f;
            if(spacing <= 0.15f)
            {
                spacing = 0.15f;
            }
        }
        scale += Random.Range(0.05f, 0.15f);
    }

    public void CreateWave()
    {
        maxCost = waveNumber;
        while (maxCost > 0)
        {
            //Enemies to spawn on round 6
            if (waveNumber > 5)
            {
                int randomValue = Random.Range(0, 8);
                int[] costs = { 1, 1, 2, 2, 3,3,3,5 };
                if (maxCost - costs[randomValue] >= 0)
                {
                    if (randomValue == 1 || randomValue == 5)
                    {
                        waveOrder.Add(randomValue);
                        waveOrder.Add(randomValue);
                        waveOrder.Add(randomValue);
                        waveOrder.Add(randomValue);
                        waveOrder.Add(randomValue);
                        maxCost -= costs[randomValue];
                    }
                    else
                    {
                        waveOrder.Add(randomValue);
                        maxCost -= costs[randomValue];
                    }
                }
            }
            //Enemies to spawn on round 4
            else if(waveNumber > 3)
            {
                int randomValue = Random.Range(0, 4);
                int[] costs = { 1, 1, 2, 2 };
                if(maxCost - costs[randomValue] >= 0)
                {
                    if(randomValue == 1)
                    {
                        waveOrder.Add(1);
                        waveOrder.Add(1);
                        waveOrder.Add(1);
                        waveOrder.Add(1);
                        waveOrder.Add(1);
                        maxCost -= 1;
                    }
                    else
                    {
                        waveOrder.Add(randomValue);
                        maxCost -= costs[randomValue];
                    }
                }
            }
            else if (waveNumber > 1)
            {
                int randomValue = Random.Range(1, 3);
                Debug.Log(randomValue);
                if (randomValue > 1)
                {
                    waveOrder.Add(1);
                    waveOrder.Add(1);
                    waveOrder.Add(1);
                    waveOrder.Add(1);
                    waveOrder.Add(1);
                    maxCost -= 1;
                }
                else
                {
                    waveOrder.Add(0);
                    maxCost -= 1;
                }
            }
            else
            {
                waveOrder.Add(0);
                maxCost -= 1;
            }
        }
        for (int i = 0; i < waveOrder.Count; i++)
        {
            //Debug.Log(tags[waveOrder[i]]);
            remainders++;
        }

    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void activateSpawner()
    {
        if(gameManager.paused == false)
        {
            if (remainders <= 0)
            {
                remainders = 0;
                //waveText.text = "Wave Number: " + waveNumber;
                StartCoroutine(spawnWave());
            }
        }    
    }

    public void zombieDefeat()
    {
        remainders--;

        //check if it was the last zombie, if it was end the game
        if (remainders <= 0 && waveNumber >= finalWave)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void outsideSpawns()
    {
        remainders++;
    }


}
