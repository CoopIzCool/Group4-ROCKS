using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Developed by Ryan Cooper 2021
public class EnemyBehavior : MonoBehaviour
{
    #region Fields
    public float hitpoints;
    public float maxHealth;
    public int wealth;
    //public HealthBar healthBar;
    public int damage;
    private GameManager gm;
    public bool armored;
    public float scale;

    #endregion

    #region Properties
    public GameManager GM
    {
        get { return gm; }
        set { gm = value; }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hitpoints = 5;
        hitpoints *= scale;
        maxHealth = hitpoints;
        //wealth = 5;
        damage = 1;
        gameObject.GetComponent<NavMeshAgent>().speed = 2;
        armored = false;
    }

    public void takeDamage(float damage)
    {
        if(armored)
        {
            damage /= 2;
        }
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Death(true);
        }

    }

    public virtual void Death(bool slain)
    {
        if (slain)
        {
            gm.moneyEarned(wealth);
        }
        gameObject.transform.GetComponentInParent<MobSpawner>().zombieDefeat();
        resetHealth();
        gameObject.SetActive(false);
    }

    public void resetHealth()
    {
        hitpoints = maxHealth;
        wealth = 5;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    

}
