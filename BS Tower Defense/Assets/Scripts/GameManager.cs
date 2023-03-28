using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Developed by Ryan Cooper 2021
public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int rounds;
    [SerializeField]
    private int money;
    [SerializeField]
    private int health;
    [SerializeField]
    public Text healthText;
    [SerializeField]
    public Text moneyText;

    [SerializeField]
    public GameObject gameoverUI;

    public bool paused;
    #endregion Fields

    public int Money { get { return money; } }

    // Start is called before the first frame update
    void Start()
    {
        //healthText.text = "Health: " + health;
        //moneyText.text = "Money: " + money;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void breach(int damage)
    {
        health -= damage;
        //healthText.text = "Health: " + health;
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameoverUI.SetActive(true);
    }

    public void moneyEarned(int wealth)
    {
        money += wealth;
        //moneyText.text = "Money: " + money;
    }
}
