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
    public bool blighted;
    public float bufferTime;
    private float redValue;
    private float redIncrementer;
    #endregion

    #region Properties
    public GameManager GM
    {
        get { return gm; }
        set { gm = value; }
    }
    #endregion
    // Start is called before the first frame update

    private void Awake()
    {
        //Enemies._enemies.Add(gameObject);
    }


    void Start()
    {
        //hitpoints = 50;
        hitpoints *= scale;
        maxHealth = hitpoints;
        //wealth = 5;
        //damage = 1;
        //gameObject.GetComponent<NavMeshAgent>().speed = 2;
        armored = false;
        redValue = GetComponent<SpriteRenderer>().color.r;
        //redIncrementer = ( 1- redValue) / 100.0f;
    }


    public void takeDamage(float damage)
    {
        if(armored)
        {
            damage /= 2;
        }
        hitpoints -= damage;
        ShowDamage(damage);
        if (hitpoints <= 0)
        {
            Death(true);
            //Enemies._enemies.Remove(gameObject);
            //gameObject.SetActive(false);
        }

    }

    public virtual void Death(bool slain)
    {

        Enemies._enemies.Remove(gameObject);

        if(slain)
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
        //wealth = 5;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void ShowDamage(float damage)
    {
        float dealtDamagePercent = damage / maxHealth;
        dealtDamagePercent = Mathf.Clamp(dealtDamagePercent, 0.001f, 1.0f);
        float redIncrementValue = dealtDamagePercent * (1-redValue);
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r + redIncrementValue, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b);
        Debug.Log(GetComponent<SpriteRenderer>().color.r);
    }

}
