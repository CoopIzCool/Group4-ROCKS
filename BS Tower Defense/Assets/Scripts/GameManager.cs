using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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
    public TMP_Text gemsBalance;
    [SerializeField]
    public GameObject GameOverUI;

    public bool paused;
    [SerializeField]
    public int gems;
    #endregion Fields

    public int Money { get { return money; } }
    public int PremiumMoney { get { return _premiumMoney; } }


    public int _costTower1;
    public int _costTower2;
    public int _costTower3;

    // Start is called before the first frame update
    void Start()
    {
        gems = Variables.Saved.Get<int>("gems");
        healthText.text = "Health: " + health;
        moneyText.text = "Money: " + money;
        gemsBalance.text = gems.ToString();
        paused = false;
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
        GameOverUI.SetActive(true);
    }

    public void moneyEarned(int wealth)
    {
        money += wealth;
        moneyText.text = "Money: " + money;
    }
}
