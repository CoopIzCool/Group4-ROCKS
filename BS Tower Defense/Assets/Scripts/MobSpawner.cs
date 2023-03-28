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
            enemy.GetComponent<EnemyBehavior>().scale = (1.0f + (waveNumber/(float)finalWave) * scale);
            enemy.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(spacing);
        }

        waveOrder.Clear();
        waveNumber++;
        incrementer++;
        //as waves increase, decrease the spawn time
        if(incrementer >= 10)
        {
            incrementer = 0;
            spacing -= 0.05f;
            if(spacing <= 0.15f)
            {
                spacing = 0.15f;
            }
        }
    }

    public void CreateWave()
    {
        maxCost = waveNumber;
        while (maxCost > 0)
        {
            //process of determining what enemies are spawned, may change later
            /*
            if (Random.Range(1, 100) > weightedValue && maxCost > 1)
            {
                if (maxCost >= 10 && Random.Range(1, 20) <= 8)
                {
                    int bossdecider = (int)(Random.value * 4.0f);
                    Debug.Log("Spawning something huge...");
                    switch (bossdecider)
                    {
                        case 0:
                            {
                                waveOrder.Add(6);
                                maxCost -= 10;
                            }
                            break;
                        case 1:
                            {
                                waveOrder.Add(7);
                                maxCost -= 10;
                            }
                            break;
                        case 2:
                            {
                                waveOrder.Add(9);
                                maxCost -= 10;
                            }
                            break;
                        case 3:
                            {
                                waveOrder.Add(10);
                                maxCost -= 10;
                            }
                            break;
                    }
                }
                if (Random.Range(1, 20) > 7 && maxCost > 5)
                {
                    if(Random.Range(1,10) >=5)
                    {
                        waveOrder.Add(4);
                    }
                    else
                    {
                        waveOrder.Add(8);
                    }
                   
                    maxCost -= 6;
                    //Debug.Log("Spawning Biggerer Enemy");
                }
                if (Random.Range(1, 30) > 20 && maxCost > 3)
                {
                    if (Random.Range(1, 30) > 15)
                    {
                        waveOrder.Add(3);
                    }
                    else
                    {
                        waveOrder.Add(5);
                    }
                    maxCost -= 4;
                    //Debug.Log("Spawning Bigger Enemy");
                }
                else if (maxCost > 1)
                {
                    waveOrder.Add(1 + Random.Range(0, 2));
                    maxCost -= 2;
                    //Debug.Log("Spawning Big Enemy");
                }
            }
            else
            {
                waveOrder.Add(0);
                maxCost -= 1;
            }
            */
            waveOrder.Add(0);
            maxCost -= 1;
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
