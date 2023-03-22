using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Developed by Ryan Cooper 2021
public class PathMovement : MonoBehaviour
{
    #region Fields
    GameManager gameManager;
    [SerializeField] 
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField]
    private float speed;
    private float distanceMoved = 0;
    #endregion

    #region Properties
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    public float DistanceMoved
    {
        get { return distanceMoved; }
    }
    #endregion


    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        speed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.paused == false)
        {
            agent.SetDestination(target.position);
            distanceMoved += speed;
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        
    }
}
