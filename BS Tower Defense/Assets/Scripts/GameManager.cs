using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Developed by Ryan Cooper 2021
public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int rounds;
    [SerializeField]
    private int money;
    [SerializeField]
    private int _premiumMoney;
    [SerializeField]
    private int health;
    [SerializeField]
    public TMP_Text healthText;
    [SerializeField]
    public TMP_Text moneyText;

    [SerializeField]
    public GameObject gameOverUI;

    public bool paused;
    #endregion Fields

    public int Money { get { return money; } }
    public int PremiumMoney { get { return _premiumMoney; } }


    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health;
        moneyText.text = "Money: " + money;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void breach(int damage)
    {
        health -= damage;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(false);
    }

    public void moneyEarned(int wealth)
    {
        money += wealth;
        moneyText.text = "Money: " + money;
    }
}
